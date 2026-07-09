using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Codigo_para_el_proyecto_final__U_
{
    public class productos
    {
        public string nombre_producto { get; set; }
        public int cantidad_inicio { get; set; }
        public int cantidad_final { get; set; }
        public decimal precio_venta { get; set; }
        public double precio_compra { get; set; }
    }
    public enum Rol
    {
        Administrador,
        Empleado
    }
    public class workers
    {
        public string name { get; set; }
        public Rol Rol { get; set; }
        public string password { get; set; }
        public int sells { get; set; }
        public int mistakes { get; set; }
        public int score { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== SISTEMA DE LOGIN ==========\n");

            List<workers> trabajadores = new List<workers>();

            Console.Write("Ingrese el número de trabajadores: ");
            int numeroDeTrabajadores = int.Parse(Console.ReadLine());

            // Registro de trabajadores
            for (int i = 0; i < numeroDeTrabajadores; i++)
            {
                workers trabajador = new workers();

                Console.WriteLine($"\nTrabajador #{i + 1}");

                Console.Write("Nombre: ");
                trabajador.name = Console.ReadLine();

                Rol rolIngresado;

                Console.Write("Rol (Administrador/Empleado): ");

                while (!Enum.TryParse(Console.ReadLine(), true, out rolIngresado))
                {
                    Console.WriteLine("Rol inválido.");
                    Console.Write("Ingrese nuevamente: ");
                }

                trabajador.Rol = rolIngresado;

                Console.Write("Contraseña: ");
                trabajador.password = Console.ReadLine();

                trabajadores.Add(trabajador);
            }

            // Mostrar trabajadores registrados
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"========== TRABAJADORES REGISTRADOS ==========");
            Console.ResetColor();


            foreach (workers trabajador in trabajadores)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"Nombre: {trabajador.name}");
                Console.WriteLine($"Rol: {trabajador.Rol}");
                Console.WriteLine($"Contraseña: {trabajador.password}");
            }
            Console.WriteLine("\nRegistro completado correctamente.");
            Console.Clear();

            // LOGIN
            char continuarSistema;
            do
            {
                Console.WriteLine("\n========== LOGIN ==========");

                bool acceso = false;
                workers usuarioLogueado = null;

                for (int intento = 1; intento <= 3; intento++)
                {
                    Console.Write("\nUsuario: ");
                    string usuario = Console.ReadLine();

                    Console.Write("Contraseña: ");
                    string contraseña = Console.ReadLine();

                    foreach (workers trabajador in trabajadores)
                    {
                        if (trabajador.name == usuario &&
                            trabajador.password == contraseña)
                        {
                            usuarioLogueado = trabajador;
                            acceso = true;
                            break;
                        }
                    }

                    if (acceso)
                        break;

                    Console.WriteLine("Usuario o contraseña incorrectos.");
                    Console.WriteLine($"Intento {intento} de 3.");
                }

                if (!acceso)
                {
                    Console.WriteLine("\nDemasiados intentos fallidos.");
                    Console.WriteLine("Acceso denegado.");
                }
                else
                {
                    // Bienvenida
                    Console.WriteLine($"\nBienvenido {usuarioLogueado.name}");
                    Console.WriteLine($"Rol detectado: {usuarioLogueado.Rol}");

                    // Menú según el rol
                    switch (usuarioLogueado.Rol)
                    {
                        case Rol.Administrador:
                            Console.WriteLine("\n===== MENÚ ADMINISTRADOR =====");
                            sistema_administradores(trabajadores.Count, trabajadores);
                            break;

                        case Rol.Empleado:
                            Console.WriteLine("\n===== MENÚ EMPLEADO =====");
                            sistema_vendedores(trabajadores.Count, trabajadores);
                            break;

                        default:
                            Console.WriteLine("El rol asignado no existe.");
                            break;
                    }
                }

                Console.Write("\n¿Desea volver a ingresar al sistema? (s/n): ");
            }
            while (char.TryParse(Console.ReadLine(), out continuarSistema) && char.ToLower(continuarSistema) == 's');

            Console.WriteLine("\nCerrando el sistema. Presione cualquier tecla para salir...");
            Console.ReadKey();
            //Si el admin nunca ingresa al sistema, ¿la lista de productos existira en caso de que el vendedor ingrese al sistema?
            //No, por lo tanto, el vendedor no podra acceder a la lista de productos, lo que es un gran problema,
            //ya que el vendedor necesita esa informacion para vender los productos.
        }
        static public void sistema_vendedores(int n, List<workers> a)
        {
            Console.WriteLine("Bienvenido al sistema de vendedores:]");
            Console.WriteLine("Ingrese sus credenciales...");
            Console.Write("Nombre: "); string nombre = Console.ReadLine();
            Console.Write("Contraseña: "); string pw = Console.ReadLine();
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].name == nombre)
                {
                    if (a[i].password == pw)
                    {
                        Console.WriteLine($"Bienvenido {nombre}");
                        funciones_vendedores(n, a);
                    }
                }
                else if (a[i].name != nombre)
                {
                    Console.WriteLine("Nombre equivocado");
                    if (a[i].password != pw)
                    {
                        Console.WriteLine("Contraseña incorrecta, cerrando el sistemaa");
                        Console.WriteLine("Credenciales invalidas, tenga buen dia");
                    }
                }
            }
        }
        static public void funciones_vendedores(int n, List<workers> a)
        {
            Console.WriteLine("¿Que funcion desea ejecutar?\n1. Ventas mensuales\n2. Rendimiento\n3. Almacen disponible");
            int reader = int.Parse(Console.ReadLine());
            while (!int.TryParse(Console.ReadLine(), out reader) || reader < 1 && reader > 2) 
            { 
                Console.WriteLine("Opcion invalida, por favor ingrese una opcion valida");
            }
            string RutaArchivo, lector;
            switch (reader)
            {
                case 1:
                    Console.WriteLine("ventas mensuales: ");
                    double[] ventasmensuales = new double[12];
                    for (int i = 0; i < ventasmensuales.Length; i++)
                    {
                        Console.WriteLine($"Venta n° {i + 1}: ");
                        ventasmensuales[i] = double.Parse(Console.ReadLine());
                    }
                    for (int i = 0; i < ventasmensuales.Length; i++)
                    {
                        Console.WriteLine($"Venta n° {i + 1} :" + ventasmensuales[i]);
                    }
                    RutaArchivo = "Venta.txt";
                    string ElementoAGuardar = Convert.ToString(ventasmensuales);
                    File.WriteAllText(RutaArchivo, ElementoAGuardar);
                    break;
                case 2:
                    List<workers> calificador = new List<workers>();
                    for (int i = 0; i < n; i++)
                    {
                        calificador.Add(new workers());
                    }
                    Console.WriteLine("Sistema dedicado al informe de rendimiento de los trabajadres");
                    for (int i = 0; i < n; i++)
                    {
                        Console.WriteLine($"Trabajador n° {i + 1}: {a[i].name}");
                        Console.WriteLine($"ventas: ");
                        calificador[i].sells = int.Parse(Console.ReadLine());
                        Console.WriteLine($"errores: ");
                        calificador[i].mistakes = int.Parse(Console.ReadLine());
                    }
                    Console.WriteLine("Este es el ranking de empleados:");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 1; j < n; j++)
                        {
                            if (calificador[i].sells < calificador[j].sells)
                            {
                                Console.WriteLine($"El trabajar numero {i} es: {calificador[i].name}");
                                Console.WriteLine($"El trabajador numero {j} es: {calificador[j].name}");
                            }
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("Has llegado al final de tu jornada");
                    lector = Console.ReadLine();
                    Console.WriteLine("En orden de concluir con tu dia, registra la cantidad de productos restantes");      
                    List<productos> estante = new List<productos>();
                    Console.WriteLine("Ingrese la cantidad de productos iniciales: "); int lector2 = int.Parse(Console.ReadLine());
                        for (int i = 0; i < lector2; i++)
                        {
                            estante.Add(new productos());
                        }
                        for (int i = 0; i < lector2; i++)
                        {
                            Console.WriteLine("Ingrese el nombre del producto nº " + (i + 1) + ": ");
                            estante[i].nombre_producto = Console.ReadLine();
                            Console.WriteLine("Ingrese la cantidad del producto nº " + (i + 1) + ": ");
                            estante[i].cantidad_inicio = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ingrese el precio de venta del producto nº " + (i + 1) + ": ");
                            estante[i].precio_venta = decimal.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("Ingrese la cantidad de productos restantes: ");
                        for (int i = 0; i < lector2; i++)
                        {
                            Console.WriteLine("Producto nº " + (i + 1) + ": " + estante[i].nombre_producto);
                            Console.WriteLine("Cantidad: "); estante[i].cantidad_final = int.Parse(Console.ReadLine());
                            Console.WriteLine("Precio de venta: " + estante[i].precio_venta);
                            int cantidad_vendida = estante[i].cantidad_inicio - estante[i].cantidad_final;
                            int venta_producto = cantidad_vendida * (int)estante[i].precio_venta;
                            Console.WriteLine($"Cantidad vendida del producto {estante[i].nombre_producto}: {cantidad_vendida}");
                            int venta_total = estante.Sum(p => (p.cantidad_inicio - p.cantidad_final) * (int)p.precio_venta);
                        }
                        Console.WriteLine($"La venta total de todos los productos es: {estante.Sum(p => (p.cantidad_inicio - p.cantidad_final) * (int)p.precio_venta)}");
                    break;
            }
        }
        static public void sistema_administradores(int n, List<workers> a)
        {
            Console.Clear();
            Console.WriteLine("Bienvenido al sistema de administradores...");
            Console.WriteLine("Esta es su lista de trabajadores registrados...");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("trabajador nº " + (i + 1)  + ": " + a[i].name);
                Console.WriteLine("Cargo: " + a[i].Rol);
                Console.WriteLine("contraseña: " + a[i].password);
            }

            string entrada;
            char reader = '\0';
            do
            {
                Console.Write("¿Desea ejecutar alguna acción extra? (s/n): ");
                entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Debe ingresar una opción.");
                    continue;
                }

                reader = char.ToLower(entrada[0]);

                if (reader != 's' && reader != 'n' || !char.TryParse(Console.ReadLine(), out reader))
                {
                    Console.WriteLine("Opción inválida.");
                }

            } while (reader != 's' && reader != 'n');
            if (reader == 's')
            {
                Console.Clear();
                Console.WriteLine("¿Que operacion desea ejecutar?");
                Console.WriteLine("1. Gestionar almacen. \n2. Ajuste de recibos. \n3. Asignacion de salarios. \n4. Renovacion de contratos.");
                //Las dos últimas funciones son redundantes y podria simplemente ponerlas en la tercera funcion, igualmente, podria renombrar la funcion como gestion de empleados
                //la idea sera esa, reajustar y en base a la valoracion del empleado, asignar salario, posteriormente y en base a la segunda, considerar quienes se quedan
                // y quienes se van.
                int opcionElegida = int.Parse(Console.ReadLine());
                while (opcionElegida < 1 && opcionElegida > 4 || !int.TryParse(Console.ReadLine(), out opcionElegida)) 
                {
                    Console.WriteLine("Ingrese una opcion valida y dentro de los parametros");
                }
                Console.Clear();
                funciones_administrador(opcionElegida, a);
            }
            else if (reader == 'n')
            {
                Console.Clear();
                Console.WriteLine("Cerrando el sistema, presione enter para cerrar la consola...");
                Console.ReadKey();
            }
        }
        static public void funciones_administrador(int n, List<workers> a) 
        {
            string rutaArchivo;
            string extractor;
            switch (n)
            {
                case 1:
                    Console.WriteLine("Indicar la cantidad de productos a su disposicion: ");
                    int numeroproductos = int.Parse(Console.ReadLine());
                    List<productos> estante = new List<productos>();
                    for (int i = 0; i < numeroproductos; i++)
                    {
                        estante.Add(new productos());
                    }
                    for (int i = 0; i < numeroproductos; i++)
                    {
                        Console.WriteLine("Ingrese el nombre del producto nº " + (i + 1) + ": ");
                        estante[i].nombre_producto = Console.ReadLine();
                        while (string.IsNullOrWhiteSpace(estante[i].nombre_producto) || !int.TryParse(Console.ReadLine(), out estante.nombre_producto))
                        {
                            Console.WriteLine("El nombre del producto no puede estar vacío. Por favor, ingrese un nombre válido.");
                            estante[i].nombre_producto = Console.ReadLine();
                        }
                        Console.WriteLine("Ingrese la cantidad del producto nº " + (i + 1) + ": ");
                        estante[i].cantidad_inicio = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese el precio de venta del producto nº " + (i + 1) + ": ");
                        estante[i].precio_venta = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese el precio de compra del producto nº " + (i + 1) + ": ");
                        estante[i].precio_compra = double.Parse(Console.ReadLine());
                    }
                    for (int i = 0; i < numeroproductos; i++)
                    {
                        Console.WriteLine("Producto nº " + (i + 1) + ": " + estante[i].nombre_producto);
                        Console.WriteLine("Cantidad: " + estante[i].cantidad_inicio);
                        Console.WriteLine("Precio de venta: " + estante[i].precio_venta);
                        Console.WriteLine("Precio de compra: " + estante[i].precio_compra);
                    }
                    Console.ReadKey();
                    Console.WriteLine("¿Desea agregar o eliminar productos? (agregar/eliminar)"); string reader = Console.ReadLine();
                    Console.ReadKey();
                    if (reader == "agregar")
                    {
                        Console.WriteLine("Indique la cantidad de productos a agregar..."); int nuevolargo = int.Parse(Console.ReadLine());
                        for (int i = 0; i < nuevolargo; i++)
                        {
                            estante.Add(new productos());
                        }
                        for (int i = numeroproductos; i < numeroproductos + nuevolargo; i++)
                        {
                            Console.WriteLine("Ingrese el nombre del producto nº " + (i + 1) + ": ");
                            estante[i].nombre_producto = Console.ReadLine();
                            Console.WriteLine("Ingrese la cantidad del producto nº " + (i + 1) + ": ");
                            estante[i].cantidad_inicio = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ingrese el precio de compra del producto nº " + (i + 1) + ": ");
                            estante[i].precio_compra = double.Parse(Console.ReadLine());
                            Console.WriteLine("Ingrese el precio de venta del producto nº " + (i + 1) + ": ");
                            estante[i].precio_venta = decimal.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("Productos agregados exitosamente.");
                        Console.WriteLine("Esta es su nueva lista:");
                        for (int i = 0; i < nuevolargo; i++)
                        {
                            Console.WriteLine("Producto nº " + (i + 1) + ": " + estante[i].nombre_producto);
                            Console.WriteLine("Cantidad: " + estante[i].cantidad_inicio);
                            Console.WriteLine("Precio de venta: " + estante[i].precio_venta);
                        }
                        rutaArchivo = "productos.txt";
                        File.WriteAllLines(rutaArchivo, estante.Select(p => $"{p.nombre_producto},{p.cantidad_inicio},{p.precio_venta},{p.precio_compra}"));
                    }
                    else if (reader == "eliminar")
                    {
                        Console.WriteLine("Indique la cantidad de productos a eliminar..."); int nuevolargo = int.Parse(Console.ReadLine());
                        for (int i = 0; i < nuevolargo; i++)
                        {
                            estante.RemoveAt(estante.Count - 1);
                        }
                        Console.WriteLine("Productos eliminados exitosamente.");
                        Console.WriteLine("Esta es su nueva lista:");
                        for (int i = 0; i < estante.Count; i++)
                        {
                            Console.WriteLine("Producto nº " + (i + 1) + ": " + estante[i].nombre_producto);
                            Console.WriteLine("Cantidad: " + estante[i].cantidad_inicio);
                            Console.WriteLine("Precio de venta: " + estante[i].precio_venta);
                        }
                        rutaArchivo = "productos.txt";
                        File.WriteAllLines(rutaArchivo, estante.Select(p => $"{p.nombre_producto},{p.cantidad_inicio},{p.precio_venta},{p.precio_compra}"));
                    }
                    else
                    {
                        Console.WriteLine("Opcion invalida, cerrando sistema...");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    int uit = 38500;
                    int igv = 18 / 100;
                    Console.WriteLine("Bienvenido al sistema de contabilizaciones");
                    Console.WriteLine("Registre sus ventas...");
                    int[] ventas = new int[4];
                    for (int i = 0; i < ventas.Length; i++)
                    {
                        Console.WriteLine($"ingreso n° {ventas[i + 1]}: ");
                        ventas[i] = int.Parse(Console.ReadLine());
                    }
                    int ventatotal = ventas.Sum();
                    double ventaneta = ventatotal / igv;
                    Console.Write($"Su venta anual es de: {ventatotal}");
                    Console.WriteLine($"Con IGV: {ventaneta}");
                    Console.WriteLine("Ingrese el coste de inventario: "); int costes = int.Parse(Console.ReadLine());
                    double ganancias = ventaneta - costes;
                    if (ganancias < uit)
                    {
                        Console.WriteLine($"Su ganancia es de: {ganancias}");
                    }
                    else
                    {
                        Console.WriteLine("Aplicando UIT");
                        double impuesto = (ganancias - uit) * 8 / 100;
                        Console.WriteLine($"El impuesto a la renta anual seria de {impuesto}");
                        Console.WriteLine($"La ganancia anual seria de {ganancias - impuesto}");
                        Console.ReadKey();
                        rutaArchivo = "ganancia.txt";
                        string ElementoAGuardar = Convert.ToString(ganancias - impuesto);
                        File.WriteAllText(rutaArchivo, ElementoAGuardar);
                    } 
                    break;
                case 3:
                    Console.WriteLine("Nienvenido al sistema de salarios");
                    if (File.Exists(rutaArchivo = "ganancias.txt")) 
                    {
                        extractor = File.ReadAllText("ganancia.txt");
                        double ganancia = Convert.ToDouble(extractor);
                        Console.WriteLine("Estimaremos la cantidad de partes en las que puede dividir sus ganancias entre salarios");
                        Console.WriteLine($"Sueldo anual equitativo: {ganancia / a.Count}");
                    }
                    else 
                    {
                        Console.WriteLine("Ingrese el monto de su ganancia: "); int ganancia = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Sueldo anual equitativo entre todos los empleados: {ganancia / a.Count}");
                    }
                    break;
                case 4:
                    Console.WriteLine("Bienvenido al sistema calificativo de desempeño");
                    Console.WriteLine("¿Cuenta con registro previo del desempeño de la plana? (s/n)");
                    string respuesta = Console.ReadLine();
                    if (respuesta.ToLower() == "s")
                    {
                        Console.WriteLine("Ingrese el nombre del archivo de desempeño: ");
                        string nombreArchivo = Console.ReadLine();
                        if (File.Exists(nombreArchivo))
                        {
                            string[] lineas = File.ReadAllLines(nombreArchivo);
                            foreach (string linea in lineas)
                            {

                            }
                            int contador = lineas.Count();
                            for (int i = 0; i < contador; i++) 
                            { 
                            }
                        }
                        else
                        {
                            Console.WriteLine("El archivo no existe.");
                        }
                    }
                    else if (respuesta.ToLower() == "n")
                    {
                        Console.WriteLine("Ingrese el desempeño de cada trabajador:");
                        for (int i = 0; i < a.Count; i++)
                        {
                            Console.WriteLine($"Desempeño de {a[i].name}: ");
                            a[i].sells = int.Parse(Console.ReadLine());
                            Console.WriteLine($"Errores de {a[i].name}: ");
                            a[i].mistakes = int.Parse(Console.ReadLine());
                        }
                        for (int i = 0; i < a.Count; i++)
                        {
                            Console.WriteLine($"Trabajador: {a[i].name}\nVentas: {a[i].sells} \nErrores: {a[i].mistakes}");
                            int puntaje = a[i].sells - a[i].mistakes;
                            Console.WriteLine($"Puntaje de {a[i].name}: {puntaje}");
                            a[i].score = puntaje;
                        }
                        // calcular puntajes para cada trabajador
                        // ordenar por puntaje descendente e imprimir ranking
                        var puntajesOrdenados = a.OrderByDescending(s => s.score).ToList();
                        for (int j = 0; j < puntajesOrdenados.Count; j++)
                        {
                            Console.WriteLine($"================Trabajador {(j + 1)}================" +
                                $"\nNombre: {puntajesOrdenados[j].name}\nPuntaje: {puntajesOrdenados[j].score}");
                        }

                        // obtener puntaje mínimo y trabajador con peor desempeño (si necesitas ese dato)
                        int minimo = a.Min(w => w.score);
                        var peor = a.FirstOrDefault(w => w.score == minimo);
                        if (peor != null)
                        {
                            Console.WriteLine($"Empleado con peor desempeño: {peor.name} con puntaje {peor.score}");
                            Console.WriteLine("¿Desea eliminarlo de la planilla de trabajadores? (s/n)");
                            respuesta = Console.ReadLine();
                            while (respuesta.ToLower() != "s" && respuesta.ToLower() != "n")
                            {
                                Console.WriteLine("Respuesta inválida. Ingrese 's' para sí o 'n' para no.");
                                respuesta = Console.ReadLine();
                            }
                            if (respuesta.ToLower() == "s")
                            {
                                a.Remove(peor);
                                Console.WriteLine($"Empleado {peor.name} eliminado de la planilla.");
                            }
                            else
                            {
                                Console.WriteLine("No se eliminó ningún empleado.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Respuesta inválida.");
                    }
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }
    }
}
