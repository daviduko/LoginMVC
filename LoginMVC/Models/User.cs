﻿using System.Globalization;

namespace LoginMVC.Models
{
    public class User
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set;}
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }

    }
}
