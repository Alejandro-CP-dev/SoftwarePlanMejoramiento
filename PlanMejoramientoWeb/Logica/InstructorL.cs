using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class InstructorL
    { 
        private InstructorD oInstructorD = new InstructorD();

        public bool MtRegistrarInstructor(Instructor instructor)
        {
            return oInstructorD.MtRegistrarInstructor(instructor);
        }

        public List<Instructor> MtListarInstructor()
        {
            return oInstructorD.MtListarInstructor();
        }

        public Instructor MtObtenerInstructorPorId(int id)
        {
            return oInstructorD.MtObtenerInstructorPorId(id);
        }

        public bool MtActualizarInstructor(Instructor instructor)
        {
            return oInstructorD.MtActualizarInstructor(instructor);
        }

        public bool MtEliminarInstructor(int id)
        {
            return oInstructorD.MtEliminarInstructor(id);
        }
    }
}