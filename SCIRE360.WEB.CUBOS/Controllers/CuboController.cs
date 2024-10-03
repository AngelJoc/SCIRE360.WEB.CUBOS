using C1.Web.Mvc.Olap;
using Entidad;
using Entidad.Entity;
using Negocio;
using Newtonsoft.Json;
using SCIRE360.WEB.CUBOS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace SCIRE360.WEB.CUBOS.Controllers
{
    //[CheckAuthorization]
    public class CuboController : Controller
    {
        string defaultHREF = string.Empty;

        bool DivInfoV = false;
        string DivInfoCss = "";
        string DivMessage = "";

        private static Dictionary<string, object[]> ChartSettings = new Dictionary<string, object[]>
        {
            {"ChartType", new object[] { PivotChartType.Column, PivotChartType.Area, PivotChartType.Bar, PivotChartType.Line, PivotChartType.Pie, PivotChartType.Scatter} }
        };

        // GET: Cube
        private readonly ClientSettingsModel OlapModells = new ClientSettingsModel
        {
            Settings = new Dictionary<string, object[]>
            {
                {"RowTotals", new object[] { ShowTotals.Subtotals, ShowTotals.None, ShowTotals.GrandTotals} },
                {"ColumnTotals", new object[] { ShowTotals.Subtotals, ShowTotals.None, ShowTotals.GrandTotals} },
                {"ShowZeros", new object[] { false, true } },
                { "AllowMerging", new object[] {
                  C1.Web.Mvc.Grid.AllowMerging.All,
                  C1.Web.Mvc.Grid.AllowMerging.AllHeaders,
                  C1.Web.Mvc.Grid.AllowMerging.Cells,
                  C1.Web.Mvc.Grid.AllowMerging.ColumnHeaders,
                  C1.Web.Mvc.Grid.AllowMerging.None,
                  C1.Web.Mvc.Grid.AllowMerging.RowHeaders
                } }
            }
        };

        // GET: Cubo
        public async Task<ActionResult> Index(string corpotareId, string ddl_compania, string ddl_planilla, string ddl_proceso, string ddl_cobertura, string ddl_ejercicio, string ddl_mes, string ddl_periodo, string cubo, string ddl_grafico, string disenoCuboId)
        {
            Session["iToken"] = "adadadadadawdaw";
            Session["corpotareId"] = corpotareId;

            if (Session["corpotareId"].ToString() != null)
            {
                Session["ddl_compania"] = ddl_compania;
                Session["ddl_planilla"] = ddl_planilla;
                Session["ddl_proceso"] = ddl_proceso;
                Session["ddl_cobertura"] = ddl_cobertura;
                Session["ddl_ejercicio"] = ddl_ejercicio;
                Session["ddl_mes"] = ddl_mes;
                Session["ddl_periodo"] = ddl_periodo;

                Session["cubo"] = cubo;
                Session["ddl_grafico"] = ddl_grafico;

                string ddl_companias = ""; string ddl_planillas = "";
                string ddl_procesos = ""; string ddl_coberturas = "";
                string ddl_ejercicios = ""; string ddl_mess = "";
                string ddl_periodos = ""; string cubos = "";
                string ddl_graficos = "";


                ddl_companias = Session["ddl_compania"].ToString();
                ddl_planillas = Session["ddl_planilla"].ToString();
                ddl_procesos = Session["ddl_proceso"].ToString();
                ddl_coberturas = Session["ddl_cobertura"].ToString();
                ddl_ejercicios = Session["ddl_ejercicio"].ToString();
                ddl_mess = Session["ddl_mes"].ToString();
                ddl_periodos = Session["ddl_periodo"].ToString();

                cubos = Session["cubo"].ToString();
                ddl_graficos = Session["ddl_grafico"].ToString();
                
                await cargarDiseno(disenoCuboId);

                if (cubos == "CuboCalculo")
                {
                    TblUsuarioNE ProcUsuario = new TblUsuarioNE();
                    List<Cubo_Planilla> Cubo_Planilla = new List<Cubo_Planilla>();

                    Cubo_Planilla = await ProcUsuario.listarCuboPlanilla(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;
                    /*if (Cubo_Planilla.Count > 0)
                    {
                        DivInfoV = false;
                        DivInfoCss = "";
                        DivMessage = "";

                        Session["DivInfoVC"] = DivInfoV;
                        Session["DivInfoCssC"] = DivInfoCss;
                        Session["DivMessageC"] = DivMessage;
                    }
                    else
                    {
                        DivInfoV = true;
                        DivInfoCss = "alert alert-danger";
                        DivMessage = "No hay información disponible para la selección.";

                        Session["DivInfoVC"] = DivInfoV;
                        Session["DivInfoCssC"] = DivInfoCss;
                        Session["DivMessageC"] = DivMessage;

                        defaultHREF = "CuboMaster";
                        string strPage = defaultHREF;
                        return RedirectToAction("Index", strPage);
                    }*/
                    return View(Cubo_Planilla);
                }
                if (cubos == "CuboDatos")
                {
                    TblUsuarioNE ProcUsuario = new TblUsuarioNE();
                    List<Cubo_Datos> Cubo_Datos = new List<Cubo_Datos>();

                    Cubo_Datos = await ProcUsuario.listarCuboDatos(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";
                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";
                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");
                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;
                    /*if (Cubo_Datos.Count > 0)
                    {
                        DivInfoV = false;
                        DivInfoCss = "";
                        DivMessage = "";

                        Session["DivInfoVC"] = DivInfoV;
                        Session["DivInfoCssC"] = DivInfoCss;
                        Session["DivMessageC"] = DivMessage;
                    }
                    else
                    {
                        DivInfoV = true;
                        DivInfoCss = "alert alert-danger";
                        DivMessage = "No hay información disponible para la selección.";

                        Session["DivInfoVC"] = DivInfoV;
                        Session["DivInfoCssC"] = DivInfoCss;
                        Session["DivMessageC"] = DivMessage;

                        defaultHREF = "CuboMaster";
                        string strPage = defaultHREF;
                        return RedirectToAction("Index", strPage);
                    }*/
                    return View(Cubo_Datos);
                }
                if (cubos == "CuboTrabajadores")
                {
                    TblUsuarioNE ProcUsuario = new TblUsuarioNE();
                    List<Cubo_Padron> Cubo_Padron = new List<Cubo_Padron>();
                    Cubo_Padron = await ProcUsuario.listarCuboPadron(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;
                    /*if (Cubo_Padron.Count > 0)
                    {
                        DivInfoV = false;
                        DivInfoCss = "";
                        DivMessage = "";

                        Session["DivInfoVC"] = DivInfoV;
                        Session["DivInfoCssC"] = DivInfoCss;
                        Session["DivMessageC"] = DivMessage;
                    }
                    else
                    {
                        DivInfoV = true;
                        DivInfoCss = "alert alert-danger";
                        DivMessage = "No hay información disponible para la selección.";

                        Session["DivInfoVC"] = DivInfoV;
                        Session["DivInfoCssC"] = DivInfoCss;
                        Session["DivMessageC"] = DivMessage;

                        defaultHREF = "CuboMaster";
                        string strPage = defaultHREF;
                        return RedirectToAction("Index", strPage);
                    }*/
                    return View(Cubo_Padron);
                }
                if (cubos == "CuboCtaCte")
                {
                    TblUsuarioNE ProcUsuario = new TblUsuarioNE();
                    List<Cubo_CuentasCorrientes> Cubo_CuentasCorrientes = new List<Cubo_CuentasCorrientes>();
                    Cubo_CuentasCorrientes = await ProcUsuario.listarCuboCuentasCorrientes(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;
                    /*if (Cubo_CuentasCorrientes.Count > 0)
                    {
                        DivInfoV = false;
                        DivInfoCss = "";
                        DivMessage = "";

                        Session["DivInfoVC"] = DivInfoV;
                        Session["DivInfoCssC"] = DivInfoCss;
                        Session["DivMessageC"] = DivMessage;
                    }
                    else
                    {
                        DivInfoV = true;
                        DivInfoCss = "alert alert-danger";
                        DivMessage = "No hay información disponible para la selección.";

                        Session["DivInfoVC"] = DivInfoV;
                        Session["DivInfoCssC"] = DivInfoCss;
                        Session["DivMessageC"] = DivMessage;

                        defaultHREF = "CuboMaster";
                        string strPage = defaultHREF;
                        return RedirectToAction("Index", strPage);

                    }*/
                    return View(Cubo_CuentasCorrientes);
                }
                if (cubos == "CuboVacaciones")
                {
                    TblUsuarioNE ProcUsuario = new TblUsuarioNE();
                    List<Cubo_Planilla> Cubo_Planilla = new List<Cubo_Planilla>();
                    Cubo_Planilla = await ProcUsuario.listarCuboPlanilla(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;
                    Session["Cubo_Planilla"] = Cubo_Planilla.Count.ToString();
                    /**if (Cubo_Planilla.Count > 0)
                    {
                        DivInfoV = false;
                        DivInfoCss = "";
                        DivMessage = "";

                        Session["DivInfoVC"] = DivInfoV;
                        Session["DivInfoCssC"] = DivInfoCss;
                        Session["DivMessageC"] = DivMessage;
                    }
                    else
                    {
                        DivInfoV = true;
                        DivInfoCss = "alert alert-danger";
                        DivMessage = "No hay información disponible para la selección.";

                        Session["DivInfoVC"] = DivInfoV;
                        Session["DivInfoCssC"] = DivInfoCss;
                        Session["DivMessageC"] = DivMessage;

                        defaultHREF = "CuboMaster";
                        string strPage = defaultHREF;
                        return RedirectToAction("Index", strPage);

                    }*/
                    return View(Cubo_Planilla);
                }
            }
            else
            {
                Session["ConfigParam"] = 1;
                defaultHREF = "setParametersinfo";
                string strPage = defaultHREF;
                return RedirectToAction("Index", strPage);
            }
            return View();

        }
        public async Task<ActionResult> Cubo_Datos(string corpotareId, string ddl_compania, string ddl_planilla, string ddl_proceso, string ddl_cobertura, string ddl_ejercicio, string ddl_mes, string ddl_periodo, string cubo, string ddl_grafico, string disenoCuboId)
        {
            Session["iToken"] = "adadadadadawdaw";
            Session["corpotareId"] = corpotareId;

            if (Session["corpotareId"].ToString() != null)
            {
                Session["ddl_compania"] = ddl_compania;
                Session["ddl_planilla"] = ddl_planilla;
                Session["ddl_proceso"] = ddl_proceso;
                Session["ddl_cobertura"] = ddl_cobertura;
                Session["ddl_ejercicio"] = ddl_ejercicio;
                Session["ddl_mes"] = ddl_mes;
                Session["ddl_periodo"] = ddl_periodo;

                Session["cubo"] = cubo;
                Session["ddl_grafico"] = ddl_grafico;

                string ddl_companias = ""; string ddl_planillas = "";
                string ddl_procesos = ""; string ddl_coberturas = "";
                string ddl_ejercicios = ""; string ddl_mess = "";
                string ddl_periodos = ""; string cubos = "";
                string ddl_graficos = "";


                ddl_companias = Session["ddl_compania"].ToString();
                ddl_planillas = Session["ddl_planilla"].ToString();
                ddl_procesos = Session["ddl_proceso"].ToString();
                ddl_coberturas = Session["ddl_cobertura"].ToString();
                ddl_ejercicios = Session["ddl_ejercicio"].ToString();
                ddl_mess = Session["ddl_mes"].ToString();
                ddl_periodos = Session["ddl_periodo"].ToString();

                cubos = Session["cubo"].ToString();
                ddl_graficos = Session["ddl_grafico"].ToString();

                await cargarDiseno(disenoCuboId);

                if (cubos == "CuboCalculo")
                {
                    TblUsuarioNE tblNE = new TblUsuarioNE();
                    List<Cubo_Planilla> Cubo_Planilla = new List<Cubo_Planilla>();

                    Cubo_Planilla = await tblNE.listarCuboPlanilla(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_Planilla);
                }
                if (cubos == "CuboDatos")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Datos> Cubo_Datos = new List<Cubo_Datos>();

                    Cubo_Datos = await Proc.listarCuboDatos(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";
                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";
                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");
                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;
                    return View(Cubo_Datos);
                }
                if (cubos == "CuboTrabajadores")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Padron> Cubo_Padron = new List<Cubo_Padron>();
                    Cubo_Padron = await Proc.listarCuboPadron(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_Padron);
                }
                if (cubos == "CuboCtaCte")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_CuentasCorrientes> Cubo_CuentasCorrientes = new List<Cubo_CuentasCorrientes>();
                    Cubo_CuentasCorrientes = await Proc.listarCuboCuentasCorrientes(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_CuentasCorrientes);
                }
                if (cubos == "CuboVacaciones")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Planilla> Cubo_Planilla = new List<Cubo_Planilla>();
                    Cubo_Planilla = await Proc.listarCuboPlanilla(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_Planilla);
                }
            }
            else
            {
                Session["ConfigParam"] = 1;
                defaultHREF = "setParametersinfo";
                string strPage = defaultHREF;
                return RedirectToAction("Index", strPage);
            }
            return View();
        }
        public async Task<ActionResult> Cubo_Padron(string corpotareId, string ddl_compania, string ddl_planilla, string ddl_proceso, string ddl_cobertura, string ddl_ejercicio, string ddl_mes, string ddl_periodo, string cubo, string ddl_grafico, string disenoCuboId)
        {
            Session["iToken"] = "adadadadadawdaw";

            Session["corpotareId"] = corpotareId;

            if (Session["corpotareId"].ToString() != null)
            {
                Session["ddl_compania"] = ddl_compania;
                Session["ddl_planilla"] = ddl_planilla;
                Session["ddl_proceso"] = ddl_proceso;
                Session["ddl_cobertura"] = ddl_cobertura;
                Session["ddl_ejercicio"] = ddl_ejercicio;
                Session["ddl_mes"] = ddl_mes;
                Session["ddl_periodo"] = ddl_periodo;

                Session["cubo"] = cubo;
                Session["ddl_grafico"] = ddl_grafico;

                string ddl_companias = ""; string ddl_planillas = "";
                string ddl_procesos = ""; string ddl_coberturas = "";
                string ddl_ejercicios = ""; string ddl_mess = "";
                string ddl_periodos = ""; string cubos = "";
                string ddl_graficos = "";


                ddl_companias = Session["ddl_compania"].ToString();
                ddl_planillas = Session["ddl_planilla"].ToString();
                ddl_procesos = Session["ddl_proceso"].ToString();
                ddl_coberturas = Session["ddl_cobertura"].ToString();
                ddl_ejercicios = Session["ddl_ejercicio"].ToString();
                ddl_mess = Session["ddl_mes"].ToString();
                ddl_periodos = Session["ddl_periodo"].ToString();

                cubos = Session["cubo"].ToString();
                ddl_graficos = Session["ddl_grafico"].ToString();

                await cargarDiseno(disenoCuboId);

                if (cubos == "CuboCalculo")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Planilla> Cubo_Planilla = new List<Cubo_Planilla>();

                    Cubo_Planilla = await Proc.listarCuboPlanilla(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_Planilla);
                }
                if (cubos == "CuboDatos")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Datos> Cubo_Datos = new List<Cubo_Datos>();

                    Cubo_Datos = await Proc.listarCuboDatos(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";
                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";
                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");
                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;
                    return View(Cubo_Datos);
                }
                if (cubos == "CuboTrabajadores")
                {
                    Session["iToken"] = "adawd";
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Padron> Cubo_Padron = new List<Cubo_Padron>();
                    Cubo_Padron = await Proc.listarCuboPadron(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_Padron);
                }
                if (cubos == "CuboCtaCte")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_CuentasCorrientes> Cubo_CuentasCorrientes = new List<Cubo_CuentasCorrientes>();
                    Cubo_CuentasCorrientes = await Proc.listarCuboCuentasCorrientes(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_CuentasCorrientes);
                }
                if (cubos == "CuboVacaciones")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Planilla> Cubo_Planilla = new List<Cubo_Planilla>();
                    Cubo_Planilla = await Proc.listarCuboPlanilla(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_Planilla);
                }
            }
            else
            {
                Session["ConfigParam"] = 1;
                defaultHREF = "setParametersinfo";
                string strPage = defaultHREF;
                return RedirectToAction("Index", strPage);
            }
            return View();
        }
        public async Task<ActionResult> Cubo_CuentasCorrientes(string corpotareId, string ddl_compania, string ddl_planilla, string ddl_proceso, string ddl_cobertura, string ddl_ejercicio, string ddl_mes, string ddl_periodo, string cubo, string ddl_grafico, string disenoCuboId)
        {

            Session["iToken"] = "adadadadadawdaw";
            Session["corpotareId"] = corpotareId;

            if (Session["corpotareId"].ToString() != null)
            {
                Session["ddl_compania"] = ddl_compania;
                Session["ddl_planilla"] = ddl_planilla;
                Session["ddl_proceso"] = ddl_proceso;
                Session["ddl_cobertura"] = ddl_cobertura;
                Session["ddl_ejercicio"] = ddl_ejercicio;
                Session["ddl_mes"] = ddl_mes;
                Session["ddl_periodo"] = ddl_periodo;

                Session["cubo"] = cubo;
                Session["ddl_grafico"] = ddl_grafico;

                string ddl_companias = ""; string ddl_planillas = "";
                string ddl_procesos = ""; string ddl_coberturas = "";
                string ddl_ejercicios = ""; string ddl_mess = "";
                string ddl_periodos = ""; string cubos = "";
                string ddl_graficos = "";


                ddl_companias = Session["ddl_compania"].ToString();
                ddl_planillas = Session["ddl_planilla"].ToString();
                ddl_procesos = Session["ddl_proceso"].ToString();
                ddl_coberturas = Session["ddl_cobertura"].ToString();
                ddl_ejercicios = Session["ddl_ejercicio"].ToString();
                ddl_mess = Session["ddl_mes"].ToString();
                ddl_periodos = Session["ddl_periodo"].ToString();

                cubos = Session["cubo"].ToString();
                ddl_graficos = Session["ddl_grafico"].ToString();

                await cargarDiseno(disenoCuboId);

                if (cubos == "CuboCalculo")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Planilla> Cubo_Planilla = new List<Cubo_Planilla>();

                    Cubo_Planilla = await Proc.listarCuboPlanilla(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_Planilla);
                }
                if (cubos == "CuboDatos")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Datos> Cubo_Datos = new List<Cubo_Datos>();

                    Cubo_Datos = await Proc.listarCuboDatos(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";
                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";
                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");
                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;
                    return View(Cubo_Datos);
                }
                if (cubos == "CuboTrabajadores")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Padron> Cubo_Padron = new List<Cubo_Padron>();
                    Cubo_Padron = await Proc.listarCuboPadron(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_Padron);
                }
                if (cubos == "CuboCtaCte")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_CuentasCorrientes> Cubo_CuentasCorrientes = new List<Cubo_CuentasCorrientes>();
                    Cubo_CuentasCorrientes = await Proc.listarCuboCuentasCorrientes(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_CuentasCorrientes);
                }
                if (cubos == "CuboVacaciones")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Planilla> Cubo_Planilla = new List<Cubo_Planilla>();
                    Cubo_Planilla = await Proc.listarCuboPlanilla(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_Planilla);
                }
            }
            else
            {
                Session["ConfigParam"] = 1;
                defaultHREF = "setParametersinfo";
                string strPage = defaultHREF;
                return RedirectToAction("Index", strPage);
            }
            return View();
        }
        public async Task<ActionResult> Cubo_Vacaciones(string corpotareId, string ddl_compania, string ddl_planilla, string ddl_proceso, string ddl_cobertura, string ddl_ejercicio, string ddl_mes, string ddl_periodo, string cubo, string ddl_grafico, string disenoCuboId)
        {
            Session["iToken"] = "adadadadadawdaw";

            Session["corpotareId"] = corpotareId;

            if (Session["corpotareId"].ToString() != null)
            {
                Session["ddl_compania"] = ddl_compania;
                Session["ddl_planilla"] = ddl_planilla;
                Session["ddl_proceso"] = ddl_proceso;
                Session["ddl_cobertura"] = ddl_cobertura;
                Session["ddl_ejercicio"] = ddl_ejercicio;
                Session["ddl_mes"] = ddl_mes;
                Session["ddl_periodo"] = ddl_periodo;

                Session["cubo"] = cubo;
                Session["ddl_grafico"] = ddl_grafico;

                string ddl_companias = ""; string ddl_planillas = "";
                string ddl_procesos = ""; string ddl_coberturas = "";
                string ddl_ejercicios = ""; string ddl_mess = "";
                string ddl_periodos = ""; string cubos = "";
                string ddl_graficos = "";
                

                ddl_companias = Session["ddl_compania"].ToString();
                ddl_planillas = Session["ddl_planilla"].ToString();
                ddl_procesos = Session["ddl_proceso"].ToString();
                ddl_coberturas = Session["ddl_cobertura"].ToString();
                ddl_ejercicios = Session["ddl_ejercicio"].ToString();
                ddl_mess = Session["ddl_mes"].ToString();
                ddl_periodos = Session["ddl_periodo"].ToString();

                cubos = Session["cubo"].ToString();
                ddl_graficos = Session["ddl_grafico"].ToString();

                await cargarDiseno(disenoCuboId);

                if (cubos == "CuboVacaciones")
                {
                    TblUsuarioNE Proc = new TblUsuarioNE();
                    List<Cubo_Vacation> Cubo_Vacation = new List<Cubo_Vacation>();
                    Cubo_Vacation = await Proc.listarCuboVacation(Session["iToken"].ToString(), corpotareId, ddl_companias, ddl_ejercicios, ddl_procesos, ddl_planillas, ddl_periodos, ddl_mess, ddl_coberturas);
                    OlapModells.ControlId = "indexPanel";

                    var engineModel1 = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
                    foreach (var item in OlapModells.Settings)
                    {
                        engineModel1.Settings.Add(item);
                    }
                    foreach (var chartItem in ChartSettings)
                    {
                        engineModel1.Settings.Add(chartItem);
                    }
                    engineModel1.ControlId = "chart";

                    ViewBag.id_grafico = new SelectList(ChartSettings, "ChartType", "ChartType");

                    ViewBag.DemoSettingsModel1 = engineModel1;
                    ViewBag.DemoSettingsModel = OlapModells;

                    return View(Cubo_Vacation);
                }
            }
            else
            {
                Session["ConfigParam"] = 1;
                defaultHREF = "setParametersinfo";
                string strPage = defaultHREF;
                return RedirectToAction("Index", strPage);
            }
            return View();
        }

        private async Task cargarDiseno(string disenoCuboId)
        {
            Session["disenoCuboId"] = disenoCuboId;
            if (disenoCuboId == "0")
            {
                Session["disenoCubo"] = "";
                return;
            }

            TblUsuarioNE ProcUsuario = new TblUsuarioNE();
            var response = await ProcUsuario.getDisenoCubo(Session["iToken"].ToString(), disenoCuboId);
            if (response.result == 1)
            {
                DisenoCubo disenoCubo = JsonConvert.DeserializeObject<DisenoCubo>(response.data.ToString());
                Session["disenoCubo"] = disenoCubo.Diseno;
            }
            else
            {
                Session["disenoCubo"] = "";
            }
        }
        [HttpPost]
        public ActionResult getView()
        {
            return Json(Session["disenoCubo"]);
        }
        [HttpPost]
        public async Task<ActionResult> SaveCubo(DisenoCubo ent)
        {
            RespuestaBE oR = new RespuestaBE();
            
            TblUsuarioNE ProcUsuario = new TblUsuarioNE();
            var response = await ProcUsuario.getDisenoCubo(Session["iToken"].ToString(), Session["disenoCuboId"].ToString());
            if (response.result == 1)
            {
                DisenoCubo disenoCubo = JsonConvert.DeserializeObject<DisenoCubo>(response.data.ToString());
                disenoCubo.Diseno = ent.Diseno;
                oR = await ProcUsuario.saveCubo(Session["iToken"].ToString(), disenoCubo);
            }
            else
            {
                oR = response;
            }
            return Json(oR, JsonRequestBehavior.AllowGet);
        }
    }
}