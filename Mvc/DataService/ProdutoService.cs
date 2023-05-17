using Mvc.DataService.Interface;
using Mvc.Models;
using Mvc.Models.Produto;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Mvc.DataService;
public class ProdutoService : IProdutoService
{

    public async Task<IEnumerable<ProdutoViewModel>> Get(string token, string? filter = null)
    {

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = "http://localhost:5000/api/Produto";
            url += !string.IsNullOrWhiteSpace(filter)
                ? $"?filter={filter}"
                : string.Empty;

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var produtos = await response.Content.ReadFromJsonAsync<List<ProdutoViewModel>>();
                if (produtos != null)
                {
                    return produtos;
                }
            }
        }

        throw new Exception("Ocorreu um erro ao recuperar os produtos.");
    }

    public async Task<ProdutoViewModel> GetById(Guid id, string token)
    {

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(@"http://localhost:5000/api/Produto/" + id);
            if (response.IsSuccessStatusCode)
            {
                var produto = await response.Content.ReadFromJsonAsync<ProdutoViewModel>();
                if (produto != null)
                {
                    return produto;
                }
            }
        }

        throw new Exception("Ocorreu um erro ao recuperar o produto.");
    }
    public async Task<GenericResponseViewModel> CreateOrEdit(CreateOrEditProdutoViewModel model, string token)
    {
        HttpResponseMessage response;
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var uri = @"http://localhost:5000/api/Produto/";
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
                throw new Exception(genericResponse.Messages.FirstOrDefault());
            }
        }

        throw new Exception("Não foi possível atualizar o cliente.");
    }

    public async Task Delete(Guid id, string token)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.DeleteAsync(@"http://localhost:5000/api/Produto/" + id);
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

        throw new Exception("Ocorreu um erro ao tentar excluir o produto.");
    }
}
