using System.Net.Http.Headers;
using System.Text;
using Mvc.DataService.Interface;
using Mvc.Models;
using Mvc.Models.Cliente;
using Newtonsoft.Json;

namespace Mvc.DataService;

public class ClienteService : IClienteService
{

    public async Task<IEnumerable<ClienteViewModel>> Get(string token, string? filter = null)
    {

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = "http://localhost:5000/api/Cliente";
            url += filter != null 
                ? $"?filter={filter}" 
                : string.Empty;

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var clientes = await response.Content.ReadFromJsonAsync<List<ClienteViewModel>>();
                if (clientes != null)
                {
                    return clientes;
                }
            }
        }

        throw new Exception("Ocorreu um erro ao recuperar os clientes.");
    }

    public async Task<ClienteViewModel> GetById(Guid id, string token)
    {

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(@"http://localhost:5000/api/Cliente/" + id);
            if (response.IsSuccessStatusCode)
            {
                var cliente = await response.Content.ReadFromJsonAsync<ClienteViewModel>();
                if (cliente != null)
                {
                    return cliente;
                }
            }
        }

        throw new Exception("Ocorreu um erro ao recuperar o cliente.");
    }

    public async Task<GenericResponseViewModel> CreateOrEdit(CreateOrEditClienteViewModel model, string token)
    {
        HttpResponseMessage response;
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var uri = @"http://localhost:5000/api/Cliente/";
            if (model.Id != Guid.Empty)
            {
                uri += model.Id;

            }
            var jsonContent = JsonConvert.SerializeObject(model);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            if (model.Id == Guid.Empty)
            {
                response = await client.PostAsync(uri, contentString);
            }
            else
            {
                response = await client.PutAsync(uri, contentString);
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<GenericResponseViewModel>();
                if (content != null)
                {
                    return content;
                }
            }
        }

        if ((int)response.StatusCode == StatusCodes.Status400BadRequest)
        {
            var genericResponse =
                await response.Content.ReadFromJsonAsync<GenericResponseViewModel>()
                ?? throw new Exception("Não foi possível atualizar o cliente.");

            throw new ArgumentException(genericResponse.Messages.FirstOrDefault());
        }

        throw new Exception("Não foi possível atualizar o cliente.");
    }

    public async Task Delete(Guid id, string token)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.DeleteAsync(@"http://localhost:5000/api/Cliente/" + id);
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        if ((int)response.StatusCode == StatusCodes.Status400BadRequest)
        {
            var error = await response.Content.ReadFromJsonAsync<GenericResponseViewModel>();
            if (error != null)
            {
                throw new ArgumentException(error.Messages.FirstOrDefault());
            }
        }

        throw new Exception("Não foi possível excluir o cliente.");
    }
}