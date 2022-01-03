using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Json;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string board = "[[\"HR\", \"HKn\", \"HB\", \"HK\", \"HQ\", \"HB\", \"HKn\", \"HR\"], [\"HP\", \"HP\", \"HP\", \"HP\", \"HP\", \"HP\", \"HP\", \"HP\"], [],[],[],[],[\"BP\", \"BP\", \"BP\", \"BP\", \"BP\", \"BP\", \"BP\", \"BP\"],[\"BR\", \"BKn\", \"BB\", \"BQ\", \"BK\", \"BB\", \"BKn\", \"BR\"]]";
            IEnumerable<IEnumerable<string>> b = JsonSerializer.Deserialize<List<List<string>>>(board);


            foreach (IEnumerable<string> lst in b) {
                foreach (string s in lst) {
                    Console.WriteLine(s);   
                }
            }

            JsonArray array = JsonSerializer.Deserialize<JsonArray>(board);
            Console.WriteLine(array.ToString());
        }
    }
}
