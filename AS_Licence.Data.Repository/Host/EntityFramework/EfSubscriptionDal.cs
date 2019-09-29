using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AS_Licence.Data.Interface.DataAccess;
using AS_Licence.Data.Repository.Infrastracture.EntityFramework;
using AS_Licence.Entities.Model.Subscription;

namespace AS_Licence.Data.Repository.Host.EntityFramework
{
  public class EfSubscriptionDal : EfEntityRepositoryBase<Subscription>, ISubscriptionDal
  {
    private readonly EfAsLicenceContext _context;

    public EfSubscriptionDal(EfAsLicenceContext context) : base(context)
    {
      _context = context;
    }

    public List<Subscription> GetSubscriptionListBySoftwareId(int softwareId)
    {
      return this.Get(x => x.SoftwareId == softwareId).ToList();
    }

    public List<Subscription> GetSubscriptionListByCustomerId(int customerId)
    {
      return this.Get(x => x.CustomerId == customerId);
    }

    public Subscription GetSubscriptionBySoftwareIdAndCustomerId(int softwareId, int customerId)
    {
      return this.Get(x => x.SoftwareId == softwareId && x.CustomerId == customerId && x.SubScriptionStartDate <= DateTime.Now && DateTime.Now <= x.SubScriptionEndDate).SingleOrDefault();
    }
  }
}
