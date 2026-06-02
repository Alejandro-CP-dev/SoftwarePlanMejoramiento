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

        public bool MtRegistrarPrograma(Programa programa)
        {
            if (string.IsNullOrEmpty(programa.Codigo))
            {
                return false;
            }
            if (string.IsNullOrEmpty(programa.Nombre))
            {
                return false;
            }
            if (programa.Duracion <= 0)
            {
                return false;
            }

            return oProgramaD.MtRegistrarPrograma(programa);
        }

        public List<Programa> MtListarPrograma()
        {
            return oProgramaD.MtListarPrograma();
        }

        public Programa MtObtenerProgramaPorId(int id)
        {
            return oProgramaD.MtObtenerProgramaPorId(id);
        }

        public bool MtEliminarPrograma(int id)
        {
            return oProgramaD.MtEliminarPrograma(id);
        }

        public bool MtActualizarPrograma(Programa programa)
        {
            return oProgramaD.MtActualizarPrograma(programa);
        }
    }
}