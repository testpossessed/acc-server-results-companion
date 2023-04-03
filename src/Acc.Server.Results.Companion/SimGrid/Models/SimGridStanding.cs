using System;
using System.Collections.Generic;

namespace Acc.Server.Results.Companion.SimGrid.Models;

public class SimGridStanding
{
    public string Id { get; set; }
    public string Car { get; set; }
    public int CarNum { get; set; }
    public int ChampionshipPenalties { get; set; }
    public double ChampionshipPoints { get; set; }
    public double ChampionshipScore { get; set; }
    public int Position { get; set; }
    public List<SimGridStandingsRace> Races { get; set; }
}