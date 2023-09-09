// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    //RequestUri = new Uri("https://pokedex2.p.rapidapi.com/pokedex/usa/pikachu"),
    /*Headers =
    {
        { "X-RapidAPI-Key", "8b612b5078msheef33a9277fb744p1dbfddjsne0206765bd88" },
        { "X-RapidAPI-Host", "pokedex2.p.rapidapi.com" },
    },*/
    RequestUri = new Uri("https://pokeapi.co/api/v2/pokemon?limit=100000&offset=0")
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    string body = await response.Content.ReadAsStringAsync();
    Console.WriteLine(body);
    JObject jsonObject = JsonConvert.DeserializeObject<JObject>(body);
    JArray resultsArray = (JArray)jsonObject["results"];

    foreach (JObject result in resultsArray)
    {
        string nombre = (string)result["name"];

        Console.WriteLine($"Nombre: {nombre}");
    }
}

class Ahorcado
{
    static List<string> palabras = new List<string> { "PERRO", "GATO", "CASA", "ARBOL" };
    static Random random = new Random();

    static void Main()
    {
        Console.WriteLine("¡Bienvenido al juego de Ahorcado!");
        string palabraAdivinar = SeleccionarPalabraAleatoria();
        char[] palabraOculta = new char[palabraAdivinar.Length];

        for (int i = 0; i < palabraAdivinar.Length; i++)
        {
            palabraOculta[i] = '_';
        }

        int intentosRestantes = 6;

        while (intentosRestantes > 0)
        {
            Console.WriteLine("\nPalabra: " + new string(palabraOculta));
            Console.WriteLine("Intentos restantes: " + intentosRestantes);

            Console.Write("Ingrese una letra: ");
            char letra = char.ToUpper(Console.ReadKey().KeyChar);

            if (!char.IsLetter(letra))
            {
                Console.WriteLine("\nPor favor, ingrese una letra válida.");
                continue;
            }

            bool acierto = false;
            for (int i = 0; i < palabraAdivinar.Length; i++)
            {
                if (palabraAdivinar[i] == letra)
                {
                    palabraOculta[i] = letra;
                    acierto = true;
                }
            }

            if (!acierto)
            {
                intentosRestantes--;
                Console.WriteLine("\nLetra incorrecta. Intentos restantes: " + intentosRestantes);
            }

            if (new string(palabraOculta) == palabraAdivinar)
            {
                Console.WriteLine("\n¡Felicidades! ¡Has adivinado la palabra: " + palabraAdivinar + "!");
                break;
            }
        }

        if (intentosRestantes == 0)
        {
            Console.WriteLine("\n¡Lo siento! Has agotado tus intentos. La palabra era: " + palabraAdivinar);
        }

        Console.WriteLine("\nPresione cualquier tecla para salir.");
        Console.ReadKey();
    }

    static string SeleccionarPalabraAleatoria()
    {
        int indiceAleatorio = random.Next(palabras.Count);
        return palabras[indiceAleatorio];
    }
}