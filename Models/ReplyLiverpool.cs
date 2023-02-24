using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_status_liverpool.Models
{
    public class ReplyLiverpool
    {
        public string tipo_respuesta { get; set; }
        public string respuesta { get; set; }
        public string tiempo_respuesta { get; set; }
        public string id_peticion { get; set; }
        public int status { get; set; }
        public string empresa { get; set; }
        public string mensajeria { get; set; }
        public string referencia { get; set; }
        public string existeNotificacion { get; set; }  
        public string etiquetas { get; set; }
        public string date { get;set; }
    }
}