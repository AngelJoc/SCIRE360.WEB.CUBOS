using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidad
{
    public class Cubo_Planilla
    {
        public string RUC_COMPAÑIA { get; set; }
        public string PERSONAL_ID { get; set; }
        public string PERIODO_ID { get; set; }
        public string PERIODO { get; set; }
        public string MES_ID { get; set; }
        public string MES { get; set; }
        public string COMPANIA { get; set; }
        public string PLANILLA { get; set; }
        public string TRABAJADOR { get; set; }
        public string DNI { get; set; }
        public string CENTRO_DE_COSTO { get; set; }
        public string AREA { get; set; }
        public string CARGO { get; set; }
        public string AFP { get; set; }
        public string CUSPP { get; set; }
        public string EPS { get; set; }
        public string CATEGORIA { get; set; }
        public string CATEGORIA2 { get; set; }
        public string CATEGORIA3 { get; set; }
        public string CATEGORIA_AUXILIAR { get; set; }
        public string CATEGORIA_AUXILIAR2 { get; set; }
        public string ANEXO { get; set; }
        public string ANEXO2 { get; set; }
        public string BANCO_HABERES { get; set; }
        public string CUENTA_HABERES { get; set; }
        public string CUENTA_HABERES_CCI { get; set; }

        public string BANCO_CTS { get; set; }
        public string CUENTA_CTS { get; set; }
        public string CUENTA_CTS_CCI { get; set; }
        public string UBIGEO { get; set; }
        public string SITUACION { get; set; }
        public string PROYECTO { get; set; }
        public string TIPO_DE_TRABAJADOR { get; set; }
        public string TIPO_DE_CONTRATO { get; set; }
        public DateTime? FECHA_DE_INGRESO { get; set; }
        public DateTime? FECHA_INI_CONTRATO { get; set; }
        public DateTime? FECHA_FIN_CONTRATO { get; set; }
        public DateTime? FECHA_DE_CESE { get; set; }
        public string PROCESO { get; set; }
        public string COLUMNA { get; set; }
        public string CONCEPTO { get; set; }

        public decimal BASICO { get; set; }
        public decimal AFAM { get; set; }
        public decimal AFPMIXTA { get; set; }
        public decimal DIAS_LAB { get; set; }
        public decimal DIAS_VACA { get; set; }
        public decimal FALTAS { get; set; }
        public decimal DMEDICO { get; set; }
        public decimal SUBINCA { get; set; }
        public decimal SUBMATE { get; set; }
        public decimal LIC_PATER { get; set; }
        public decimal PERM_CGOCE { get; set; }
        public decimal PERM_SGOCE { get; set; }
        public decimal PERM_SIND { get; set; }
        public decimal LIC_SIND { get; set; }
        public decimal SUSP_LAB { get; set; }
        public decimal HED1 { get; set; }
        public decimal HED2 { get; set; }
        public decimal HEDB { get; set; }
        public decimal HEN1 { get; set; }
        public decimal HEN2 { get; set; }
        public decimal MIN_TARD { get; set; }
        public decimal FERIADO { get; set; }
        public decimal HUELGA { get; set; }

        public decimal? VALOR { get; set; }

    }
}