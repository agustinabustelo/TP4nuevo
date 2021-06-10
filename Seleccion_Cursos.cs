using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Solicitud_Inscripcion
{
    class Seleccion_Cursos
    {
        public static void InscribirAlumno()
        {
            //Definimos todas las variables que utilizaremos
            bool flagValidaciones = false;
            bool FlagGeneral = false;

            string NroRegistro;
            string CarreraIngresada;
            string CodigoMateriaIngresado;
            string CodigoCursoIngresado;
            string CursoAlt;
            string CodigoCursoAltIngresado;
            int CodigoCursoAltValidado;
            int SinCursoAlt = 0;

            int CodigoMateriaValidado;
            int CodigoCursoValidado;
            int ContadorMateriasAgregadas = 0;
            int ContadorMateriasAprobadas = 0;

            List<int> HistorialAlumnos = new List<int>();

            string ContinuarAgregando;


            //Cargamos las listas de carreras, materias y alumnos en el programa (mediante .txt)
            Carrera.CargarCarreras();
            Materia.CargarMaterias();
            AlumnoRegular.CargarAlumnos();


            //Solicitamos el ingreso del número de registro para validar el alumno. Se pedirá hasta que se ingrese un número válido (flagValidaciones = true).
            //Tener en cuenta que este paso no aparece en el caso de uso ya que se valida su identidad cuando el alumno ingresa al campus.

            do
            {
                Console.WriteLine("Ingresar su número de registro antes de empezar:");
                NroRegistro = Console.ReadLine();

                //Primero validamos el tipo de ingreso
                if (AlumnoRegular.ValidarNroRegistro(NroRegistro))
                {
                    //Luego validamos que se encuentre entre los alumnos regulares habilitados
                    if (AlumnoRegular.ValidarNroRegEnLista(Convert.ToInt32(NroRegistro)))
                    {
                        flagValidaciones = true;
                    }
                    else
                    {
                        flagValidaciones = false;
                    }

                }
                else
                {
                    flagValidaciones = false;
                }

            } while (flagValidaciones == false);

            do
            {
                Console.WriteLine("Se encuentra cursando las últimas 4 materias de la carrera? (S/N)");
                ContinuarAgregando = Console.ReadLine();

            } while (ContinuarAgregando.ToLower() != "s" && ContinuarAgregando.ToLower() != "n");

            //Se empiezan a solicitar los ingresos para la inscripción a cursos
            do
            {
                Curso.ListaCursosDeMaterias.Clear();
                ContinuarAgregando = "";

                //Se muestran las carreras para que el alumno elija en la que se anotará.
                Carrera.MostrarCarreras();

                Console.WriteLine("Ingresar el nombre de la carrera en la que se inscribirá: ");
                CarreraIngresada = Console.ReadLine();

                //Primero se valida el tipo de dato ingresado
                flagValidaciones = Carrera.ValidarCarreraIngresada(CarreraIngresada);

                if (flagValidaciones == true)
                {
                    //Luego se valida que la carrera exista en el listado de carreras cargadas
                    FlagGeneral = Carrera.ValidarCarreraEnLista(CarreraIngresada);

                    if (FlagGeneral == true)
                    {
                        //Una vez validada la carrera, se muestran en pantalla las materias disponibles para inscripción.
                        Console.WriteLine($"Presione [ENTER] para ver las materias disponibles de {CarreraIngresada}");
                        Console.ReadKey();

                        Materia.ObtenerMateriasDisponibles(CarreraIngresada);

                        string MateriasAprobadas;
                        string MateriasAprobadas2;
                        string NewMateriaAprobada;
                        int MateriasAprobadas2Validadas;
                        
                        Console.WriteLine("Ingresar el codigo de una materia que ya aprobaste: ");
                        MateriasAprobadas = Console.ReadLine();
                        Console.WriteLine(MateriasAprobadas);
                        flagValidaciones = Materia.ValidarCodigoMateria(MateriasAprobadas);

                        if (flagValidaciones == true)
                        {
                            CodigoMateriaValidado = Convert.ToInt32(MateriasAprobadas);
                            HistorialAlumnos.Add(CodigoMateriaValidado);

                            //Se valida que el código ingresado exista en la lista de materias.
                            if (Materia.ValidarCodigoMateriaenLista(CodigoMateriaValidado) == true)
                            {
                                do
                                {
                                    Console.WriteLine("Desea agregar otra materia aprobada? (S/N)");
                                    NewMateriaAprobada = Console.ReadLine();

                                } while (NewMateriaAprobada.ToLower() != "s" && NewMateriaAprobada.ToLower() != "n");

                                if (NewMateriaAprobada.ToUpper() == "S")
                                {
                                    do
                                    {
                                        Console.WriteLine("Ingresar el código de la siguiente materia aprobada: ");
                                        MateriasAprobadas2 = Console.ReadLine();

                                        flagValidaciones = Materia.ValidarCodigoMateria(MateriasAprobadas2);

                                        if (flagValidaciones == true)
                                        {
                                            MateriasAprobadas2Validadas = Convert.ToInt32(MateriasAprobadas2);
                                            HistorialAlumnos.Add(MateriasAprobadas2Validadas);

                                            if (Materia.ValidarCodigoMateriaenLista(MateriasAprobadas2Validadas))
                                            {
                                                if (MateriasAprobadas2Validadas == CodigoMateriaValidado)
                                                {
                                                    Console.WriteLine("No puede ingresar el mismo código que antes");
                                                    flagValidaciones = false;
                                                    continue;
                                                }
                                                else
                                                {
                                                    //Preguntara al usuario si desea agregar otra materia
                                                    if (ContadorMateriasAprobadas < 100)
                                                    {
                                                        do
                                                        {
                                                            Console.WriteLine("Desea agregar otra materia aprobada? (S/N)");
                                                            ContinuarAgregando = Console.ReadLine();

                                                        } while (ContinuarAgregando.ToLower() != "s" && ContinuarAgregando.ToLower() != "n");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                FlagGeneral = false;
                                            }
                                        }
                                        else
                                        {
                                            flagValidaciones = false;
                                        }
                                    } while (flagValidaciones == false || FlagGeneral == false);
                                }
                            }
                        }
                    }
                }
                



                //Se pide al usuario que ingrese el código de la materia en la que se inscribirá y se valida el ingreso.
                Console.WriteLine("Ingresar el código de la materia a la que desea inscribirse: ");
                CodigoMateriaIngresado = Console.ReadLine();

                flagValidaciones = Materia.ValidarCodigoMateria(CodigoMateriaIngresado);

                if (flagValidaciones == true)
                {
                    CodigoMateriaValidado = Convert.ToInt32(CodigoMateriaIngresado);

             

                    //Se valida que la materia no haya sido elegida aún en la inscripción.
                    if (!Materia.ValidarMateriasElegidas(CodigoMateriaValidado))
                    {
                        flagValidaciones = false;
                        continue;
                    }


                    //Se valida que no se haya aprobado aún
                    if (HistorialAlumnos.Contains(CodigoMateriaValidado))
                    {
                        Console.WriteLine("Esa materia ya fue aprobada. Elija otra.");
                        flagValidaciones = false;
                        continue;
                    }

                    //Se valida que el código ingresado exista en la lista de materias.
                    if (Materia.ValidarCodigoMateriaenLista(CodigoMateriaValidado) == true)
                    {

                        //Una vez validada la materia, se muestran en pantalla los cursos disponibles para la misma.
                        Console.WriteLine($"Presione [ENTER] para ver los cursos disponibles de la materia {CodigoMateriaValidado}");
                        Console.ReadKey();

                        //Cargo en la lista de cursos solo los que correspondan a la materia ingresada y los muestro al usuario.
                        Curso.CargarCursosCorrespondientesAMateria(CodigoMateriaValidado);
                        Curso.MostrarCursosDeMateria();

                        do
                        {

                            //lista preguntando que materias aprobo. si selecciona alguna se abre el txt materias aprobadas y se escriben las materias q aprobo (S/N). si pones q si que aparezca el listado de todas las materias cargadas. copiar el codigo de cuando te inscribis a la materia
                            //hacer otro txt llamado MateriasAprobadasPorAlumno que guarde el NroRegistro del alumno con el o los codigos de las materias que aprobo

                            //Se pide al usuario que ingrese el código del curso en el que se inscribirá y se valida el ingreso.
                            Console.WriteLine("Ingresar el código del curso al que desea inscribirse: ");
                            CodigoCursoIngresado = Console.ReadLine();

                            flagValidaciones = Curso.ValidarCodigoCurso(CodigoCursoIngresado);

                            if (flagValidaciones == true)
                            {
                                CodigoCursoValidado = Convert.ToInt32(CodigoCursoIngresado);

                                //Se valida que el código ingresado exista en la lista de cursos.
                                if (Curso.ValidarCodigoCursoEnLista(CodigoCursoValidado))
                                {
                                    do
                                    {
                                        Console.WriteLine("Desea agregar un curso alternativo? (S/N)");
                                        CursoAlt = Console.ReadLine();

                                    } while (CursoAlt.ToLower() != "s" && CursoAlt.ToLower() != "n");

                                    if (CursoAlt.ToUpper() == "S")
                                    {
                                        do
                                        {
                                            Console.WriteLine("Ingresar el código del curso alternativo al que desea inscribirse: ");
                                            CodigoCursoAltIngresado = Console.ReadLine();

                                            flagValidaciones = Curso.ValidarCodigoCurso(CodigoCursoIngresado);

                                            if (flagValidaciones == true)
                                            {
                                                CodigoCursoAltValidado = Convert.ToInt32(CodigoCursoAltIngresado);

                                                if (Curso.ValidarCodigoCursoEnLista(CodigoCursoAltValidado))
                                                {
                                                    if (CodigoCursoAltValidado == CodigoCursoValidado)
                                                    {
                                                        Console.WriteLine("No puede ingresar el mismo código que el curso principal");
                                                        flagValidaciones = false;
                                                        continue;
                                                    }
                                                    //se toma el codigo que eligio y se fija en MateriasCorrelativas si tiene correlativa o no
                                                    //if los datos de las correlativas son distintos de cero, agarro esos numeros. los guardo. voy a materiasaprobadasporalumno y me fijo (con el nroregistro) si tiene esos codigos aprobados. si los tiene aprobados sigo con el programa. sino, tiro un msj de error.
                                                    //if los datos de las correlativas son iguales a cero, no hago nada porque no tiene correlativas.
                                                    else
                                                    {
                                                        //Una vez que se validaron todos los ingresos se guardan en la solicitud de inscripción
                                                        Solicitud_Inscripcion.AgregarIngresosASolicitudDeInscripcionConCA(Convert.ToInt32(NroRegistro), CarreraIngresada, CodigoMateriaValidado, CodigoCursoValidado, CodigoCursoAltValidado);
                                                        flagValidaciones = true;
                                                        FlagGeneral = true;

                                                        //Al finalizar de agregar una materia, sumo una al contador. El programa dejara agregar hasta 3 materias.
                                                        ContadorMateriasAgregadas++;

                                                        //Preguntara al usuario si desea agregar otra materia, siempre que no haya agregado 3.
                                                        if (ContadorMateriasAgregadas < 3)
                                                        {
                                                            do
                                                            {
                                                                Console.WriteLine("Desea agregar otra materia a la solicitud? (S/N)");
                                                                ContinuarAgregando = Console.ReadLine();

                                                            } while (ContinuarAgregando.ToLower() != "s" && ContinuarAgregando.ToLower() != "n");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    FlagGeneral = false;
                                                }
                                            }
                                            else
                                            {
                                                flagValidaciones = false;
                                            }
                                        } while (flagValidaciones == false || FlagGeneral == false);
                                    }
                                    else
                                    {
                                        //Una vez que se validaron todos los ingresos se guardan en la solicitud de inscripción
                                        Solicitud_Inscripcion.AgregarIngresosASolicitudDeInscripcionSinCA(Convert.ToInt32(NroRegistro), CarreraIngresada, CodigoMateriaValidado, CodigoCursoValidado, SinCursoAlt);
                                        flagValidaciones = true;
                                        FlagGeneral = true;

                                        //Al finalizar de agregar una materia, sumo una al contador. El programa dejara agregar hasta 3 materias.
                                        ContadorMateriasAgregadas++;

                                        //Preguntara al usuario si desea agregar otra materia, siempre que no haya agregado 3.
                                        if (ContadorMateriasAgregadas < 3)
                                        {
                                            do
                                            {
                                                Console.WriteLine("Desea agregar otra materia a la solicitud? (S/N)");
                                                ContinuarAgregando = Console.ReadLine();

                                            } while (ContinuarAgregando.ToLower() != "s" && ContinuarAgregando.ToLower() != "n");
                                        }

                                    }
                                }
                                else
                                {
                                    FlagGeneral = false;
                                }

                            }
                            else
                            {
                                flagValidaciones = false;
                            }

                        } while (flagValidaciones == false || FlagGeneral == false);
                    }
                    else
                    {
                        FlagGeneral = false;
                    }
                }










            } while (flagValidaciones == false || FlagGeneral == false || ContinuarAgregando.ToLower() == "s");

            Solicitud_Inscripcion.GuardarSolicitud();

        }

    } 
}
