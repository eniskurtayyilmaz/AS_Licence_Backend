using AS_Licence.Entites.ComplexType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AS_Licence.Entities.Model.Customer
{
  public class Customer : IBaseModel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEMail { get; set; }
    public string CustomerPhone { get; set; }
    public bool CustomerIsActive { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
    

  }
}
