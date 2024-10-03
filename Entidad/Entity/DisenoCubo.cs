using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public partial class DisenoCubo
    {
        public int DisenoCuboId { get; set; }
        public Guid UserId { get; set; }
        public string Descripcion { get; set; }
        public string Cubo { get; set; }
        public string Diseno { get; set; }
    }
}
