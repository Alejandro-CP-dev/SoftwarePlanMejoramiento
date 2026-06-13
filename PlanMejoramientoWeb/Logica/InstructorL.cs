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

        public string MtRegistrarInstructor(Instructor instructor)
        {
            if (string.IsNullOrWhiteSpace(instructor.Nombre) || string.IsNullOrWhiteSpace(instructor.Apellido))
                return "Nombre y apellido son obligatorios.";

            if (string.IsNullOrWhiteSpace(instructor.Correo))
                return "El correo es obligatorio.";

            if (string.IsNullOrWhiteSpace(instructor.Contrasena))
                return "La contraseña es obligatoria.";

            bool ok = oInstructorD.MtRegistrarInstructor(instructor);
            return ok ? null : "No se pudo registrar el instructor.";
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