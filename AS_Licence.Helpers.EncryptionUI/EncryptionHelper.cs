using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AS_Licence.Helpers.EncryptionUI
{
  public class LisansHelper
  {
    private readonly string LisansAdresi;

    public LisansHelper(string lisansAdresi)
    {
      LisansAdresi = lisansAdresi;
    }

    public async Task<OperationResponse<bool>> GetRegisterRequestResponse(RegisterComputer registerComputer)
    {
      OperationResponse<bool> responseOperation = new OperationResponse<bool>();

      string url = this.LisansAdresi + "/api/Register/CheckLicenceStatus";

      string result = "";

      string postData = JsonConvert.SerializeObject(registerComputer);
      try
      {

        using (var client = new System.Net.Http.HttpClient())
        {
          var uri = new Uri(url);
          string contents = JsonConvert.SerializeObject(registerComputer);
          var response = client.PostAsync(uri, new StringContent(contents, Encoding.UTF8, "application/json"));
          response.Wait();

          Stream receiveStream = await response.Result.Content.ReadAsStreamAsync();
          StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
          result = readStream.ReadToEnd();
          /*
          if (response.Result.IsSuccessStatusCode)
          {
            Stream receiveStream = await response.Result.Content.ReadAsStreamAsync();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            result = readStream.ReadToEnd();
          }
          else
          {
            Stream receiveStream = await response.Result.Content.ReadAsStreamAsync();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            result  = readStream.ReadToEnd();
          }
          */
        }
      }
      catch (HttpRequestException ex)
      {
        responseOperation.Message = ex.Message;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        responseOperation.Message = ex.Message;
      }

      if (!string.IsNullOrEmpty(result))
      {
        try
        {
          var responseDecryptString = new EncryptionHelper<object>().DecryptString(result);

          responseOperation.Status = responseDecryptString.Status;
          responseOperation.Message = responseDecryptString.Message;
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
          responseOperation.Message = e.Message;
        }
      }

      return responseOperation;


    }
  }

  internal static class HardwareInfo
  {
    internal static string GetProcessSerialCode()
    {
      String processorID = "";
      try
      {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM WIN32_Processor");
        ManagementObjectCollection mObject = searcher.Get();

        foreach (ManagementObject obj in mObject)
        {
          processorID = obj["ProcessorId"].ToString();
          break;
        }

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
      return processorID;
    }

    internal static string GetHDDSerialCode()
    {
      String hddID = "";
      try
      {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM Win32_DiskDrive");
        ManagementObjectCollection mObject = searcher.Get();

        foreach (ManagementObject obj in mObject)
        {
          if (obj["SerialNumber"] != null)
          {
            hddID = obj["SerialNumber"].ToString().Trim();
            break;
          }
        }

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
      return hddID;
    }


    internal static string GetMacSerialCode()
    {
      String hddID = "";
      try
      {
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM Win32_NetworkAdapter");
        ManagementObjectCollection mObject = searcher.Get();

        foreach (ManagementObject obj in mObject)
        {
          if (obj["MACAddress"] != null)
          {
            hddID = obj["MACAddress"].ToString().Trim();
            break;
          }
        }

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
      return hddID;
    }
  }


  public class RegisterComputer
  {


    public RegisterComputer(string softwareName, string customerEMail)
    {
      this.SoftwareName = softwareName;
      this.CustomerEMail = customerEMail;

      this.ComputerInfoHddSerialCode = HardwareInfo.GetHDDSerialCode();
      this.ComputerInfoMacSerialCode = HardwareInfo.GetMacSerialCode();
      this.ComputerInfoProcessSerialCode = HardwareInfo.GetProcessSerialCode();
    }
    public string SoftwareName { get; set; }
    public string CustomerEMail { get; set; }
    public string ComputerInfoHddSerialCode { get; set; }
    public string ComputerInfoMacSerialCode { get; set; }
    public string ComputerInfoProcessSerialCode { get; set; }
  }

  public class OperationResponse<T>
  {
    public OperationResponse()
    {
      Status = false;
      Message = "";
      Data = default(T);
    }

    public bool Status { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
  }

  internal class EncryptionHelper<T>
  {
    // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
    // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
    private const string initVector = "pemgail9uzpgzl88";
    // This constant is used to determine the keysize of the encryption algorithm
    private const int keysize = 256;

    private const string passPhrase = "nasilsinhaciabiiyimisinbendecokiyimallahiyilikversin12312312";
    //Encrypt
    //Decrypt
    public OperationResponse<T> DecryptString(string cipherText)
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
      string json = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
      return JsonConvert.DeserializeObject<OperationResponse<T>>(json);
    }
  }
}
