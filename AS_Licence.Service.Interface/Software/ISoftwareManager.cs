using AS_Licence.Entities.ViewModel.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AS_Licence.Service.Interface.Software
{
  public interface ISoftwareManager
  {
    Task<OperationResponse<List<Entities.Model.Software.Software>>> GetSoftwareList(
      Expression<Func<Entities.Model.Software.Software, bool>> filter = null,
      Func<IQueryable<Entities.Model.Software.Software>, IOrderedQueryable<Entities.Model.Software.Software>> orderBy =
        null, string includeProperties = "");
    Task<OperationResponse<Entities.Model.Software.Software>> SaveSoftware(Entities.Model.Software.Software software);
    Task<OperationResponse<Entities.Model.Software.Software>> DeleteSoftwareBySoftwareId(int id);
    Task<OperationResponse<Entities.Model.Software.Software>> GetBySoftwareId(int id);
    Task<OperationResponse<Entities.Model.Software.Software>> GetBySoftwareName(string softwareName);
  }
}
