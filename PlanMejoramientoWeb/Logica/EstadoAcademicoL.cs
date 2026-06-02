using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class EstadoAcademicoL
    {
        private EstadoAcademicoD oEstadoAcademicoD = new EstadoAcademicoD();

        public List<EstadoAcademico> MtListarEstadoAcademico()
        {
            return oEstadoAcademicoD.MtListarEstadoAcademico();
        }
    }
}