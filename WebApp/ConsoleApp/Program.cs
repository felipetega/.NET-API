using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp.Services;
using ConsoleApp;

class Program
{
    static async Task Main()
    {
        await ExecuteApiRequestAsync();
    }

    static async Task ExecuteApiRequestAsync()
    {
        ApiService apiService = new ApiService(); // Create an instance of the ApiService
        string apiUrl = "https://localhost:7243/api";
        bool exitRequested = false;

        while (!exitRequested)
        {
            Console.WriteLine("Escolha o método HTTP (GET, POST, PUT, DELETE) ou digite EXIT para sair:");
            string httpMethod = Console.ReadLine()?.ToUpper();

            if (httpMethod != null && httpMethod != "EXIT")
            {
                try
                {
                    HttpResponseMessage response;

                    if (httpMethod == "GET")
                    {
                        response = await apiService.GetAsync(apiUrl);
                    }
                    else if (httpMethod == "POST")
                    {
                        City newCity = GetCityDetails();
                        response = await apiService.PostAsync(apiUrl, newCity);
                    }
                    else if (httpMethod == "PUT")
                    {
                        int id = GetCityId();
                        City updatedCity = GetCityDetails();
                        response = await apiService.PutAsync(apiUrl, id, updatedCity);
                    }
                    else if (httpMethod == "DELETE")
                    {
                        int id = GetCityId();
                        response = await apiService.DeleteAsync(apiUrl, id);
                    }
                    else
                    {
                        Console.WriteLine("Método HTTP inválido.");
                        continue;
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        Console.WriteLine($"Erro na solicitação. Código de status: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na solicitação: {e.Message}");
                }
            }
            else if (httpMethod == "EXIT")
            {
                exitRequested = true;
            }
            else
            {
                Console.WriteLine("Método HTTP inválido.");
            }
        }
    }

    // Helper method to get city details from the user
    static City GetCityDetails()
    {
        Console.WriteLine("Insira o nome para a cidade que deseja criar:");
        string name = Console.ReadLine();

        Console.WriteLine("Insira o nome do estado para a cidade:");
        string stateName = Console.ReadLine();

        return new City(name, stateName);
    }

    // Helper method to get city ID from the user
    static int GetCityId()
    {
        Console.WriteLine("Insira o ID da Cidade:");
        return int.Parse(Console.ReadLine());
    }
}
