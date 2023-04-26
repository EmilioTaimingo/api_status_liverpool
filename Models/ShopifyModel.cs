using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace api_status_liverpool.Models
{
    public class ShopifyModel
    {
        [Key]
        [Column(Order = 0)]
        public int ShopifyID { get; set; }
        [Display(Name = "Key")]
        public string Key { get; set; }
        [Display(Name = "Token")]
        public string Token { get; set; }
        [Display(Name = "Tienda")]
        public string Tienda { get; set; }
        [Display(Name = "Cliente")]
        public int ClienteID { get; set; }
        [Display(Name = "Fecha de Creacion")]
        public string FechaAlta { get; set; }
        [Display(Name = "FechaBaja")]
        public string FechaBaja { get; set; }
        [Display(Name = "Instalada")]
        public bool Instalada { get; set; }
        public int ID_Temporal { get; set; }
        [Display(Name = "Cliente")]
        public string Razon_Social { get; set; }
    }
}