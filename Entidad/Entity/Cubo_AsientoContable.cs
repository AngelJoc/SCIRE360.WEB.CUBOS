using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Cubo_AsientoContable
    {
        public string ASIENTO { get; set; }
        public string PERIODO { get; set; }
        public string CUENTA { get; set; }
        public string PLAN_DE_CUENTA { get; set; }
        public int? ANALITICA { get; set; }
        public string CENTRO_DE_COSTO { get; set; }
        public string AREAS { get; set; }
        public string TIPO_DE_TRABAJADOR { get; set; }
        public string PARTIDA_PRESUPUESTARIA { get; set; }
        public decimal DEBE { get; set; }
        public decimal HABER { get; set; }
        public string CTACTE { get; set; }
        public string CUENTA_RESUMIDA { get; set; }
    }
}
