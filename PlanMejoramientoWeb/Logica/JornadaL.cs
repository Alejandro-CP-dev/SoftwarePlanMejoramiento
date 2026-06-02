using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class JornadaL
    {
        private JornadaD oJornadaD = new JornadaD();

        public bool MtRegistrarJornada(Jornada jornada)
        {
            return oJornadaD.MtRegistrarJornada(jornada);
        }

        public List<Jornada> MtListarJornada()
        {
            return oJornadaD.MtListarJornada();
        }

        public Jornada MtObtenerJornadaPorId(int id)
        {
            return oJornadaD.MtObtenerJornadaPorId(id);
        }

        public bool MtActualizarJornada(Jornada jornada)
        {
            return oJornadaD.MtActualizarJornada(jornada);
        }

        public bool MtEliminarJornada(int id)
        {
            return oJornadaD.MtEliminarJornada(id);
        }
    }
}