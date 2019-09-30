using System.Threading.Tasks;
using AS_Licence.Entities.ViewModel.Operations;

namespace AS_Licence.Service.Interface.RegisterComputer
{
  public interface IRegisterComputerManager
  {
    Task<OperationResponse<Entities.ViewModel.Register.RegisterComputer>> SaveRegisterComputer(
      Entities.ViewModel.Register.RegisterComputer registerComputer);
  }
}