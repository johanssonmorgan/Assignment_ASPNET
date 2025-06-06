﻿using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class EditClientForm
{
    public string Id { get; set; } = null!;
    public string? Image { get; set; }
    public string ClientName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Location { get; set; }
    public string? Phone { get; set; }
}
