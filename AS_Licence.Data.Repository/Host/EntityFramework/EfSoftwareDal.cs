using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AS_Licence.Data.Interface.DataAccess;
using AS_Licence.Data.Repository.Infrastracture.EntityFramework;
using AS_Licence.Entities.Model.Software;

namespace AS_Licence.Data.Repository.Host.EntityFramework
{
  public class EfSoftwareDal : EfEntityRepositoryBase<Software>, ISoftwareDal
  {
    private readonly EfAsLicenceContext _context;

    public EfSoftwareDal(EfAsLicenceContext context) : base(context)
    {
      _context = context;
    }

    public Software GetSoftwareByName(string softwareName)
    {
      return this.Get(x => x.SoftwareName == softwareName).SingleOrDefault();
    }
  }
}
