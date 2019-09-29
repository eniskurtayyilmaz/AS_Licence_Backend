using AS_Licence.Entities.ViewModel.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AS_Licence.Service.Interface.Customer
{
  public interface ICustomerManager
  {
    OperationResponse<List<Entities.Model.Customer.Customer>> GetCustomerList(Expression<Func<Entities.Model.Customer.Customer, bool>> filter = null, Func<IQueryable<Entities.Model.Customer.Customer>, IOrderedQueryable<Entities.Model.Customer.Customer>> orderBy = null, string includeProperties = "");
    OperationResponse<Entities.Model.Customer.Customer> SaveCustomer(Entities.Model.Customer.Customer customer);
    OperationResponse<Entities.Model.Customer.Customer> DeleteCustomerByCustomerId(int id);
    OperationResponse<Entities.Model.Customer.Customer> GetByCustomerId(int id);
    OperationResponse<Entities.Model.Customer.Customer> GetCustomerByEmail(string customerEmail);

  }
}
