namespace AS_Licence.Entites.Dto
{
  public class UserForPasswordChangeDto
  {
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string AgainNewPassword { get; set; }
  }
}