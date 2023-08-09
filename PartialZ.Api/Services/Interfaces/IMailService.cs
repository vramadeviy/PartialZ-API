namespace PartialZ.Api.Services.Interfaces
{
    public interface IMailService
    {
        void SendVerificationMail(string toMailID);
        void SendOTP(string toMailID, string OTP);

        void SendPassword(string toMailId);
        void SendAffidavitSubmisionMail(string toMailID , string EmployerName);
    }
}
