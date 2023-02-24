using api_status_liverpool.Context;
using api_status_liverpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace api_status_liverpool.Controllers
{
    public class Update_Status_Guide_LiverpoolController: ApiController
    {
        public Reply Post([FromBody]DataGuideComment odatos)//valida las credenciales de acceso(usuario,contraseña)
        {  
            var respuesta=new Reply();
            ValidaGuia oValidaTipoGuia=new ValidaGuia();
            SendEstatusCommand oStatus=new SendEstatusCommand();
            var datos = oValidaTipoGuia.Valida_Guia(odatos.Guide);
            if(datos.Tipo_Guia==2)
            {
                //cambiamos el estatus 
                var respuestaLiverpool= oStatus.Cambia_Status(odatos.Guide,datos.Identificador,odatos.Status_Code,odatos.Comment);
                if(respuestaLiverpool.tipo_respuesta=="OK")
                {
                    respuesta.Result = 200;
                    respuesta.Message = respuestaLiverpool.respuesta;
                    respuesta.type_Reply_Liverpool = respuestaLiverpool.tipo_respuesta;
                }
                else
                {
                    respuesta.Result = 400;
                    respuesta.Message = respuestaLiverpool.respuesta;
                    respuesta.type_Reply_Liverpool = respuestaLiverpool.tipo_respuesta;
                }
                
                
            }
            else if(datos.Cliente_Id==0)
            {
                respuesta.Message = "La guia ingresada no existe";
                respuesta.Result = 404;
            }
            else if (datos.Cliente_Id != 0)
            {
                respuesta.Message = "La guia ingresada no pertenece a Liverpool";
                respuesta.Result = 200;
            }

            return respuesta;
        }
    }
}