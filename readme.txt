README - Sistema de Gestión para la Bodega "Doña Cintia"
Descripción

Este proyecto corresponde a una aplicación de consola desarrollada en C# para la gestión de una bodega. El sistema permite registrar trabajadores, autenticar usuarios mediante un login, diferenciar permisos según el rol (Administrador o Empleado) y realizar operaciones relacionadas con inventario, ventas, contabilidad y evaluación del desempeño de los trabajadores.

Requisitos

Antes de ejecutar el programa, asegúrese de contar con alguno de los siguientes entornos:

.NET SDK 6.0 o superior.
Visual Studio 2022 (o una versión compatible con proyectos C#).
Sistema operativo Windows, Linux o macOS con soporte para .NET.
Dependencias

El proyecto utiliza únicamente bibliotecas incluidas en .NET, entre ellas:

System
System.Collections.Generic
System.IO
System.Linq

No requiere instalar paquetes externos mediante NuGet.

Cómo ejecutar el programa
Opción 1: Visual Studio
Abra la solución o el proyecto en Visual Studio.
Compile el proyecto (Compilar → Compilar solución).
Presione F5 o haga clic en Iniciar para ejecutar la aplicación.
Opción 2: Línea de comandos
Abra una terminal en la carpeta del proyecto.
Ejecute el siguiente comando:
dotnet run
Archivos generados

Durante la ejecución, el sistema puede crear automáticamente los siguientes archivos de texto:

productos.txt → Información del inventario.
ventas.txt → Registro de ventas mensuales.
ganancia.txt → Ganancia neta calculada.

Estos archivos se almacenan en el mismo directorio donde se ejecuta el programa.

Usuarios

Al iniciar el programa, primero se registran los trabajadores indicando:

Nombre de usuario.
Rol (Administrador o Empleado).
Contraseña.

Posteriormente, cualquiera de los trabajadores registrados podrá iniciar sesión utilizando las credenciales creadas durante esa ejecución.

Funcionalidades principales
Administrador
Gestionar inventario.
Registrar productos.
Agregar o eliminar productos.
Calcular ganancias.
Asignar salarios.
Evaluar el desempeño de los trabajadores.
Empleado
Registrar ventas mensuales.
Consultar rendimiento.
Registrar el stock restante al finalizar la jornada.
Observaciones

Actualmente el sistema almacena la información utilizando listas en memoria y archivos de texto (.txt). Los trabajadores registrados no permanecen almacenados después de cerrar el programa, por lo que deberán registrarse nuevamente en la siguiente ejecución.