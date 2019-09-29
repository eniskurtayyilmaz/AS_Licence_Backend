using System;
using AS_Licence.Data.Interface.UnitOfWork;
using AS_Licence.Data.Repository.Host.EntityFramework;
using AS_Licence.Data.Repository.UnitOfWork.EntityFramework;
using AS_Licence.Entities.Model.Software;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AS_Licence.EntityFrameworkTests
{
  public class DbSetSoftwareTests
  {
    private IUnitOfWork _unitOfWork;
    public DbSetSoftwareTests()
    {

      var optionsBuilder = new DbContextOptionsBuilder<EfAsLicenceContext>();
      optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AS_LICENCE;Data Source=.");

      EfAsLicenceContext context = new EfAsLicenceContext(optionsBuilder.Options);

      _unitOfWork = new EfUnitOfWork(context);
    }

    [Fact]
    public void Can_Add_SoftwareEntity()
    {
      //Arrange
      Software model = new Software()
      {
        SoftwareId = 0,
        SoftwareLastVersion = "v1.20",
        SoftwareName = "NottiV3",
        CreatedDateTime = DateTime.Now,
        SoftwareIsActive = true,
      };

      //Action
      /*
      _unitOfWork.SoftwareRepository.Insert(model);
      _unitOfWork.Save();

      //Assert
      Assert.True(model.SoftwareId > 0);
      */
    }
  }
}
