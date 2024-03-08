using System;
using System.Collections.Generic;

namespace loginProyectASPNETCORE_MVC.Models;

public partial class Usuario
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
