using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class FichaAprendizL
    {
        private FichaAprendizD oFichaAprendiz = new FichaAprendizD();

        public bool MtRegistrarFichaAprendiz(FichaAprendiz fichaAprendiz)
        {
            return oFichaAprendiz.MtRegistrarFichaAprendiz(fichaAprendiz);
        }
        public List<FichaAprendiz> MtListarFichaAprendiz()
        {
            return oFichaAprendiz.MtListarFichaAprendiz();
        }
        
    }
}