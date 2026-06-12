using PlanMejoramientoWeb.Datos;
using PlanMejoramientoWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramientoWeb.Logica
{
    public class AprendizL
    {
        private AprendizD oAprendizD = new AprendizD();

        public bool MtRegistrarAprendiz(Aprendiz aprendiz)
        {
            return oAprendizD.MtRegistrarAprendiz(aprendiz);
        }

        public List<Aprendiz> MtListarAprendiz()
        {
            return oAprendizD.MtListarAprendiz();
        }

        public Aprendiz MtObtenerAprendizPorId(int id)
        {
            return oAprendizD.MtObtenerAprendizPorId(id);
        }

        public bool MtActualizarAprendiz(Aprendiz aprendiz)
        {
            return oAprendizD.MtActualizarAprendiz(aprendiz);
        }

        public bool MtEliminarAprendiz(int id)
        {
            return oAprendizD.MtEliminarAprendiz(id);
        }

        public List<Aprendiz> MtListarAprendicesPorInstructor(int idInstructor)
        {
            return oAprendizD.MtListarAprendizPorInstructor(idInstructor);
        }

        public int MtRegistrarAprendizRetornandoId(Aprendiz aprendiz)
        {
            return oAprendizD.MtRegistrarAprendizRetornandoId(aprendiz);
        }

        public bool MtExisteDocumento(string numeroDocumento)
        {
            return oAprendizD.MtExisteDocumento(numeroDocumento);
        }
    }
}