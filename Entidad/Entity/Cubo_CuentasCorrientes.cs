using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Cubo_CuentasCorrientes
    {
        public string TRABAJADOR { get; set; }
        public string DNI { get; set; }
        public string OPERACION { get; set; }
        public string PROCESO { get; set; }
        public string ESTADO_DE_PAGO { get; set; }
        public DateTime? FECHA_DE_SISTEMA { get; set; }
        public string OBSERVACIONES { get; set; }
        public string MOTIVO { get; set; }
        public Decimal? MONTO_DE_PRESTAMO { get; set; }
        public Decimal NRO_CUOTAS { get; set; }
        public Decimal NRO_CUOTAS_PENDIENTES { get; set; }
        public Decimal NRO_CUOTAS_CANCELADAS { get; set; }
        public string PERIODO { get; set; }
        public Decimal MONTO_CUOTA { get; set; }
    }
}
