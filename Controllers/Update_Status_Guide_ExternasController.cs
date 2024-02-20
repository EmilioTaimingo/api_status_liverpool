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
using System.Net.Http.Headers;
using Org.BouncyCastle.Utilities;
using Newtonsoft.Json.Linq;

namespace api_status_liverpool.Controllers
{
    
    //TIPOS DE GUIAS
    //2 PERTENECE A FANTASY INTERLAIN DE MIRAKL
    //3 SE REFIERE AL CONSUMO DE SHOPIFY PARA AGREGAR LA GUIA CUANDO ESTA YA FUE ENTREGADA
    //6 SE REFIERE A LIVERPOOL DEL PROYECTO ACCESS PACK Y CONSUME LOS MISMOS ESTATUS QUE FANTASY
    public class Update_Status_Guide_ExternasController: ApiController
    {
        public Reply Post([FromBody]DataGuideComment odatos)//valida las credenciales de acceso(usuario,contraseña)
        {  
          Logs ObjLog = new Logs();
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
            } else if (datos.Tipo_Guia == 6)
            {


                string str = datos.Identificador;
                

                string[] tokens = str.Split('-');
                string prueba =tokens[0];

              string guia  = tokens[0].Replace(" ", String.Empty);
                var handler = new HttpClientHandler();
                var fecha1 = DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss");
                var fecha2 = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                using (var httpClient = new HttpClient(handler))
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://apigee-pro.liverpool.com.mx/liverpool/4pl/api/estatus"))
                    {
                        request.Headers.TryAddWithoutValidation("Apikey","kFGGSVW3R8jO9h7HjbFaR2kX7JFB5A9URRFpoNESBNXh2Es2");
                        request.Content = new StringContent("{\n\"thirdPL\":\"ACP\",\n\"tn_reference\":\""+tokens[0]+"\",\n\"estimated_delivery_date\":\""+fecha1+"\",\n\"tracking_number\":\""+odatos.Guide+"\",\n\"code\":\""+odatos.Status_Code+"\",\n\"commen\":\"Ok\",\n\"date\":\""+fecha2+"\"\n}");
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                         
                        var response = httpClient.SendAsync(request).Result;
                        var res = response.Content.ReadAsStringAsync().Result;

                        JObject respuestaObjeto = JObject.Parse(res);
                        ObjLog.Alta_logs(0, res, "api_status_liverpool-"+ odatos.Status_Code);
                    
                    if (response.IsSuccessStatusCode)
                        {
                            respuesta.type_Reply_Liverpool = "Liverpool AccessPack";
                            respuesta.Message = "OK";
                            respuesta.Result = 200;
                        }
                        else
                        {
                            respuesta.type_Reply_Liverpool = "Liverpool AccessPack";
                            respuesta.Message = "Error de alta de estatus";
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
