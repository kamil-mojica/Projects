using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using Respiri_Exam_Console_App.Models;
using Newtonsoft.Json;

namespace Respiri_Exam_Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient cons = new HttpClient();
            cons.BaseAddress = new Uri("https://localhost:7125");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            ExecuteApi(cons).Wait();

        }

        static async Task ExecuteApi(HttpClient cons)
        {
            try
            {
                using (cons)
                {
                    HttpResponseMessage helloWorldResponse = await cons.GetAsync("/api/Person/GetVersionNumber");
                    if (helloWorldResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine("A get method that returns a string with an hello world message and a version number:");
                        Console.WriteLine(await helloWorldResponse.Content.ReadAsStringAsync());
                    }

                    HttpResponseMessage getPersonListResponse = await cons.GetAsync("/api/Person/GetPersonList");
                    if (getPersonListResponse.IsSuccessStatusCode)
                    {
                        var personList = JsonConvert.DeserializeObject<List<Person>>(await getPersonListResponse.Content.ReadAsStringAsync());
                        Console.WriteLine("A get method that returns a list of Person objects serialized to JSON:");
                        Console.WriteLine(JsonConvert.SerializeObject(personList));
                    }

                    Console.WriteLine("A get by id method that returns a Person object using a unique id:");
                    Console.Write("Person Id: ");
                    var id = Console.ReadLine();
                    HttpResponseMessage getPersonByIdResponse = await cons.GetAsync($"/api/Person/GetPersonById?id={id}");
                    if (getPersonByIdResponse.IsSuccessStatusCode)
                    {
                        var person = JsonConvert.DeserializeObject<Person>(await getPersonByIdResponse.Content.ReadAsStringAsync());
                        
                        Console.WriteLine(JsonConvert.SerializeObject(person));
                        Console.ReadLine();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
