using System;
using System.Data;

namespace Proyecto2Progra3.Clases
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Dni { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public char Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public char EstadoCivil { get; set; }
        public byte[] Imagen { get; set; }
    }
}