using System;
using System.Diagnostics;
using AS_Licence.Helpers.EncryptionUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AS_Licence.EncryptionUITests
{
  [TestClass]
  public class EncryptionUITests
  {
    [TestMethod]
    public void Can_Get_HardwareInfo()
    {
      //Arrange
      var hddInfo = new RegisterComputer("NottiV3", "kurtayyilmaz@gmail.com");

      //Action

      //Assert
      Assert.IsNotNull(hddInfo);

      Assert.IsTrue(hddInfo.ComputerInfoHddSerialCode.Length > 0);
      Assert.IsTrue(hddInfo.ComputerInfoMacSerialCode.Length > 0);
      Assert.IsTrue(hddInfo.ComputerInfoProcessSerialCode.Length > 0);
    }

    [TestMethod]
    public void Can_Not_Pass_To_LisansAdresi()
    {
      //Arrange
      string lisansAdres
        = "http://lisans.codeapp.co/";

      var registerInfo = new RegisterComputer("NottiV3", "kurtayyilmaz@gmail.com");

      //Action
      var result = new LisansHelper(lisansAdres).GetRegisterRequestResponse(registerInfo);

      //Assert
      Assert.IsNotNull(result);

      Debug.WriteLine(result.Result.Message);
      Assert.IsFalse(result.Result.Status);
    }

    [TestMethod]
    public void Can_Pass_To_LisansAdresi()
    {
      //Arrange
      string lisansAdres
        = "http://lisans.codeapp.co/";

      var registerInfo = new RegisterComputer("NottiV1", "kurtay@adminsoft.com.tr");

      //Action
      var result = new LisansHelper(lisansAdres).GetRegisterRequestResponse(registerInfo);

      //Assert
      Assert.IsNotNull(result);

      Debug.WriteLine(result.Result.Message);
      Assert.IsTrue(result.Result.Status);
    }
  }
}
