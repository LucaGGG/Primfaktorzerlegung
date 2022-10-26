using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Company.Function
{
    public static class Primfaktorzerlegung
    {
        [FunctionName("Primfaktorzerlegung")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string parameter = req.Query["zahl"];

            int zahl = Int32.Parse(parameter);

            int[] primZahlen = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

            var faktoren = new List<int>();

            int letzterFaktor = 0;

            for (int i = 0; primZahlen[i] != zahl; i = i + 0) {
                if (zahl % primZahlen[i] == 0) {
                    zahl = zahl / primZahlen[i];
                   faktoren.Add(primZahlen[i]);
                   letzterFaktor =  primZahlen[i + 1];
                } else {
                    i++;
                }
            }

            if (letzterFaktor != 0) {
                faktoren.Add(zahl);
            }

            string antwort = "";

            foreach (var fak in faktoren) {
                antwort = antwort + fak.ToString() + " "; 
            }

            string responseMessage = antwort;

            return new OkObjectResult(responseMessage);
        }
    }
}
