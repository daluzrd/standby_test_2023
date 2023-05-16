using Mvc.DataService.Interface;
using Mvc.Models;
using Mvc.Models.Pedido;
using Mvc.Models.PedidoItem;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Mvc.DataService;

public class PedidoItemService : IPedidoItemService
{

    public async Task<List<GetPedidoItemByPedidoIdViewModel>> GetPedidoItemByPedidoId(Guid pedidoId, string token)
    {

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(@"http://localhost:5000/api/Pedido/" + pedidoId + "/Item");
            if (response.IsSuccessStatusCode)
            {
                var pedidoItem = await response.Content.ReadFromJsonAsync<List<GetPedidoItemByPedidoIdViewModel>>();
                if (pedidoItem != null)
                {
                    return pedidoItem;
                }
            }
        }

        throw new Exception("Ocorreu um erro ao recuperar o produto.");
    }

    public async Task<GetPedidoItemByIdViewModel> GetPedidoItemById(Guid pedidoId, string token)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(@"http://localhost:5000/api/PedidoItem/" + pedidoId);
            if (response.IsSuccessStatusCode)
            {
                var pedidoItem = await response.Content.ReadFromJsonAsync<GetPedidoItemByIdViewModel>();
                if (pedidoItem != null)
                {
                    return pedidoItem;
                }
            }
        }

        throw new Exception("Ocorreu um erro ao recuperar o item do pedido.");
    }

    public async Task<GenericResponseViewModel> CreateOrEdit(CreateOrEditPedidoItemViewModel model, string token)
    {
        HttpResponseMessage response;
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var uri = @"http://localhost:5000/api/PedidoItem/";
            if (model.Id != Guid.Empty)
            {
                uri += model.Id + "/quantidade";

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
                response = await client.PatchAsync(uri, contentString);
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

        if (model.Id != Guid.Empty)
        {
            throw new Exception($"Não foi possível criar o item do pedido.");
        }
        else
        {
            throw new Exception($"Não foi possível atualizar o item do pedido.");
        }
    }

    public async Task Delete(Guid id, string token)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.DeleteAsync(@"http://localhost:5000/api/PedidoItem/" + id);
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

        throw new Exception("Não foi possível excluir o item do pedido.");
    }
}
