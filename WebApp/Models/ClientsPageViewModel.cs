using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class ClientsPageViewModel
{
    public IEnumerable<ClientModel> Clients { get; set; } = [];
    public AddClientModalViewModel AddClient { get; set; } = new();
    public EditClientModalViewModel EditClient { get; set; } = new();
}
