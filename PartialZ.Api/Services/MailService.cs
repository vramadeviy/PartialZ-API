using Microsoft.Extensions.Logging;
using PartialZ.Api.Services.Interfaces;
using System.Net.Mail;
using System.Net;
using PartialZ.DataAccess.PartialZDB;
using PartialZ.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PartialZ.Api.Enums;

namespace PartialZ.Api.Services
{
    public class MailService: IMailService
    {
        private PartialClaimsContext _PartialClaimsContext;
        private ICryptographyService _cryptographyService;
        private IConfiguration _configuration { get; }
        public MailService(PartialClaimsContext PartialClaimsContext, IConfiguration configuration,
            ICryptographyService cryptographyService)
        {
            this._PartialClaimsContext = PartialClaimsContext;
            this._cryptographyService = cryptographyService;
            this._configuration = configuration;
        }
        public void SendVerificationMail(string toMailID)
        {
            try
            {
                using (MailMessage user = new MailMessage("PartialZ<" + this._configuration.GetValue<string>("Mail:FromEmail") + ">", toMailID.Trim()))
                {
                    var Template = GetTemplate((int)EmailEnum.EmailVerification).Result;
                    string myString = "";
                    myString = Template.Template;
                    myString = myString.Replace("$$VERIFICATION_URL$$", this._configuration.GetValue<string>("Mail:TrgetURL") + this._cryptographyService.Encrypt(toMailID));
                    user.Subject = Template.Subject;
                    user.Body = myString.ToString();
                    user.IsBodyHtml = true;
                    Send(user);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SendOTP(string toMailID, string OTP)
        {
            try
            {
                using (MailMessage user = new MailMessage("PartialZ<" + this._configuration.GetValue<string>("Mail:FromEmail") + ">", toMailID.Trim()))
                {
                    var Template = GetTemplate((int)EmailEnum.OTP).Result;
                    string myString = "";
                    myString = Template.Template;
                    myString = myString.Replace("$$OTP_CODE$$", OTP);
                    user.Subject = Template.Subject;
                    user.Body = myString.ToString();
                    user.IsBodyHtml = true;
                    Send(user);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Send(MailMessage user)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = this._configuration.GetValue<string>("Mail:Host");
            smtp.UseDefaultCredentials = false;
            smtp.Port = Convert.ToInt32(this._configuration.GetValue<string>("Mail:Port"));
            smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
            try
            {
                smtp.Send(user);
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
        public void SendAffidavitSubmisionMail(string toMailID , string EmployerName)
        {
            try
            {
                using (MailMessage user = new MailMessage("PartialZ<" + this._configuration.GetValue<string>("Mail:FromEmail") + ">", toMailID.Trim()))
                {
                    var Template = GetTemplate((int)EmailEnum.AffidavitSubmission).Result;
                    string myString = "";
                    myString = Template.Template;
                    myString = myString.Replace("$$EMPLOYER_NAME$$",EmployerName);
                    user.Subject = Template.Subject;
                    user.Body = myString.ToString();
                    user.IsBodyHtml = true;
                    Send(user);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       public void SendPassword(string toMailId)
        {
            try
            {
                using (MailMessage user = new MailMessage("PartialZ<" + this._configuration.GetValue<string>("Mail:FromEmail") + ">", toMailId.Trim()))
                {
                    var Template = GetTemplate((int)EmailEnum.PasswordChanged).Result;
                    string myString = "";
                    myString = Template.Template;
                    user.Subject = Template.Subject;
                    user.Body = myString.ToString();
                    user.IsBodyHtml = true;
                    Send(user);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<EmailTemplateDto> GetTemplate(int TempID)
        {
            try
            {
                return await this._PartialClaimsContext.EmailTemplates.Where(e => e.Id == TempID && e.IsActive == 1).Select(e => new EmailTemplateDto
                {
                    Id = e.Id,
                    Description = e.Description,
                    Subject = e.Subject,
                    Template = e.Template
                }).FirstAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
