using Entidad;
using System.IO;

using System.Diagnostics;
using System.ComponentModel;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Xml;
using System.Net;
using Alvisoft.Helpers;
using System.Threading.Tasks;
using Entidad.Entity;
using SCIRE360.WEB.CUBOS.API.Entity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System;

namespace Alvisoft.DataAccessLayer
{
    public class TblUsuarioDA
    {
        public async Task<List<Cubo_Planilla>> listarCuboPlanilla(string token,string corporateId, string id_compania, string id_ejercicio, string Proceso_Id, string id_planilla, string id_periodo, string id_mes, string id_cobertura)
        {
            List<Cubo_Planilla> lista = new List<Cubo_Planilla>();

            try
            {
                var data = new {
                    corporateId = corporateId,
                    id_compania = id_compania,
                    id_ejercicio = id_ejercicio,
                    Proceso_Id = Proceso_Id,
                    id_planilla = id_planilla,
                    id_periodo = id_periodo,
                    id_mes = id_mes,
                    id_cobertura = id_cobertura
                };
                StringContent _content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpClientHandler _clientHandlerGQ = new HttpClientHandler();
                _clientHandlerGQ.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var httpClient = new HttpClient(_clientHandlerGQ, false))
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(500);
                    httpClient.DefaultRequestHeaders.Add("Authorization", token/*Session["iToken"].ToString()*/);
                    string url = ConfigurationSettings.AppSettings["apiRest"].ToString() + "Cubos/listarCuboPlanilla";
                    using (var response = await httpClient.PostAsync(url, _content))
                    {
                        List<Cubo_Planilla> listaDes = new List<Cubo_Planilla>();
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        listaDes = JsonConvert.DeserializeObject<List<Cubo_Planilla>>(apiResponse);
                        if (listaDes != null)
                        {
                            lista = JsonConvert.DeserializeObject<List<Cubo_Planilla>>(apiResponse);
                        }
                    }

                    httpClient.Dispose();
                    _clientHandlerGQ.Dispose();
                }
            }
            catch (HttpException ex)
            {
                throw;
            }

            return lista;
        }
        public async Task<List<Cubo_Datos>> listarCuboDatos(string token, string corporateId, string id_compania, string id_ejercicio, string Proceso_Id, string id_planilla, string id_periodo, string id_mes, string id_cobertura)
        {
            List<Cubo_Datos> lista = new List<Cubo_Datos>();

            try
            {
                var data = new
                {
                    corporateId = corporateId,
                    id_compania = id_compania,
                    id_ejercicio = id_ejercicio,
                    Proceso_Id = Proceso_Id,
                    id_planilla = id_planilla,
                    id_periodo = id_periodo,
                    id_mes = id_mes,
                    id_cobertura = id_cobertura
                };
                StringContent _content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpClientHandler _clientHandlerGQ = new HttpClientHandler();
                _clientHandlerGQ.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var httpClient = new HttpClient(_clientHandlerGQ, false))
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", token/*Session["iToken"].ToString()*/);
                    string url = ConfigurationSettings.AppSettings["apiRest"].ToString() + "Cubos/listarCuboDatos";
                    using (var response = await httpClient.PostAsync(url, _content))
                    {
                        List<Cubo_Datos> listaDes = new List<Cubo_Datos>();
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        listaDes = JsonConvert.DeserializeObject<List<Cubo_Datos>>(apiResponse);
                        if (listaDes != null)
                        {
                            lista = JsonConvert.DeserializeObject<List<Cubo_Datos>>(apiResponse);
                        }
                    }

                    httpClient.Dispose();
                    _clientHandlerGQ.Dispose();
                }
            }
            catch (HttpException ex)
            {
                throw;
            }

            return lista;
        }
        public async Task<List<Cubo_Padron>> listarCuboPadron(string token, string corporateId, string id_compania, string id_ejercicio, string Proceso_Id, string id_planilla, string id_periodo, string id_mes, string id_cobertura)
        {
            List<Cubo_Padron> lista = new List<Cubo_Padron>();

            try
            {
                var data = new
                {
                    corporateId = corporateId,
                    id_compania = id_compania,
                    id_ejercicio = id_ejercicio,
                    id_planilla = id_planilla,
                    id_periodo = id_periodo,
                    id_mes = id_mes,
                    id_cobertura = id_cobertura
                };
                StringContent _content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpClientHandler _clientHandlerGQ = new HttpClientHandler();
                _clientHandlerGQ.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var httpClient = new HttpClient(_clientHandlerGQ, false))
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", token/*Session["iToken"].ToString()*/);
                    string url = ConfigurationSettings.AppSettings["apiRest"].ToString() + "Cubos/listarCuboPadron";
                    using (var response = await httpClient.PostAsync(url, _content))
                    {
                        List<Cubo_Padron> listaDes = new List<Cubo_Padron>();
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        listaDes = JsonConvert.DeserializeObject<List<Cubo_Padron>>(apiResponse);
                        if (listaDes != null)
                        {
                            lista = JsonConvert.DeserializeObject<List<Cubo_Padron>>(apiResponse);
                        }
                    }

                    httpClient.Dispose();
                    _clientHandlerGQ.Dispose();
                }
            }
            catch (HttpException ex)
            {
                throw;
            }

            return lista;
        }
        public async Task<List<Cubo_CuentasCorrientes>> listarCuboCuentasCorrientes(string token, string corporateId, string id_compania, string id_ejercicio, string Proceso_Id, string id_planilla, string id_periodo, string id_mes, string id_cobertura)
        {
            List<Cubo_CuentasCorrientes> lista = new List<Cubo_CuentasCorrientes>();

            try
            {
                var data = new
                {
                    corporateId = corporateId,
                    id_compania = id_compania,
                    id_ejercicio = id_ejercicio,
                    Proceso_Id = Proceso_Id,
                    id_planilla = id_planilla,
                    id_periodo = id_periodo,
                    id_mes = id_mes,
                    id_cobertura = id_cobertura
                };
                StringContent _content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpClientHandler _clientHandlerGQ = new HttpClientHandler();
                _clientHandlerGQ.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var httpClient = new HttpClient(_clientHandlerGQ, false))
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", token/*Session["iToken"].ToString()*/);
                    string url = ConfigurationSettings.AppSettings["apiRest"].ToString() + "Cubos/listarCuboCuentasCorrientes";
                    using (var response = await httpClient.PostAsync(url, _content))
                    {
                        List<Cubo_CuentasCorrientes> listaDes = new List<Cubo_CuentasCorrientes>();
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        listaDes = JsonConvert.DeserializeObject<List<Cubo_CuentasCorrientes>>(apiResponse);
                        if (listaDes != null)
                        {
                            lista = JsonConvert.DeserializeObject<List<Cubo_CuentasCorrientes>>(apiResponse);
                        }
                    }

                    httpClient.Dispose();
                    _clientHandlerGQ.Dispose();
                }
            }
            catch (HttpException ex)
            {
                throw;
            }

            return lista;
        }
        public async Task<List<Cubo_Vacation>> listarCuboVacation(string token, string corporateId, string id_compania, string id_ejercicio, string Proceso_Id, string id_planilla, string id_periodo, string id_mes, string id_cobertura)
        {
            List<Cubo_Vacation> lista = new List<Cubo_Vacation>();

            try
            {
                var data = new
                {
                    corporateId = corporateId,
                    id_compania = id_compania,
                    id_ejercicio = id_ejercicio,
                    Proceso_Id = Proceso_Id,
                    id_planilla = id_planilla,
                    id_periodo = id_periodo,
                    id_mes = id_mes,
                    id_cobertura = id_cobertura
                };
                StringContent _content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpClientHandler _clientHandlerGQ = new HttpClientHandler();
                _clientHandlerGQ.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var httpClient = new HttpClient(_clientHandlerGQ, false))
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(5);
                    httpClient.DefaultRequestHeaders.Add("Authorization", token/*Session["iToken"].ToString()*/);
                    string url = ConfigurationSettings.AppSettings["apiRest"].ToString() + "Cubos/listarCuboVacation";
                    using (var response = await httpClient.PostAsync(url, _content))
                    {
                        List<Cubo_Vacation> listaDes = new List<Cubo_Vacation>();
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        listaDes = JsonConvert.DeserializeObject<List<Cubo_Vacation>>(apiResponse);
                        if (listaDes != null)
                        {
                            lista = JsonConvert.DeserializeObject<List<Cubo_Vacation>>(apiResponse);
                        }
                    }

                    httpClient.Dispose();
                    _clientHandlerGQ.Dispose();
                }
            }
            catch (HttpException ex)
            {
                throw;
            }

            return lista;
        }

        public async Task<List<Cubo_AsientoContable>> listarCuboAsientoContable(string token, string corporateId, string id_compania, string id_ejercicio, string Proceso_Id, string id_planilla, string id_periodo, string id_mes, string id_asiento, string id_cobertura)
        {
            List<Cubo_AsientoContable> lista = new List<Cubo_AsientoContable>();

            try
            {
                var data = new
                {
                    corporateId = corporateId,
                    id_compania = id_compania,
                    id_ejercicio = id_ejercicio,
                    Proceso_Id = Proceso_Id,
                    id_planilla = id_planilla,
                    id_periodo = id_periodo,
                    id_mes = id_mes,
                    id_asiento = id_asiento,
                    id_cobertura = id_cobertura
                };
                StringContent _content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpClientHandler _clientHandlerGQ = new HttpClientHandler();
                _clientHandlerGQ.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var httpClient = new HttpClient(_clientHandlerGQ, false))
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(5);
                    httpClient.DefaultRequestHeaders.Add("Authorization", token/*Session["iToken"].ToString()*/);
                    string url = ConfigurationSettings.AppSettings["apiRest"].ToString() + "Cubos/listarCuboAsientoContable";
                    using (var response = await httpClient.PostAsync(url, _content))
                    {
                        List<Cubo_AsientoContable> listaDes = new List<Cubo_AsientoContable>();
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        listaDes = JsonConvert.DeserializeObject<List<Cubo_AsientoContable>>(apiResponse);
                        if (listaDes != null)
                        {
                            lista = JsonConvert.DeserializeObject<List<Cubo_AsientoContable>>(apiResponse);
                        }
                    }

                    httpClient.Dispose();
                    _clientHandlerGQ.Dispose();
                }
            }
            catch (HttpException ex)
            {
                throw;
            }

            return lista;
        }

        public async Task<RespuestaBE> getDisenoCubo(string token, string disenoCuboId)
        {
            RespuestaBE oR = new RespuestaBE();

            try
            {
                HttpClientHandler _clientHandlerGQ = new HttpClientHandler();
                _clientHandlerGQ.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var httpClient = new HttpClient(_clientHandlerGQ, false))
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(5);
                    httpClient.DefaultRequestHeaders.Add("Authorization", token/*Session["iToken"].ToString()*/);
                    string url = ConfigurationSettings.AppSettings["apiRest"].ToString() + "DisenoCubo/" + disenoCuboId;
                    using (var response = await httpClient.GetAsync(url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        oR = JsonConvert.DeserializeObject<RespuestaBE>(apiResponse);
                    }

                    httpClient.Dispose();
                    _clientHandlerGQ.Dispose();
                }
            }
            catch (HttpException ex)
            {
                oR.result = 0;
                oR.message = ex.Message;
            }

            return oR;
        }

        public async Task<RespuestaBE> saveCubo(string token, DisenoCubo ent)
        {
            RespuestaBE oR = new RespuestaBE();

            try
            {
                StringContent _content = new StringContent(JsonConvert.SerializeObject(ent), Encoding.UTF8, "application/json");
                HttpClientHandler _clientHandlerGQ = new HttpClientHandler();
                _clientHandlerGQ.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var httpClient = new HttpClient(_clientHandlerGQ, false))
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(5);
                    httpClient.DefaultRequestHeaders.Add("Authorization", token/*Session["iToken"].ToString()*/);
                    string url = ConfigurationSettings.AppSettings["apiRest"].ToString() + "DisenoCubo";
                    using (var response = await httpClient.PutAsync(url, _content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        oR = JsonConvert.DeserializeObject<RespuestaBE>(apiResponse);
                    }

                    httpClient.Dispose();
                    _clientHandlerGQ.Dispose();
                }
            }
            catch (HttpException ex)
            {
                oR.result = 0;
                oR.message = ex.Message;
            }

            return oR;
        }
    }
}
