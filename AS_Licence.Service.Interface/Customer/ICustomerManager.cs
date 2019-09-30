using AS_Licence.Entities.ViewModel.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AS_Licence.Service.Interface.Customer
{
  public interface ICustomerManager
  {
    Task<OperationResponse<List<Entities.Model.Customer.Customer>>> GetCustomerList(
      Expression<Func<Entities.Model.Customer.Customer, bool>> filter = null,
      Func<IQueryable<Entities.Model.Customer.Customer>, IOrderedQueryable<Entities.Model.Customer.Customer>> orderBy =
        null, string includeProperties = "");
    Task<OperationResponse<Entities.Model.Customer.Customer>> SaveCustomer(Entities.Model.Customer.Customer customer);
    Task<OperationResponse<Entities.Model.Customer.Customer>> DeleteCustomerByCustomerId(int id);
    Task<OperationResponse<Entities.Model.Customer.Customer>> GetByCustomerId(int id);
    Task<OperationResponse<Entities.Model.Customer.Customer>> GetCustomerByEmail(string customerEmail);

  }
}
