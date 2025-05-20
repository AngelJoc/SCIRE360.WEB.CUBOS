using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public partial class Cubo_Vacation
    {
        public string TRABAJADOR { get; set; }
        public DateTime? FECHA_DE_INGRESO { get; set; }
        public DateTime? FECHA_DE_CORTE { get; set; }
        public int? ITEM { get; set; }
        public string PERIODO_VACACIONAL { get; set; }
        public decimal DIAS { get; set; }
        public decimal DIAS_PAGADOS { get; set; }
        public decimal DIAS_SALDO { get; set; }
        public decimal DIAS_GANADOS { get; set; }
        public decimal DIAS_GANADOS_SALDO { get; set; }
        public decimal DIAS_VENDIDOS { get; set; }
        public decimal IMPORTE_TOTAL { get; set; }
        public string PERIODO { get; set; }
        public DateTime? INICIO_VACACIONES { get; set; }
        public DateTime? FIN_VACACIONES { get; set; }
        public bool VENDIDO { get; set; }
        public decimal DIAS_VACACIONES { get; set; }
        public decimal IMPORTE { get; set; }
    }
}
