using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Solicitud_Inscripcion
{
    class Solicitud_Inscripcion
    {
        public int NroRegistro { get; set; }
        public string Carrera { get; set; }
        public int codigoMateria { get; set; }
        public int codigoCurso { get; set; }
        public int codigoCursoAlt { get; set; }

        public static List<Solicitud_Inscripcion> ListaCursosConfirmados = new List<Solicitud_Inscripcion>();

        //Con Curso alternativo
        public static void AgregarIngresosASolicitudDeInscripcionConCA(int NroRegistro, string CarreraIngresada, int CodigoMateria, int CodigoCurso, int CodigoCursoAlt)
        {

            Solicitud_Inscripcion SolicitudInscripcion = new Solicitud_Inscripcion();

            //Guardo en una nueva solicitud todos los datos de la inscripción que realizó el usuario
            SolicitudInscripcion.NroRegistro = NroRegistro;
            SolicitudInscripcion.Carrera = CarreraIngresada;
            SolicitudInscripcion.codigoMateria = CodigoMateria;
            SolicitudInscripcion.codigoCurso = CodigoCurso;
            SolicitudInscripcion.codigoCursoAlt = CodigoCursoAlt;

            ListaCursosConfirmados.Add(SolicitudInscripcion);

        }

        //Sin curso alternativo
        public static void AgregarIngresosASolicitudDeInscripcionSinCA(int NroRegistro, string CarreraIngresada, int CodigoMateria, int CodigoCurso, int SinCursoAlt)
        {

            Solicitud_Inscripcion SolicitudInscripcion = new Solicitud_Inscripcion();

            //Guardo en una nueva solicitud todos los datos de la inscripción que realizó el usuario
            SolicitudInscripcion.NroRegistro = NroRegistro;
            SolicitudInscripcion.Carrera = CarreraIngresada;
            SolicitudInscripcion.codigoMateria = CodigoMateria;
            SolicitudInscripcion.codigoCurso = CodigoCurso;
            SolicitudInscripcion.codigoCursoAlt = SinCursoAlt;

            ListaCursosConfirmados.Add(SolicitudInscripcion);

        }

        public string Formato()
        {
            return string.Format("Registro: {0}- Carrera: {1}- Código materia: {2}- Código Curso: {3}- Código curso Alternativo: {4}", NroRegistro, Carrera,codigoMateria, codigoCurso,codigoCursoAlt);

        }


        public static void GuardarSolicitud()
        {
            string Path = @"/Users/agustinabustelo/Downloads/Entrega-TP4-Grupo-J-main/bin/Debug/Solicitud_Inscripcion.txt";
            FileInfo FI = new FileInfo(Path);

            //StreamWriter SW = new StreamWriter(Path);

            StreamWriter SW;

            SW = File.AppendText(Path);



            foreach (Solicitud_Inscripcion S in ListaCursosConfirmados)
            {

                SW.WriteLine(S.Formato());
            }

            SW.Close();

            Console.WriteLine("Se ha guardado correctamente el archivo en la ruta: " + Path);
        }

    }
}
