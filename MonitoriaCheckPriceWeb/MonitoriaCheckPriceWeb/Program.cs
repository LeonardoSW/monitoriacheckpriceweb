using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MonitoriaCheckPriceWeb.Calls;
using System.IO;

namespace MonitoriaCheckPriceWeb
{
    public class Program
    {
        public static callHubApi getInfo;
        public static List<string> urls = new List<string>() {"http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1450?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1450?System=centauro",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1708?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1708?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1708?System=madeiramadeira",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1739?System=mobly",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1739?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1739?System=madeiramadeira",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1802?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1802?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1802?System=kabum",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1802?System=shopee",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1928?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/1928?System=amazon",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2042?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2102?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2138?System=amazon",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2138?System=ativostore",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2163?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2163?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2163?System=leroymerlin",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2163?System=kabum",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2163?System=shopee",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2286?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2286?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2286?System=amazon",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2329?System=dafiti",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2331?System=centauro",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2351?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2355?System=amazon",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2355?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2355?System=shopee",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2359?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2359?System=dafiti",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2359?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2361?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2361?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2361?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2361?System=kabum",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2368?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2368?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2368?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2368?System=centauro",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2368?System=gpa",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2368?System=shopee",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2374?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2374?System=woocommerce",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2374?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2374?System=leroymerlin",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2374?System=kabum",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2374?System=shopee",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2388?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2417?System=amazon",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2417?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2418?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2418?System=woocommerce",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2418?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2418?System=enjoei",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2418?System=shopee",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2439?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2439?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2439?System=gpa",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2444?System=amazon",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2444?System=dafiti",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2444?System=centauro",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2446?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2446?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2446?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2446?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2446?System=gpa",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2479?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2479?System=luxuryloyalty",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2484?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2484?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2484?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2484?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2491?System=leroymerlin",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2493?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2493?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2493?System=madeiramadeira",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2493?System=shopee",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2497?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2497?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2497?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2497?System=gpa",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2524?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2524?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2524?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2524?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2524?System=kabum",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2535?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2535?System=kabum",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2542?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2542?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2542?System=woocommerce",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2542?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2542?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2542?System=madeiramadeira",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2542?System=shopee",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2653?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2653?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2653?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2709?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2709?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2709?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2733?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2733?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2733?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2733?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2934?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2934?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2934?System=dafiti",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2934?System=carrefour",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2934?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2934?System=centauro",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/2934?System=madeiramadeira",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3021?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3021?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3021?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3021?System=madeiramadeira",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3038?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3063?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3063?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3063?System=madeiramadeira",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3063?System=gpa",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3146?System=dafiti",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3146?System=centauro",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3173?System=luxuryloyalty",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3228?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3451?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3451?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3451?System=mobly",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3451?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3451?System=leroymerlin",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3451?System=madeiramadeira",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3451?System=gpa",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3451?System=kabum",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3472?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3472?System=gpa",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3588?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3867?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3867?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3867?System=madeiramadeira",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3884?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/3884?System=magazineluiza",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/4313?System=cnova",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/4313?System=mercadolivre",
                                                    "http://eb-api.plataformahub.com.br/RestServiceImpl.svc/CheckPriceStock/4313?System=shopee"};

        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Processo de monitoria iniciado.");
        StreamWriter txt = new StreamWriter(@"C:\monitoria\ErrosMonitoria" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + ".txt", true);
        getInfo = new callHubApi();
        client.Timeout = TimeSpan.FromMinutes(3);
            Console.WriteLine("Processando:");

            foreach (string call in urls)
            {
                int pos = (call.IndexOf("?") - 4);
                try
                {
                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss - ")+call+"...");
                    string tenantOk = call.Substring(pos, 4);
                    await getInfo.getCheckPrice(call,client,tenantOk);
                }
                catch (Exception e)
                {
                    Console.Write("error");
                    string tenantErro = call.Substring(pos);
                    txt.WriteLine("Tenant/Canal: " + tenantErro + "\n");
                    txt.WriteLine("Falha - " + DateTime.Now.ToString() + "\nErro: " + e.ToString());
                    txt.WriteLine("\n - - - - - - ");
                }
            }
            Console.WriteLine("-----------------------------------------------------------------\nProcesso finalizado\n-----------------------------------------------------------------");
        }
    }
}
