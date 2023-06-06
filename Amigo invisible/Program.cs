using System;

namespace Amigo_invisible
{
    class Program
    {
        static void Main(string[] args)
        {
            bool repetir = true; //Para decidir cuando salir del bucle
            int participantes = 5;//0//En una versión oficial, sin personas ya insertadas, esta variable tendría por valor 0//Cantidad de participantes
            string[] nombres = new string[30];//Array de nombres. Porque que el límite es 30
            string[] asignaciones = new string[30];//=nombres pero será necesario
            int cantidadParejas = 0;
            string[] parejas = new string[30];
            bool haSalidoBien = true;//Por si queda alguien regalándose a sí mismo en el básico
            string opcion = "0";
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n\tElija una opción:");//Opciones a elegir
                Console.WriteLine("\t1-Introducir nombre");
                Console.WriteLine("\t2-Introducir parejas");
                Console.WriteLine("\t3-Amigo invisible básico");
                Console.WriteLine("\t4-Amigo invisible avanzado");
                Console.WriteLine("\t5-Salir");
                Console.Write("\n\t");
                if (haSalidoBien == true)
                {
                    opcion = Console.ReadLine();//Elegir opción
                }
                //
                //Estos nombres son los ya insertados/**/
                nombres[0] = "Fede";
                nombres[1] = "Ana";
                nombres[2] = "Sara";
                nombres[3] = "Andrea";
                nombres[4] = "Paco";
                asignaciones[0]= "Fede";
                asignaciones[1] = "Ana";
                asignaciones[2] = "Sara";
                asignaciones[3] = "Andrea";
                asignaciones[4] = "Paco";
                /**/
                //
                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("\n\tAviso: 30 personas máximo");
                        if (participantes >= 30)//Así evitamos que se introduzca más de 30 nombres
                        {
                            Console.WriteLine("\tError: la cantidad de participantes ya es de 30");
                        }
                        else
                        {
                            IntroducirNombres(ref nombres, participantes, ref asignaciones);//Para poder introducir los nombres
                            participantes++;//Cada vez que se decida introducir nuevo nombre, suma automáticamente 1 a la cantidad
                        }
                        break;
                    case "2":
                        Parejas(nombres, ref parejas, participantes, ref cantidadParejas);
                        break;
                    case "3":
                        Console.Clear();
                        AmigoInvisible(nombres, participantes, asignaciones, parejas, cantidadParejas, ref haSalidoBien);
                        break;

                    case "4":
                        Console.Clear();
                        AmigoInvisibleCircularCon(nombres, participantes, asignaciones, parejas, cantidadParejas);
                        break;

                    case "5":
                        repetir = false;//Así se consigue salir
                        break;
                    default:
                        Console.WriteLine("\tError.");
                        break;
                }
            } while (repetir);
        }





        static string[] IntroducirNombres(ref string[] nombres,int participantes, ref string[] asignaciones)//Lo he creado así porque en el enunciado entendí que después de introducir un nombre debía volver al menú pero después de ver el de otros compañeros me crea dudas
        { 
            Console.WriteLine("\t¿Cuál es el nombre de la persona en la posición " + (participantes+1) + "?"); //Esto permite saber cuántos hay ya
            Console.Write("\n\t");
            string nombre = Console.ReadLine(); //Esta variable permite guardar los nombres en ambos arrays sin problemas
            nombres[participantes] = nombre;
            asignaciones[participantes] = nombre;
            return nombres;
        }















        static void Parejas(string[] nombres, ref string[] parejas, int participantes, ref int cantidadParejas)
        {
            Console.WriteLine("\n\n\tEstos son los participantes:");
            for (int i=0; i< participantes; i++)//Esto permite facilitar la comprobación de aqué número corresponde cada persona
            {
                Console.Write("\n\t");
                Console.WriteLine((i + 1) +".  " + nombres[i]);
            }
            Console.Write("\n\t");
            Console.WriteLine("Introduzca el número de uno de los integrantes de la pareja");
            Console.Write("\n\t");
            int e= Convert.ToInt32(Console.ReadLine());
            parejas[cantidadParejas] = nombres[e-1];
            cantidadParejas++;
            Console.Write("\n\t");
            Console.WriteLine("Introduzca el número del otro integrante de la pareja");
            Console.Write("\n\t");
            e = Convert.ToInt32(Console.ReadLine());
            parejas[cantidadParejas] = nombres[e-1];
            cantidadParejas++;
        }


        //Ha sido creado mediante dos arrays idénticos con los nonmres, el primero queda igual y del segundo se extraen los nombres a medida que salen; impide los autorregalos y se reinicia automáticamente si alguien queda solo. Quien regala va muestra en orden y quien recibe el regalo se elige de forma aleatoria.
        static void AmigoInvisible(string[] nombres, int participantes, string[] asignaciones, string[] parejas, int cantidadParejas, ref bool haSalidoBien)
        {
            if (participantes < 3)//Para evitar que se ejecute con menos de 1 pareja y 1 persona
            {
                Console.Write("\n\t");
                Console.WriteLine("Cantidad mínima habiendo alguna pareja: 3 personas.");
            }
            else
            {
                int participantesRestantes = participantes;//También es necesario duplicar la variable que indica la cantidad
                for (int i = 1; i <= participantes; i++)//Para que se ejecute la cantidad justa de veces
                {
                    int seed = Environment.TickCount;
                    Random rng = new Random();//Elección aleatoria
                    int asignacion = rng.Next(0, participantesRestantes);//Porque indica la posición en el array (-1)
                    bool sonPareja=false;
                    for (int a=0; a<=cantidadParejas;a+=2)//Así se comprueban de 2 en 2
                    {
                        if (((asignaciones[asignacion]== parejas[a])&& (nombres[i - 1]==parejas[a+1]))||((asignaciones[asignacion] == parejas[a+1]) && (nombres[i - 1] == parejas[a])))//Comprobación de no ser pareja
                        {
                            sonPareja = true;
                        }
                    }
                    if (i == participantes)
                    {
                        if ((asignaciones[asignacion] != nombres[i - 1]) && (sonPareja == false))//Así se evita que el nombre sea el mismo o sean pareja
                        {
                            participantesRestantes--;//Para que se corresponda con el array secundario
                            Console.Write("\n\t");
                            Console.WriteLine(nombres[i - 1] + " debe regalar a " + asignaciones[asignacion]);//Se muestra antes de eliminarlo del array
                            for (int a = asignacion; a < participantes; a++)
                            {
                                asignaciones[a] = asignaciones[a + 1];
                            }

                            haSalidoBien = true;
                        }
                        else
                        {
                            Console.Clear();
                            haSalidoBien = false;
                        }
                    }
                    else 
                    { 
                        if ((asignaciones[asignacion] != nombres[i - 1])&&(sonPareja==false))//Así se evita que el nombre sea el mismo o sean pareja
                        {
                            participantesRestantes--;//Para que se corresponda con el array secundario
                            Console.Write("\n\t");
                            Console.WriteLine(nombres[i - 1] + " debe regalar a " + asignaciones[asignacion]);//Se muestra antes de eliminarlo del array
                            for (int a = asignacion; a < participantes; a++)
                            {
                                asignaciones[a] = asignaciones[a + 1];
                            }
                        }
                        else
                        {
                            i--;//Para permitir que el bucle se repita correctamente al entrar por aquí
                        }

                    }
                }
            }
            Console.ReadLine();//Para que dé tiempo a leerlo
        }



        








        //En este caso, se eliminan de los dos arrays a medida que salen, la persona que se muestra como que recibe un regalo em la última vuelta, será quien regale en la siguiente y las únicas elecciones no aleatorias son la pprimera persona en regalar y la última en recibir que son la primera persona de la lista convirtiéndolo en un ciclo.
        static void AmigoInvisibleCircularCon(string[] nombres, int participantes, string[] asignaciones, string[] parejas, int cantidadParejas)
        {
            if (participantes < 3)//Para evitar que se ejecute con menos de 1 pareja y 1 persona
            {
                Console.WriteLine("Cantidad mínima habiendo alguna pareja: 3 personas.");
            }
            else
            {
                bool nombresRepetidos = false;
                int asignacion = 0;//Para asegurar que regala el primero en la primera vuelta
                int participantesRestantes = participantes;//=Básico
                string regalador = nombres[0];//Regalador es la persona que regala//Para asegurar que regale el primero en la primera vuelta
                string regalado = "";//Regalado es la persona que recibe el regalo
                for (int i = 0; i < participantes; i++)//=Básico
                {
                    if (nombresRepetidos == false)//Para que no escriba si el número aleatorio escigido correspondía con la misma persona pero creé otro número aleatorio
                    {
                        Console.Write("\n\t");
                        Console.Write(regalador);//Para que se escriba antes de ser eliminado
                    }
                    Random rng = new Random();//=básico
                    asignacion = rng.Next(1, participantesRestantes);//A partir de 1 para que el de la primera posición no pueda ser escigido
                    regalado = asignaciones[asignacion];// Para elegir aleatoriamente quien recibe
                    bool sonPareja = false;
                    for (int a=0; a<=cantidadParejas;a+=2)//=básico
                    {
                        if (((regalador == parejas[a])&& (regalado == parejas[a+1]))||((regalador == parejas[a+1]) && (regalado == parejas[a])))//=básico
                        {
                            sonPareja = true;
                        }
                    }
                    if ((regalador != regalado)&& (sonPareja == false))//=básico
                    {
                        if (participantesRestantes <= 1)//Para evitar que pase en la última vuelta y reciba el regalo el primero
                        {
                            Console.WriteLine(" debe regalar a " + nombres[0]);//Para que escoja a la primera persona en la última vuelta
                        }
                        else
                        {
                            participantesRestantes--;//=Básico
                            Console.WriteLine(" debe regalar a " + asignaciones[asignacion]);//=básico
                            regalador = regalado;//Para que al repetirse el bucle la persona que regala sea la misma que la última que recibió el regalo
                            for (int a = asignacion; a < participantes; a++)//=básico
                            {
                                asignaciones[a] = asignaciones[a + 1];//=básico
                            }
                            nombresRepetidos = false;//Para que se repita naturalmente
                        }
                    }
                    else
                    {
                        i--;//=básico
                        nombresRepetidos = true;//Para que se repita sin escribir nada por pantalla
                    }
                }
            }
            Console.ReadLine();//=básico
        }
    }
}
