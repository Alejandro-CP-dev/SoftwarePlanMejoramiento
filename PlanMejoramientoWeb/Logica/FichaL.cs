using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class FichaL
    {
        private FichaD oFichaD = new FichaD();

        public bool MtRegistrarFicha(Ficha ficha)
        {
            return oFichaD.MtRegistrarFicha(ficha);
        }

        public List<Ficha> MtListarFicha()
        {
            return oFichaD.MtListarFicha();
        }

        public Ficha MtObtenerFichaPorId(int id)
        {
            return oFichaD.MtObtenerFichaPorId(id);
        }

        public bool MtActualizarFicha(Ficha ficha)
        {
            return oFichaD.MtActualizarFicha(ficha);
        }

        public bool MtEliminarFicha(int id)
        {
            return oFichaD.MtEliminarFicha(id);
        }
    }
}