using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartialZ.Api.Dtos;
using PartialZ.Api.Services.Interfaces;
using PartialZ.DataAccess.PartialZDB;
using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PartialZ.Api.Services
{
    public class EmployeeService : IEmployee
    {
        private PartialClaimsContext _PartialClaimsContext;
        private ICryptographyService _cryptographyService;
        private IMailService _mailService;
        public EmployeeService(PartialClaimsContext PartialClaimsContext, IMailService mailService,
            ICryptographyService cryptographyService)
        {
            this._PartialClaimsContext = PartialClaimsContext;
            this._cryptographyService = cryptographyService;
           this._mailService = mailService;
        }
        public async Task<EmployeeDto> GetEmployee(string EmailID)
        {
            return await this._PartialClaimsContext.Filers.Select(e => new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Password = e.Password,
                BusinessTitle = e.BusinessTitle,
                PhoneNumber = e.PhoneNumber
            }).Where(e => e.Email == EmailID).FirstAsync();
        }
        public  List<RegistrationDetailsDto> GetEmployeeDetails(string EmailID)
        {          
                
              var result = this._PartialClaimsContext.Filers
                .Join(this._PartialClaimsContext.EmployeeWorkHistories,
                f => f.EmployeeId, s => s.EmployeeId,
                (f,s) => new { f,s})
                .Join(this._PartialClaimsContext.Employers, e => e.s.EmployerId, x => x.EmployerId,
                (e,x) => new RegistrationDetailsDto
                {
                    filerName = e.f.FirstName + ' ' + e.f.LastName,
                    email = e.f.Email,
                    employerName = x.Name,
                    eannumber = x.Eannumber,
                    feinnumber = x.Feinnumber,
                    phoneNumber = e.f.PhoneNumber,
                    payrollEndDay = e.s.PayrollEndDay
                }).ToList();
            return result;
        }
        public async Task<string> RegsregisterEmployee(string emailID, string password)
        {
            try
            {
                if (this._PartialClaimsContext.EmployeeWorkHistories.Where(e => e.EmployeeId == (this._PartialClaimsContext.Filers.Where(e => e.Email == emailID).Select(x => x.EmployeeId).FirstOrDefault())).Any())
                {
                    return "Registered";
                }
                else
                {
                    if (this._PartialClaimsContext.Filers.Where(e => e.Email == emailID).Any())
                    {
                        //update
                        var existingdata = await this._PartialClaimsContext.Filers.Where(e => e.Email == emailID).FirstAsync();
                        existingdata.Email = emailID;
                        existingdata.Password = this._cryptographyService.Encrypt(password);
                        existingdata.LastModifedDate = DateTime.UtcNow;
                        if (existingdata.IsVerified == 1)
                        {
                            await this._PartialClaimsContext.SaveChangesAsync();
                            return "Verified.";
                        }
                        else
                        {
                            await this._PartialClaimsContext.SaveChangesAsync();
                            return "Verify";
                        }
                    }
                    else
                    {

                        //insert
                        var data = new Filer()
                        {
                            Email = emailID,
                            Password = this._cryptographyService.Encrypt(password)
                        };
                        await this._PartialClaimsContext.Filers.AddAsync(data);
                        await this._PartialClaimsContext.SaveChangesAsync();
                        this._mailService.SendVerificationMail(emailID);
                        return "Verify";
                    }
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> VerifyEmployee(string emailID)
        {
            try
            {
                emailID=this._cryptographyService.Decrypt(emailID);
                if (this._PartialClaimsContext.Filers.Where(e => e.Email == emailID).Any())
                {
                    //update
                    var existingdata = await this._PartialClaimsContext.Filers.Where(e => e.Email == emailID).FirstAsync();
                    existingdata.IsVerified = 1;
                    existingdata.LastModifedDate = DateTime.UtcNow;
                    return await this._PartialClaimsContext.SaveChangesAsync();
                }
                else
                {
                    //bad request
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> EmplpyeeDetailRegistration(AffidavitDto affidavitDto)
        {
            try
            {
                int employeeID = 0;
                //affidavitDto.Email = this._cryptographyService.Decrypt(affidavitDto.Email);
                if (this._PartialClaimsContext.Filers.Where(e => e.Email == affidavitDto.EmployerEmail).Any())
                {
                    //update
                    var existingdata = await this._PartialClaimsContext.Filers.Where(e => e.Email == affidavitDto.EmployerEmail).FirstAsync();
                    existingdata.Email = affidavitDto.EmployerEmail;
                    existingdata.FirstName = affidavitDto.ContactFirstName;
                    existingdata.LastName = affidavitDto.ContactLastName;
                    existingdata.BusinessTitle = affidavitDto.BusinessTitle;
                    existingdata.PhoneNumber = affidavitDto.ContactPhone;
                    existingdata.LastModifedDate = DateTime.UtcNow;
                    await this._PartialClaimsContext.SaveChangesAsync();
                    employeeID = existingdata.EmployeeId;
                }
                else
                {
                    //insert
                    var data = new Filer()
                    {
                        Email = affidavitDto.Email,
                        FirstName = affidavitDto.ContactFirstName,
                        LastName = affidavitDto.ContactLastName,
                        BusinessTitle = affidavitDto.BusinessTitle,
                        PhoneNumber = affidavitDto.ContactPhone
                    };
                    await this._PartialClaimsContext.Filers.AddAsync(data);
                    await this._PartialClaimsContext.SaveChangesAsync();
                    employeeID =data.EmployeeId;
                }
                this._mailService.SendAffidavitSubmisionMail(affidavitDto.EmployerEmail , affidavitDto.EmployerName);
                return employeeID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FilerDto> GetFilerNames()
        {
            return this._PartialClaimsContext.Filers.Select(e => new FilerDto
            {
                FilerId = e.EmployeeId,
                FilerName = e.FirstName + e.LastName
            }).ToList();
        }



    }
}
