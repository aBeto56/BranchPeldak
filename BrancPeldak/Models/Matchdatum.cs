using System;
using System.Collections.Generic;

namespace BrancPeldak.Models;

public partial class Matchdatum
{
    public string Id { get; set; } = null!;

    public DateTime SubbedIn { get; set; }

    public DateTime SubbedOut { get; set; }

    public int Try { get; set; }

    public int Goal { get; set; }

    public int Fault { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime UpdatedTime { get; set; }

    public string PlayerId { get; set; }

    public virtual Player Player { get; set; }
}
