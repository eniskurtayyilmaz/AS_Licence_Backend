using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace AS_Licence.Helpers.Encryption
{

  //SELECT * FROM Win32_DiskDrive
  public static class HardwareInfo
  {
    public static string GetHddNo()
    {
      string hddNo = "";
      try
      {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
        ManagementObjectCollection disks = searcher.Get();
        foreach (ManagementObject disk in disks)
        {
          if (disk["SerialNumber"] != null)
          {
            hddNo = disk["SerialNumber"].ToString().Trim();
            break;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }

      return hddNo;
    }


  }
  public class RegisterComputer
  {
    
    public RegisterComputer()
    {

    }

    public RegisterComputer(string softwareName, string customerEmail)
    {
      this.SoftwareName = softwareName;
      this.CustomerEMail = customerEmail;

      this.ComputerInfoHddSerialCode = HardwareInfo.GetHddNo();
      

    }

    public string SoftwareName { get; set; }
    public string CustomerEMail { get; set; }
    public string ComputerInfoHddSerialCode { get; set; }
    public string ComputerInfoMacSerialCode { get; set; }
    public string ComputerInfoProcessSerialCode { get; set; }
  }
  public class OperationResponse
  {
    public OperationResponse()
    {
      Status = false;
      Message = "";
      Data = null;
    }

    public bool Status { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
  }

  public class EncryptionHelper
  {
    // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
    // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
    private const string initVector = "pemgail9uzpgzl88";
    // This constant is used to determine the keysize of the encryption algorithm
    private const int keysize = 256;

    private const string passPhrase = "nasilsinhaciabiiyimisinbendecokiyimallahiyilikversin12312312";
    //Encrypt
    public string EncryptString(string plainText)
    {
      byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
      byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
      PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
      byte[] keyBytes = password.GetBytes(keysize / 8);
      RijndaelManaged symmetricKey = new RijndaelManaged();
      symmetricKey.Mode = CipherMode.CBC;
      ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
      cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
      cryptoStream.FlushFinalBlock();
      byte[] cipherTextBytes = memoryStream.ToArray();
      memoryStream.Close();
      cryptoStream.Close();
      return Convert.ToBase64String(cipherTextBytes);
    }
    //Decrypt
    public string DecryptString(string cipherText)
    {
      byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
      byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
      PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
      byte[] keyBytes = password.GetBytes(keysize / 8);
      RijndaelManaged symmetricKey = new RijndaelManaged();
      symmetricKey.Mode = CipherMode.CBC;
      ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
      MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
      CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
      byte[] plainTextBytes = new byte[cipherTextBytes.Length];
      int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
      memoryStream.Close();
      cryptoStream.Close();
      return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }
  }
}
