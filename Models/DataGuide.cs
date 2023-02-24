using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_status_liverpool.Models
{
    public class DataGuide
    {
        public string thirdPl { get; set; }// por default 036
        public string tn_reference { get; set; }
        public string estimated_delivery_date { get; set; }
        public string tracking_number { get; set; }
        public string code { get; set; }
        public string commen { get; set; }
        public string date { get; set; }
        public string id_solicitante { get; set; }
        public string new_coord { get; set; }
        public string motivo { get; set; }
        public string user_autoriza { get; set; }
        public guiaImg guiaImg { get; set; }
    }
}