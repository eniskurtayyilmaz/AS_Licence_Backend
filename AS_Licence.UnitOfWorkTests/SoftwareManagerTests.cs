using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Data.Repository.Host.EntityFramework;
using AS_Licence.Data.Repository.UnitOfWork.EntityFramework;
using AS_Licence.Entities.Model.Software;
using AS_Licence.Service.Host.Software;
using AS_Licence.Service.Host.Subscription;
using AS_Licence.Service.Interface.Software;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace AS_Licence.UnitOfWorkTests
{
  public class SoftwareManagerTests
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISoftwareManager _softwareManager;
    private ITestOutputHelper _outputHelper;
    public SoftwareManagerTests(ITestOutputHelper outputHelper)
    {
      this._outputHelper = outputHelper;
      var optionsBuilder = new DbContextOptionsBuilder<EfAsLicenceContext>();
      optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AS_LICENCE;Data Source=.");

      EfAsLicenceContext context = new EfAsLicenceContext(optionsBuilder.Options);

      _unitOfWork = new EfUnitOfWork(context);

      _softwareManager = new SoftwareService(_unitOfWork, new SubscriptionService(_unitOfWork));
    }
    [Fact]
    public void Can_Add_Software_Thought_UnitOfWork()
    {
      //Arrange
      var softwareModel = new Software()
      {
        SoftwareId = 0,
        SoftwareName = "AdýSoyadý",
        SoftwareIsActive = true,
        SoftwareLastVersion = "v.1.2"
      };

      //Action
      var result = _softwareManager.SaveSoftware(softwareModel);

      //Asserts
      if (result.Status == false)
      {
        _outputHelper.WriteLine(result.Message);
      }

      Assert.True(result.Status);
      Assert.True(softwareModel.SoftwareId > 0);

      Can_Delete_Exists_Software_Thought_UnitOfWork(softwareModel.SoftwareId);
    }


    private void Can_Delete_Exists_Software_Thought_UnitOfWork(int softwareId)
    {
      //Arrange


      //Action
      var result = _softwareManager.DeleteSoftwareBySoftwareId(softwareId);

      //Asserts
      if (result.Status == false)
      {
        _outputHelper.WriteLine(result.Message);
      }

      Assert.True(result.Status);

    }
  }
}
