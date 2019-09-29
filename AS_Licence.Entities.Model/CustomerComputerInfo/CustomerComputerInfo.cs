using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AS_Licence.Entites.ComplexType;

namespace AS_Licence.Entities.Model.CustomerComputerInfo
{
  public class CustomerComputerInfo : IBaseModel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerComputerInfoId { get; set; }
    public int SubscriptionId { get; set; }
    public string CustomerComputerInfoHddSerialCode { get; set; }
    public string CustomerComputerInfoMacSerialCode { get; set; }
    public string CustomerComputerInfoProcessSerialCode { get; set; }

    public DateTime? CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
    
  }
}
