using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace api_status_liverpool.Models
{
    public class Guias
    {
        //guias
        // numero con el que llega el paquete
        public string NoPedido { get; set; }// agregamos el numero de pedido 
        public int GuiaID { get; set; }
        [Display(Name = "Numero de Guia:")]
        public string Guia { get; set; }
        [Display(Name = "Fecha de Creacion:")]
        public DateTime FechaCreacion { get; set; }
        public string Medida { get; set; }
        public double Peso { get; set; }
        public string Descripcion { get; set; }
        public string Instrucciones { get; set; }
        public string Url { get; set; }
        public int DestinatarioID { get; set; }
        public int ClienteID { get; set; }
        public string Identificador { get; set; }
        public int UsuarioID { get; set; }
        public int? DeliveryID { get; set; }
        public bool Tipo_Guia { get; set; }
        public string CodigoPostal { get; set; }
        public int ZonaId { get; set; }
        public int PaqId { get; set; }  //se agrega propiedad Paquete Id
        [Display(Name = "Nombre de Destinatario:")]
        public string Destinatario { get; set; }
        [Display(Name = "Direccion de Destinatario:")]
        public string DireccionDestinatario { get; set; }
        [Display(Name = "Remitente:")]
        public string Cliente_RZ { get; set; }
        public string ZonaDes { get; set; }

        [Display(Name = "Fecha de Creacion:")]
        public string Fecha { get; set; }
        public int ID_Temporal { get; set; }
        public int T_Guia { get; set; }
    }
}