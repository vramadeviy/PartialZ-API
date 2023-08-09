using Microsoft.EntityFrameworkCore;
using PartialZ.Api.Services.Interfaces;
using PartialZ.DataAccess.PartialZDB;
using System.Xml.Linq;

namespace PartialZ.Api.Services
{
    public class LoginService : ILoginService
    {
        private PartialClaimsContext _PartialClaimsContext;
        private ICryptographyService _cryptographyService;
        private IMailService _mailService;
        public LoginService(PartialClaimsContext PartialClaimsContext,
            IMailService mailService,
            ICryptographyService cryptographyService)
        {
            this._PartialClaimsContext = PartialClaimsContext;
            this._cryptographyService = cryptographyService;
            this._mailService = mailService;
        }
        public async Task<string> Login(string emailID, string password)
        {
            try
            {
                password = this._cryptographyService.Encrypt(password);
                if (this._PartialClaimsContext.Filers.Where(e => e.Email == emailID && e.Password == password && e.IsVerified == 1).Any())
                {

                    string instantOTP = GenerateOTP();
                    if (instantOTP.Length != 6) instantOTP = GenerateOTP();
                    SaveOTP(emailID, instantOTP);
                    this._mailService.SendOTP(emailID, instantOTP);
                    return "We have sent you the otp to register email.";

                }
                else if (this._PartialClaimsContext.Filers.Where(e => e.Email == emailID && e.Password == password).Any())
                {
                    var existingdata = await this._PartialClaimsContext.Filers.Where(e => e.Email == emailID && e.Password == password).FirstAsync();
                    if (existingdata != null && existingdata.IsVerified == 0)
                    {
                        return "Your account is inactive,please check your email to activate your account";
                    }
                    else
                    {
                        return "Invalid credentials";
                    }
                }
                else if (!this._PartialClaimsContext.Filers.Where(e => e.Email == emailID).Any())
                {
                    return "EmailId is not registered. Please register.";
                }
                else if (!this._PartialClaimsContext.Filers.Where(e => e.Password == password).Any())
                {
                    return "Password Incorrect.";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ValidateOTP(string emailID, string OTP)
        {
            try
            {
                int cotp = Convert.ToInt32(OTP);
                if (this._PartialClaimsContext.Filers.Where(e => e.Email == emailID && e.LoginOtp == cotp && e.IsVerified == 1).Any())
                {
                    return "OTP verified successfully";

                }
                else
                {
                    return "Invalid OTP";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveOTP(string emailID, string OTP)
        {
            try
            {

                if (this._PartialClaimsContext.Filers.Where(e => e.Email == emailID && e.IsVerified == 1).Any())
                {
                    var Data = this._PartialClaimsContext.Filers.Where(e => e.Email == emailID && e.IsVerified == 1).Single();
                    Data.LoginOtp = Convert.ToInt32(OTP);
                    Data.LastModifedDate = DateTime.Now;
                    this._PartialClaimsContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private static string GenerateOTP()
        {
            Random random = new Random();
            string otp = "";

            for (int i = 0; i < 6; i++)
            {
                otp += random.Next(0, 9).ToString();
            }
           
            return otp;
        }

        public void ForgetPassword(string emailID, string password)
        {
            try
            {
                if (this._PartialClaimsContext.Filers.Where(e => e.Email == emailID && e.IsVerified == 1).Any())
                {
                    var Data = this._PartialClaimsContext.Filers.Where(e => e.Email == emailID && e.IsVerified == 1).Single();
                    Data.Password = password; 
                    this._PartialClaimsContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string forgetPassword(string emailID, string password)
        {
            try
            {
                password = this._cryptographyService.Encrypt(password);
                if (!this._PartialClaimsContext.Filers.Where(e => e.Email == emailID).Any())
                {
                    return "Enter a register email id.";
                }
               else if (this._PartialClaimsContext.Filers.Where(e => e.Email == emailID && e.Password == password && e.IsVerified == 1).Any())
                {
                    return "The new Password you entered is the same as your old password. Enter a different password.";
                }
               else if (this._PartialClaimsContext.Filers.Where(e => e.Email == emailID && e.IsVerified == 1).Any())
                {
                    ForgetPassword(emailID, password);
                    this._mailService.SendPassword(emailID);
                    return "Password successfully changed.";
                }                
                else
                {
                    return "Invalid credentials";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
