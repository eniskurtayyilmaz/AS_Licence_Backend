using AS_Licence.Entities.ViewModel.Operations;

namespace AS_Licence.Service.Interface.RegisterComputer
{
  public interface IRegisterComputerManager
  {
    OperationResponse<Entities.ViewModel.Register.RegisterComputer> SaveRegisterComputer(Entities.ViewModel.Register.RegisterComputer registerComputer);
  }
}