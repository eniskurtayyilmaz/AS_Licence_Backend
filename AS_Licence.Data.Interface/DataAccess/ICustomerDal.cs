using System.Threading.Tasks;
using AS_Licence.Data.Interface.GenericRepository;
using AS_Licence.Entities.Model.Customer;

namespace AS_Licence.Data.Interface.DataAccess
{
  public interface ICustomerDal : IGenericRepository<Customer>
  {
    Task<Customer> GetCustomerByEmail(string customerEmail);
  }
}