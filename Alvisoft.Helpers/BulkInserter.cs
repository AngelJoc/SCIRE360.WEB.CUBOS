﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Fasterflect; // http://nuget.org/packages/fasterflect -- PM> Install-Package fasterflect


namespace Alvisoft.Helpers
{

    /// <summary>
    /// Name: SuperBulkInserter Principal Class for BulkInsert thousands record of data in seconds
    /// <remarks> Author: Anatoly Pedemonte Ku </remarks>
    /// </summary>
    /// 
    
    
    public class SuperBulkInsertEventArgs<T> : EventArgs
    {
        public SuperBulkInsertEventArgs(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException("items");
            Items = items.ToArray();
        }

        public T[] Items { get; private set; }
    }

    /// <summary>
    /// Performs buffered bulk inserts into a sql server table using objects instead of DataRows. 
    /// </summary>
    public class SuperBulkInserter<T> where T : class
    {
        public event EventHandler<SuperBulkInsertEventArgs<T>> PreBulkInsert;
        public void OnPreBulkInsert(SuperBulkInsertEventArgs<T> e)
        {
            var handler = PreBulkInsert;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<SuperBulkInsertEventArgs<T>> PostBulkInsert;
        public void OnPostBulkInsert(SuperBulkInsertEventArgs<T> e)
        {
            var handler = PostBulkInsert;
            if (handler != null) handler(this, e);
        }

        private const int DefaultBufferSize = 5000;
        private readonly SqlConnection _connection;
        private readonly int _bufferSize;
        public int BufferSize { get { return _bufferSize; } }

        private readonly Lazy<Dictionary<string, MemberGetter>> _props =
            new Lazy<Dictionary<string, MemberGetter>>(GetPropertyInformation);

        private readonly Lazy<DataTable> _dt;

        private readonly SqlBulkCopy _sbc;
        private readonly List<T> _queue = new List<T>();

        /// <param name="connection">SqlConnection to use for retrieving the schema of sqlBulkCopy.DestinationTableName</param>
        /// <param name="sqlBulkCopy">SqlBulkCopy to use for bulk insert.</param>
        /// <param name="bufferSize">Number of rows to bulk insert at a time. The default is 5000.</param>
        public SuperBulkInserter(SqlConnection connection, SqlBulkCopy sqlBulkCopy, int bufferSize = DefaultBufferSize)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (sqlBulkCopy == null) throw new ArgumentNullException("sqlBulkCopy");

            _bufferSize = bufferSize;
            _connection = connection;
            _sbc = sqlBulkCopy;
            _dt = new Lazy<DataTable>(CreateDataTable);
        }

        /// <param name="connection">SqlConnection to use for retrieving the schema of sqlBulkCopy.DestinationTableName and for bulk insert.</param>
        /// <param name="tableName">The name of the table that rows will be inserted into.</param>
        /// <param name="bufferSize">Number of rows to bulk insert at a time. The default is 5000.</param>
        /// <param name="copyOptions">Options for SqlBulkCopy.</param>
        /// <param name="sqlTransaction">SqlTransaction for SqlBulkCopy</param>
        public SuperBulkInserter(SqlConnection connection, string tableName, int bufferSize = DefaultBufferSize,
                            SqlBulkCopyOptions copyOptions = SqlBulkCopyOptions.Default, SqlTransaction sqlTransaction = null)
            : this(connection, new SqlBulkCopy(connection, copyOptions, sqlTransaction) { DestinationTableName = tableName }, bufferSize)
        {
        }

        /// <summary>
        /// Performs buffered bulk insert of enumerable items.
        /// </summary>
        /// <param name="items">The items to be inserted.</param>
        public void Insert(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException("items");

            // get columns that have a matching property, using reflection
            var cols = _dt.Value.Columns.Cast<DataColumn>()
                .Where(x => _props.Value.ContainsKey(x.ColumnName))
                .Select(x => new { Column = x, Getter = _props.Value[x.ColumnName] })
                .Where(x => x.Getter != null)
                .ToArray();

            foreach (var buffer in Buffer(items))
            {
                foreach (var item in buffer)
                {
                    var row = _dt.Value.NewRow();

                    foreach (var col in cols)
                        row[col.Column] = col.Getter(item);

                    _dt.Value.Rows.Add(row);
                }

                var bulkInsertEventArgs = new SuperBulkInsertEventArgs<T>(buffer);
                OnPreBulkInsert(bulkInsertEventArgs);

                _sbc.WriteToServer(_dt.Value);

                OnPostBulkInsert(bulkInsertEventArgs);

                _dt.Value.Clear();
            }
        }

        /// <summary>
        /// Queues a single item for bulk insert. When the queue count reaches the buffer size, bulk insert will happen.
        /// Call Flush() to manually bulk insert the currently queued items.
        /// </summary>
        /// <param name="item">The item to be inserted.</param>
        public void Insert(T item)
        {
            if (item == null) throw new ArgumentNullException("item");

            _queue.Add(item);

            if (_queue.Count == _bufferSize)
                Flush();
        }

        /// <summary>
        /// Bulk inserts the currently queued items.
        /// </summary>
        public void Flush()
        {
            Insert(_queue);
            _queue.Clear();
        }

        private static Dictionary<string, MemberGetter> GetPropertyInformation()
        {
            return typeof(T).Properties().ToDictionary(x => x.Name, x => x.DelegateForGetPropertyValue());
        }

        private DataTable CreateDataTable()
        {
            var dt = new DataTable();
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = string.Format("select top 0 * from {0}", _sbc.DestinationTableName);

                using (var reader = cmd.ExecuteReader())
                    dt.Load(reader);
            }

            return dt;
        }

        private IEnumerable<T[]> Buffer(IEnumerable<T> enumerable)
        {
            var buffer = new List<T>();
            foreach (var item in enumerable)
            {
                buffer.Add(item);
                if (buffer.Count >= BufferSize)
                {
                    yield return buffer.ToArray();
                    buffer.Clear();
                }
            }

            if (buffer.Count > 0)
                yield return buffer.ToArray();
        }
    }
}

