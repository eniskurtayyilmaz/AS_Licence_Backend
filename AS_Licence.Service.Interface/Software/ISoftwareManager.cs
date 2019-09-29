using AS_Licence.Entities.ViewModel.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AS_Licence.Service.Interface.Software
{
  public interface ISoftwareManager
  {
    OperationResponse<List<Entities.Model.Software.Software>> GetSoftwareList(Expression<Func<Entities.Model.Software.Software, bool>> filter = null, Func<IQueryable<Entities.Model.Software.Software>, IOrderedQueryable<Entities.Model.Software.Software>> orderBy = null, string includeProperties = "");
    OperationResponse<Entities.Model.Software.Software> SaveSoftware(Entities.Model.Software.Software software);
    OperationResponse<Entities.Model.Software.Software> DeleteSoftwareBySoftwareId(int id);
    OperationResponse<Entities.Model.Software.Software> GetBySoftwareId(int id);
    OperationResponse<Entities.Model.Software.Software> GetBySoftwareName(string softwareName);
  }
}
