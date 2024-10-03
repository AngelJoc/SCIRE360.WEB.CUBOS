using Alvisoft.DataAccessLayer;
using Entidad;
using Entidad.Entity;
using SCIRE360.WEB.CUBOS.API.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TblUsuarioNE
    {
        public async Task<List<Cubo_Planilla>> listarCuboPlanilla(string token, string corporateId, string id_compania, string id_ejercicio, string Proceso_Id, string id_planilla, string id_periodo, string id_mes, string id_cobertura)
        {
            TblUsuarioDA Proc = new TblUsuarioDA();
            return await Proc.listarCuboPlanilla(token, corporateId, id_compania, id_ejercicio, Proceso_Id, id_planilla, id_periodo, id_mes, id_cobertura);
        }
        public async Task<List<Cubo_Datos>> listarCuboDatos(string token, string corporateId, string id_compania, string id_ejercicio, string Proceso_Id, string id_planilla, string id_periodo, string id_mes, string id_cobertura)
        {
            TblUsuarioDA Proc = new TblUsuarioDA();
            return await Proc.listarCuboDatos(token, corporateId, id_compania, id_ejercicio, Proceso_Id, id_planilla, id_periodo, id_mes, id_cobertura);
        }
        public async Task<List<Cubo_Padron>> listarCuboPadron(string token, string corporateId, string id_compania, string id_ejercicio, string Proceso_Id, string id_planilla, string id_periodo, string id_mes, string id_cobertura)
        {
            TblUsuarioDA Proc = new TblUsuarioDA();
            return await Proc.listarCuboPadron(token, corporateId, id_compania, id_ejercicio, Proceso_Id, id_planilla, id_periodo, id_mes, id_cobertura);
        }
        public async Task<List<Cubo_CuentasCorrientes>> listarCuboCuentasCorrientes(string token, string corporateId, string id_compania, string id_ejercicio, string Proceso_Id, string id_planilla, string id_periodo, string id_mes, string id_cobertura)
        {
            TblUsuarioDA Proc = new TblUsuarioDA();
            return await Proc.listarCuboCuentasCorrientes(token, corporateId, id_compania, id_ejercicio, Proceso_Id, id_planilla, id_periodo, id_mes, id_cobertura);
        }
        public async Task<List<Cubo_Vacation>> listarCuboVacation(string token, string corporateId, string id_compania, string id_ejercicio, string Proceso_Id, string id_planilla, string id_periodo, string id_mes, string id_cobertura)
        {
            TblUsuarioDA Proc = new TblUsuarioDA();
            return await Proc.listarCuboVacation(token, corporateId, id_compania, id_ejercicio, Proceso_Id, id_planilla, id_periodo, id_mes, id_cobertura);
        }
        public async Task<RespuestaBE> getDisenoCubo(string token, string disenoCuboId)
        {
            TblUsuarioDA Proc = new TblUsuarioDA();
            return await Proc.getDisenoCubo(token, disenoCuboId);
        }
        public async Task<RespuestaBE> saveCubo(string token, DisenoCubo ent)
        {
            TblUsuarioDA Proc = new TblUsuarioDA();
            return await Proc.saveCubo(token, ent);
        }
    }
}
