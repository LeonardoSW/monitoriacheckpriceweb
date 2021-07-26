using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MonitoriaCheckPriceWeb.Objects;


namespace MonitoriaCheckPriceWeb.Calls
{
    public class callHubApi
    {
        private static StreamWriter txt = new StreamWriter(@"C:\monitoria\monitoria " + DateTime.UtcNow.ToString("MM-yyyy HH-mm-ss")+".txt",true);
        string tenant = "";
        public async Task getCheckPrice(string call, HttpClient client, string tenantok)
        {
            
            bool ok = true;
            var stringTesk = client.GetStreamAsync(call);
            var objJson = await JsonSerializer.DeserializeAsync<List<objectJsonCheckPrince>>(await stringTesk);

            try
            {
                if (tenant != tenantok)
                {
                    tenant = tenantok;
                    txt.WriteLine($"- - - - - - - - - -\nTenant: {tenantok}\n- - - - - - - - - - ");
                }

                string system = call.Substring(call.IndexOf("?") + 1);
                txt.WriteLine(system + "\n");

                foreach (var json in objJson)
                {
                    if (json.situacao == "revisar")
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(json.situacao_preco))
                    {
                        json.situacao_preco = "";
                    }

                    if (json.situacao.Contains("diferente") || json.situacao_preco.Contains("diferente"))
                    {
                        ok = false;
                        Console.WriteLine("[Divergênte]");
                        txt.WriteLine("SKU: " + json.sku + ";\n Estoque: " + json.situacao + ";\n Preço: " + json.situacao_preco);
                    }
                }
                if (ok == true)
                {
                    txt.WriteLine("Status OK!\nSem divergências.");
                    Console.WriteLine("[Sem divergências]");
                }
                txt.WriteLine("\n - - - - - - - -\n");
                txt.Flush();
            }
            catch(Exception)
            { Console.WriteLine("Tempo excedido");}
        }
    }


}
