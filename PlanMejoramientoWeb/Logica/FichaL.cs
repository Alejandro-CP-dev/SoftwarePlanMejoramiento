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
        private string MtValidarFicha(Ficha ficha, int idExcluir)
        {
            if (string.IsNullOrWhiteSpace(ficha.CodigoFicha))
                return "El código de la ficha es obligatorio.";

            if (ficha.FechaInicio == DateTime.MinValue)
                return "La fecha de inicio es obligatoria.";

            if (ficha.FechaFinalizacion == DateTime.MinValue)
                return "La fecha de finalización es obligatoria.";

            if (ficha.FechaFinalizacion <= ficha.FechaInicio)
                return "La fecha de finalización debe ser posterior a la fecha de inicio.";

            if (string.IsNullOrWhiteSpace(ficha.Estado))
                return "Debe seleccionar un estado.";

            if (ficha.Programa == null || ficha.Programa.Id <= 0)
                return "Debe seleccionar un programa.";

            if (ficha.Jornada == null || ficha.Jornada.Id <= 0)
                return "Debe seleccionar una jornada.";

            if (oFichaD.MtExisteCodigoFicha(ficha.CodigoFicha, idExcluir))
                return "Ya existe una ficha registrada con ese código.";

            return null;
        }

        public string MtRegistrarFicha(Ficha ficha)
        {
            string error = MtValidarFicha(ficha, 0);
            if (error != null)
            {
                return error;
            }

            bool resultado = oFichaD.MtRegistrarFicha(ficha);
            return resultado ? null : "No se pudo registrar la ficha.";
        }

        public string MtActualizarFicha(Ficha ficha)
        {
            if (ficha.Id <= 0)
                return "Ficha no válida.";

            string error = MtValidarFicha(ficha, ficha.Id);
            if (error != null)
            {
                return error;
            }

            bool resultado = oFichaD.MtActualizarFicha(ficha);
            return resultado ? null : "No se pudo actualizar la ficha.";
        }

        public string MtEliminarFicha(int id)
        {
            if (oFichaD.MtTieneAsignaciones(id))
            {
                return "No se puede eliminar la ficha porque tiene aprendices o instructores asignados.";
            }

            bool resultado = oFichaD.MtEliminarFicha(id);
            return resultado ? null : "No se pudo eliminar la ficha.";
        }

        public List<Ficha> MtListarFicha()
        {
            return oFichaD.MtListarFicha();
        }

        public Ficha MtObtenerFichaPorId(int id)
        {
            return oFichaD.MtObtenerFichaPorId(id);
        }

        public Ficha MtObtenerFichaPorCodigo(string codigo)
        {
            return oFichaD.MtObtenerFichaPorCodigo(codigo);
        }
    }
}