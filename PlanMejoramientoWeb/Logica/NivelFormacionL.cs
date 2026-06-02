using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class NivelFormacionL
    {
        private NivelFormacionD oNivelFormacion = new NivelFormacionD();

        public List<NivelFormacion> MtListarNivel()
        {
            return oNivelFormacion.MtListarNivel();
        }
    }
}