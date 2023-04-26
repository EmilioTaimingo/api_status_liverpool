using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using api_status_liverpool.Models;
namespace api_status_liverpool.Context
{
    public class GuiasCommands : DBContext
    {
        public Guias Muestra_GuiasMod(string guia)//muestra todos los registros activos
        {
            List<Guias> lGUias = new List<Guias>();
            string connectionString = $"server ={GetRDSConections().Writer}; {Data_base}";

            // Utiliza dispose al finalizar bloque
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                // Comandos
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "muestra_guiasmod_sp";
                //parametros
                cmd.Parameters.AddWithValue("guiguia", guia);
                conexion.Open();
                var leer = cmd.ExecuteReader();
                Guias g = new Guias();
                while (leer.Read())
                {
                    g.GuiaID = leer.GetInt32("gui_id");
                    g.Guia = leer["gui_guia"].ToString();
                    g.FechaCreacion = Convert.ToDateTime(leer["gui_fechacreacion"].ToString());
                    g.Medida = leer["gui_medida"].ToString();
                    g.Peso = Convert.ToDouble(leer["gui_peso"].ToString());
                    g.Descripcion = leer["gui_descripcion"].ToString();
                    g.Url = leer["gui_url"].ToString();
                    g.Destinatario = leer["Nombre_Destinatario"].ToString();
                    g.DireccionDestinatario = leer["Direccion_Destinatario"].ToString();
                    g.Cliente_RZ = leer["cli_razonsocial"].ToString();
                    g.ZonaDes = leer["zon_descripcion"].ToString();
                    g.Fecha = Convert.ToDateTime(leer["gui_fechacreacion"].ToString()).ToString("dddd dd 'de' MMMM 'de' yyyy");
                    g.ClienteID = Convert.ToInt32(leer["cli_id"]);
                    g.Identificador = leer["gui_identificador"].ToString();
                    g.Instrucciones = leer["gui_instrucciones"].ToString();
                }
                conexion.Close();//cierra conexion
                leer.Close();//cierra lista
                return g;//regresa la lista con datos
            }
        }

        public ShopifyModel Muestra_ShopifyMod(string tienda)
        {
            string connectionString = $"server ={GetRDSConections().Reader}; {Data_base}";

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "muestra_shopifymod_sp";
                cmd.Parameters.AddWithValue("shotienda", tienda);
                conexion.Open();
                var leer = cmd.ExecuteReader();
                ShopifyModel shopify = new ShopifyModel();
                while (leer.Read())
                {
                    shopify.ShopifyID = leer.GetInt32("sho_id");
                    shopify.Key = leer["sho_appkey"].ToString();
                    shopify.Token = leer["sho_token"].ToString();
                    shopify.ShopifyID = leer.GetInt32("sho_id");
                    shopify.FechaAlta = leer["sho_fechaalta"].ToString();
                    shopify.Tienda = leer["sho_tienda"].ToString();
                    shopify.ClienteID = leer.GetInt32("cli_id");
                    shopify.Instalada = Convert.ToBoolean(leer.GetInt32("sho_instalada"));
                    shopify.Razon_Social = leer["cli_razonsocial"].ToString();
                };
                conexion.Close();
                leer.Close();
                return shopify;
            }

        }
    }
}