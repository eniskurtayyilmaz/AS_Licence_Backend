using System.Threading.Tasks;
using AS_Licence.Data.Interface.GenericRepository;
using AS_Licence.Entities.Model.Software;

namespace AS_Licence.Data.Interface.DataAccess
{
  public interface ISoftwareDal : IGenericRepository<Software>
  {
    Task<Software> GetSoftwareByName(string softwareName);
  }
}