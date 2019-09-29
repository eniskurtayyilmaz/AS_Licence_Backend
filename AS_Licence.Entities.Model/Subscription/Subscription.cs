using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AS_Licence.Entites.ComplexType;

namespace AS_Licence.Entities.Model.Subscription
{
  public class Subscription : IBaseModel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SubscriptionId { get; set; }
    public int SoftwareId { get; set; }
    public int CustomerId { get; set; }
    public bool SubscriptionIsActive { get; set; }
    public DateTime SubScriptionStartDate { get; set; }
    public DateTime SubScriptionEndDate { get; set; }
    public int SubScriptionLicenceCount { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }

  }
}
