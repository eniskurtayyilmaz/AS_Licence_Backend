using System;
using System.Collections.Generic;
using System.Text;

namespace AS_Licence.Entities.ViewModel.Subscription
{
  public class SubscriptionInfo
  {
    public int SubscriptionId { get; set; }
    public int SoftwareId { get; set; }
    public int CustomerId { get; set; }
    public string SoftwareName { get; set; }
    public bool SubscriptionIsActive { get; set; }
    public DateTime SubScriptionStartDate { get; set; }
    public DateTime SubScriptionEndDate { get; set; }
    public int SubScriptionLicenceCount { get; set; }
    public int SubScriptionCurrentLicenceCount { get; set; }
  }
}
