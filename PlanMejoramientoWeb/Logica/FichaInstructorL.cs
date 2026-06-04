using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class FichaInstructorL
    {
        private FichaInstructorD oFichaInstructorD =
            new FichaInstructorD();

        public bool MtRegistrarFichaInstructor(
            FichaInstructor fichaInstructor)
        {
            return oFichaInstructorD.MtRegistrarFichaInstructor(fichaInstructor);
        }

        public List<Ficha> MtListarFichaInstructor(int idInstructor)
        {
            return oFichaInstructorD.MtListarFichaInstructor(idInstructor);
        }

        public bool MtEliminarFichaInstructor(int id)
        {
            return oFichaInstructorD.MtEliminarFichaInstructor(id);
        }

        public List<Ficha> MtListarFichaPorInstructor(int idInstructor)
        {
            return oFichaInstructorD.MtListarFichasPorInstructor(idInstructor);
        }
    }
}