using Acc.Server.Results.Companion.Database.Entities;

namespace Acc.Server.Results.Companion.Core.Models;

public class PenaltyViewModel 
{
    public PenaltyViewModel(Penalty penalty, ServerDetails serverDetails)
    {
        this.Car = penalty.Car;
        this.DriverCategory = penalty.DriverCategory;
        this.Driver = penalty.Driver;
        this.DriverClass = this.GetDriverClass(penalty, serverDetails);
        this.ClearedOnLap = penalty.ClearedOnLap;
        this.IsPostRacePenalty = penalty.IsPostRacePenalty;
        this.Nationality = penalty.Nationality;
        this.NationalityCode = penalty.NationalityCode;
        this.PenaltyCode = penalty.PenaltyCode;
        this.PenaltyValue = penalty.PenaltyValue;
        this.Reason = penalty.Reason;
        this.ViolationOnLap = penalty.ViolationOnLap;
    }

    private string GetDriverClass(Penalty penalty, ServerDetails serverDetails)
    {
        return penalty.DriverCategory switch
        {
            "Silver" => serverDetails.SilverClassification,
            "Gold" => serverDetails.GoldClassification,
            "Platinum" => serverDetails.PlatinumClassification,
            _ => serverDetails.BronzeClassification
        };
    }

    public string DriverClass { get; set; }
    public string Car { get; set; }
    public int ClearedOnLap { get; set; }
    public string Driver { get; set; }
    public string DriverCategory { get; set; }
    public bool IsPostRacePenalty { get; set; }
    public string Nationality { get; set; }
    public int NationalityCode { get; set; }
    public string PenaltyCode { get; set; }
    public int PenaltyValue { get; set; }
    public string Reason { get; set; }
    public int ViolationOnLap { get; set; }
}