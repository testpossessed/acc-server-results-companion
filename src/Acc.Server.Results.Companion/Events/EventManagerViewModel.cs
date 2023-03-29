using System;
using Acc.Server.Results.Companion.Database.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acc.Server.Results.Companion.Events;

public class EventManagerViewModel : ObservableObject
{
    private ServerDetails serverDetails;

    public void SetServerDetails(ServerDetails serverDetails)
    {
        this.serverDetails = serverDetails;
    }
}