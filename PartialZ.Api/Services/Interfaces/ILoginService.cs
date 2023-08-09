namespace PartialZ.Api.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(string emailID, string password);
        string ValidateOTP(string emailID, string OTP);
        string forgetPassword(string emailID, string password);
    }
}
