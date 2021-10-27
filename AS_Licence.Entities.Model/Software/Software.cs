using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AS_Licence.Entites.ComplexType;

namespace AS_Licence.Entities.Model.Software
{
  public class Software : IBaseModel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SoftwareId { get; set; }
    public string SoftwareName { get; set; }
    public string SoftwareDownloadUrl { get; set; }
    public string SoftwareLastVersion { get; set; }
    public bool SoftwareIsActive { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }

  }
}
