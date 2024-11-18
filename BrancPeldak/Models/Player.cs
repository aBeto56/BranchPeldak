using System;
using System.Collections.Generic;

namespace BrancPeldak.Models;

public partial class Player
{
    public string Id { get; set; }

    public string Name { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }

    public DateTime CreatedTime { get; set; }


    public virtual ICollection<Matchdatum> Matchdata { get; set; } = new List<Matchdatum>();

}
