using Mvc.DataService.Interface;
using Mvc.Models;
using Mvc.Models.Pedido;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Mvc.DataService;
public class PedidoService : IPedidoService
{
    public async Task<IEnumerable<PedidoViewModel>> Get(string token)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(@"http://localhost:5000/api/Pedido");
            if (response.IsSuccessStatusCode)
            {
                var pedidos = await response.Content.ReadFromJsonAsync<List<PedidoViewModel>>();
                if (pedidos != null)
                {
                    return pedidos;
                }
            }
        }

        throw new Exception("Ocorreu um erro ao recuperar os pedidos.");
    }

    public async Task<GetPedidoByIdViewModel> GetById(Guid id, string token)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(@"http://localhost:5000/api/Pedido/" + id);
            if (response.IsSuccessStatusCode)
            {
                var pedido = await response.Content.ReadFromJsonAsync<GetPedidoByIdViewModel>();
                if (pedido != null)
                {
                    return pedido;
                }
            }
        }

        throw new Exception("Ocorreu um erro ao recuperar o pedido.");
    }
    public async Task<GenericResponseViewModel> CreateOrEdit(CreateOrEditPedidoViewModel model, string token)
    {
        HttpResponseMessage response;
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var uri = @"http://localhost:5000/api/Pedido/";
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
            var genericResponse = await response.Content.ReadFromJsonAsync<GenericResponseViewModel>();
            if (genericResponse != null)
            {
                throw new ArgumentException(genericResponse.Messages.FirstOrDefault());
            }
        }

        throw new Exception("Não foi possível atualizar o pedido.");
    }

    public async Task<GenericResponseViewModel> Close(Guid id, string token)
    {
        HttpResponseMessage response;
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            response = await client.PatchAsync(@"http://localhost:5000/api/Pedido/" + id + "/Close", null);
            if (response.IsSuccessStatusCode)
            {
                var genericResponseViewModel = await response.Content.ReadFromJsonAsync<GenericResponseViewModel>();
                if (genericResponseViewModel != null)
                {
                    return genericResponseViewModel;
                }
            }
        }

        if ((int)response.StatusCode == StatusCodes.Status400BadRequest)
        {
            var genericResponse = await response.Content.ReadFromJsonAsync<GenericResponseViewModel>();
            if (genericResponse != null)
            {
                throw new ArgumentException(genericResponse.Messages.FirstOrDefault());
            }
        }

        throw new Exception("Não foi possível atualizar o pedido.");
    }

    public async Task<GenericResponseViewModel> Cancel(Guid id, string token)
    {
        HttpResponseMessage response;
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            response = await client.PatchAsync(@"http://localhost:5000/api/Pedido/" + id + "/Cancel", null);
            if (response.IsSuccessStatusCode)
            {
                var genericResponseViewModel = await response.Content.ReadFromJsonAsync<GenericResponseViewModel>();
                if (genericResponseViewModel != null)
                {
                    return genericResponseViewModel;
                }
            }
        }

        if ((int)response.StatusCode == StatusCodes.Status400BadRequest)
        {
            var genericResponse = await response.Content.ReadFromJsonAsync<GenericResponseViewModel>();
            if (genericResponse != null)
            {
                throw new ArgumentException(genericResponse.Messages.FirstOrDefault());
            }
        }

        throw new Exception("Não foi possível atualizar o pedido.");
    }
}
