namespace Mvc.Models.Cliente;

public class ClienteViewModel
{
    public int Draw { get; private set; }
    public int RecordsTotal { get; private set; }
    public int RecordsFiltered { get; private set; }
    public IEnumerable<ClienteByIdViewModel> Data { get; private set; }

    public ClienteViewModel(int draw, int recordsTotal, int recordsFiltered, IEnumerable<ClienteByIdViewModel> data)
    {
        Draw = draw;
        RecordsTotal = recordsTotal;
        RecordsFiltered = recordsFiltered;
        Data = data;
    }

    public void AddDraw(int draw)
    {
        Draw = draw;
    }
}
