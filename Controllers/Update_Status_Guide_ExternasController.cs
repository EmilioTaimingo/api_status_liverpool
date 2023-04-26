using api_status_liverpool.Context;
using api_status_liverpool.Models;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace api_status_liverpool.Controllers
{
    public class Update_Status_Guide_ExternasController: ApiController
    {
        public Reply Post([FromBody]DataGuideComment odatos)//valida las credenciales de acceso(usuario,contraseña)
        {  
            var respuesta=new Reply();
            ValidaGuia oValidaTipoGuia=new ValidaGuia();
            SendEstatusCommand oStatus=new SendEstatusCommand();
            ShopifyModel _shopifyModel = new ShopifyModel();
            Guias _guias = new Guias();
            GuiasCommands _guiasCommands = new GuiasCommands();
            var datos = oValidaTipoGuia.Valida_Guia(odatos.Guide);
            if (datos.Tipo_Guia == 2)
            {
                //cambiamos el estatus 
                var respuestaLiverpool = oStatus.Cambia_Status(odatos.Guide, datos.Identificador, odatos.Status_Code, odatos.Comment);
                if (respuestaLiverpool.tipo_respuesta == "OK")
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
            else if (datos.Tipo_Guia == 3)
            {
                _guias = _guiasCommands.Muestra_GuiasMod(odatos.Guide);
                _shopifyModel = _guiasCommands.Muestra_ShopifyMod(_guias.Instrucciones);
                var credentials = new NetworkCredential(_shopifyModel.Key, _shopifyModel.Token);
                var handler = new HttpClientHandler { Credentials = credentials, UseCookies = false };
                using (var client = new HttpClient(handler))
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://" + _shopifyModel.Key + ":" + _shopifyModel.Token + "@" + _shopifyModel.Tienda + ".myshopify.com/admin/api/2022-10/orders/" + _guias.Identificador + "/close.json"))
                    {
                        var httpResponse = client.SendAsync(request).Result;
                        var PedidosResponse = httpResponse.Content.ReadAsStringAsync().Result;

                        if (httpResponse.IsSuccessStatusCode)
                        {
                            respuesta.type_Reply_Liverpool = "Shopify";
                            respuesta.Message = "OK";
                            respuesta.Result = 200;
                        }
                        else
                        {
                            respuesta.type_Reply_Liverpool = "Shopify";
                            respuesta.Message = "La guia ingresada no existe";
                            respuesta.Result = 404;
                        }
                    }

                }
            }
            else if (datos.Cliente_Id == 0)
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