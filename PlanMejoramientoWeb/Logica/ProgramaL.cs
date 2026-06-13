using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class ProgramaL
    {
        private ProgramaD oProgramaD = new ProgramaD();

        // Valida los campos obligatorios de un programa.
        // Devuelve null si todo está correcto, o un mensaje de error si algo falla.
        private string MtValidarPrograma(Programa programa)
        {
            if (string.IsNullOrWhiteSpace(programa.Codigo))
                return "El código del programa es obligatorio.";

            if (string.IsNullOrWhiteSpace(programa.Nombre))
                return "El nombre del programa es obligatorio.";

            if (string.IsNullOrWhiteSpace(programa.Version))
                return "La versión del programa es obligatoria.";

            if (programa.Duracion <= 0)
                return "La duración debe ser mayor a 0.";

            if (string.IsNullOrWhiteSpace(programa.Estado))
                return "Debe seleccionar un estado.";

            if (programa.NivelFormacion == null || programa.NivelFormacion.Id <= 0)
                return "Debe seleccionar un nivel de formación.";

            return null;
        }

        // Registra un programa nuevo.
        // Devuelve null si fue exitoso, o un mensaje de error.
        public string MtRegistrarPrograma(Programa programa)
        {
            string error = MtValidarPrograma(programa);
            if (error != null)
            {
                return error;
            }

            bool resultado = oProgramaD.MtRegistrarPrograma(programa);
            return resultado ? null : "No se pudo registrar el programa.";
        }

        // Actualiza un programa existente.
        public string MtActualizarPrograma(Programa programa)
        {
            if (programa.Id <= 0)
                return "Programa no válido.";

            string error = MtValidarPrograma(programa);
            if (error != null)
            {
                return error;
            }

            bool resultado = oProgramaD.MtActualizarPrograma(programa);
            return resultado ? null : "No se pudo actualizar el programa.";
        }

        // Elimina un programa, validando que no tenga fichas asociadas.
        public string MtEliminarPrograma(int id)
        {
            if (oProgramaD.MtTieneFichasAsociadas(id))
            {
                return "No se puede eliminar el programa porque tiene fichas asociadas.";
            }

            bool resultado = oProgramaD.MtEliminarPrograma(id);
            return resultado ? null : "No se pudo eliminar el programa.";
        }

        public List<Programa> MtListarPrograma()
        {
            return oProgramaD.MtListarPrograma();
        }

        public Programa MtObtenerProgramaPorId(int id)
        {
            return oProgramaD.MtObtenerProgramaPorId(id);
        }
    }
}