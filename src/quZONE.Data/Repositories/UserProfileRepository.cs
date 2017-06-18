using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quZONE.Data.Interfaces;
using quZONE.Domain;
using quZONE.Domain.Entities;
using quZONE.Domain.Temp;
using quZONE.Domain.ViewModels;

namespace quZONE.Data.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private qZONEDbContext _context = new qZONEDbContext();

        //public UserProfileRepository(IDbContext context)
        //    : base(context)
        //{

        //}

        public IEnumerable<UserProfileViewModel> GetAllUserProfiles()
        {
            var result = from user in _context.AspNetUsers
                         join profile in _context.UserProfiles on user.UserProfile_Id equals profile.Id
                         join org in _context.Organizations on profile.OrgainzationId equals org.Id
                        
                         select new UserProfileViewModel()
                         {
                             Id = profile.Id,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             PhoneNumber = user.PhoneNumber,
                             UserName = user.UserName,
                             JoinDate = user.JoinDate,
                             OrgainzationId = profile.OrgainzationId,
                             AvatarImgUrl = profile.AvatarImgUrl,
                             Name = org.Name
                         };

            return result;
        }

        public UserProfile GetUserProfileById(int id)
        {
            return _context.UserProfiles.FirstOrDefault(u => u.Id == id);
        }




        //Testing 
        public string GetName()
        {
            return "Bob Yuan";
        }



        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        #region User Profile

        public AspNetUser GetUser(string id)
        {
            //throw new NotImplementedException();
            return _context.AspNetUsers.FirstOrDefault(u => u.UserName == id);
        }


        //public IEnumerable<AspNetUser> GetAllUsers()
        public IEnumerable<UserProfileViewModel> GetAllUsers()
        {
            //throw new NotImplementedException();
            //return context.AspNetUsers;
            var result = from user in _context.AspNetUsers
                         join profile in _context.UserProfiles on user.UserProfile_Id equals profile.Id
                         join org in _context.Organizations on profile.OrgainzationId equals org.Id
                         join addres in _context.Addresses on org.AddressId equals addres.Id
                         join position in _context.Positions on profile.PositionId equals position.Id


                         
                         select new UserProfileViewModel()
                         {
                             Id = profile.Id,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             PhoneNumber = user.PhoneNumber,
                             UserName = user.UserName,
                             JoinDate = user.JoinDate,
                             
                             OrgainzationId = profile.OrgainzationId,
                             Name = org.Name,
                             Description = org.Description,
                             LogoImgUrl = org.LogoImgUrl,
                             AddressLine1 = addres.AddressLine1,
                             AddressLine2 = addres.AddressLine2,
                             City = addres.City,
                             ProvState = addres.ProvState,
                             PostCode = addres.PostZipCode,
                             Telephone = org.Telephone,
                             Position = position.PoistionTitle,

                             AvatarImgUrl = profile.AvatarImgUrl
                         };

            return result;
        }


        public UserProfileViewModel GetUserProfile(string username)
        {
            //throw new NotImplementedException();
            

            var result = from user in _context.AspNetUsers
                         join profile in _context.UserProfiles on user.UserProfile_Id equals profile.Id
                         join org in _context.Organizations on profile.OrgainzationId equals org.Id
                         join addres in _context.Addresses on org.AddressId equals addres.Id
                join position in _context.Positions on profile.PositionId equals position.Id
            
            
                         where user.UserName == username

                         select new UserProfileViewModel()
                         {
                             Id = profile.Id,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             PhoneNumber = user.PhoneNumber,
                             UserName = user.UserName,
                             JoinDate = user.JoinDate,
                             Level = user.Level,
                             OrgainzationId = profile.OrgainzationId,
                             Position = position.PoistionTitle,
                             Name = org.Name,
                             Description = org.Description,
                             LogoImgUrl = org.LogoImgUrl,
                             AddressLine1 = addres.AddressLine1,
                             AddressLine2 = addres.AddressLine2,
                             City = addres.City,
                             ProvState = addres.ProvState,
                             PostCode = addres.PostZipCode,
                             Telephone = org.Telephone,
                             
                             AvatarImgUrl = profile.AvatarImgUrl
                         };

            return result.FirstOrDefault();


            //var results = from user in context.AspNetUsers
            //              from profile in context.UserProfiles
            //              join org in context.Organizations on profile.OrgainzationId equals org.Id into profile_org
            //              where user.UserName == username
            //              select new UserProfileViewModel()
            //              {
            //                  Id = profile.Id,
            //                  FirstName = user.FirstName,
            //                  LastName = user.LastName,
            //                  Email = user.Email,
            //                  PhoneNumber = user.PhoneNumber,
            //                  UserName = user.UserName,
            //                  JoinDate = user.JoinDate,
            //                  OrgainzationId = profile.OrgainzationId,
            //                  AvatarImgUrl = profile.AvatarImgUrl
            //              };

            //return results.FirstOrDefault();






        }


        /// <exception cref="DbUpdateException">An error occurred sending updates to the database.</exception>
        public void UpdateUserProfile(string username)//, UserProfile profile)
        {
            var user = _context.AspNetUsers.FirstOrDefault(u => u.UserName == username);

            var userProfile = _context.UserProfiles.Where(p => p.Id == user.UserProfile_Id);

            _context.UserProfiles.Attach(userProfile.FirstOrDefault());

            var entry = _context.Entry(userProfile.FirstOrDefault());
            entry.Property(p => p.Id == user.UserProfile_Id).IsModified = true;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var msg = dbUpdateConcurrencyException.Message;
            }


        }

        //Not in user for now, for future user to update profile related attributes, not AspUser, which is handled with ASP.NET Identity
        public void UpdateUserProfile(string username, AspNetUser userInfo)
        {
            //throw new NotImplementedException();
            try
            {
                var user = _context.AspNetUsers.FirstOrDefault(u => u.UserName == username);

                if (user != null)
                {
                    user.FirstName = userInfo.FirstName;
                    user.LastName = userInfo.LastName;
                    user.Email = userInfo.Email;

                    _context.AspNetUsers.Attach(user);

                    var entry = _context.Entry(user);
                    entry.State = EntityState.Modified;
                }

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
                {
                    var msg = dbUpdateConcurrencyException.Message;
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
            

            //var userProfile = context.UserProfiles.Where(p => p.Id == user.UserProfile_Id);

            

        }


        public void UpdateUserAvatarImgUrl(string userName, string imgFileName)
        {
            //throw new NotImplementedException();

            try
            {
                var user = _context.AspNetUsers.Where(u=>u.UserName == userName);

                var userProfile = _context.UserProfiles.FirstOrDefault(p=>p.Id == user.FirstOrDefault().UserProfile_Id);


                if (userProfile != null) userProfile.AvatarImgUrl = imgFileName;

                var entry = _context.Entry(userProfile);
                entry.Property(p => p.AvatarImgUrl).IsModified = true;

            }
            catch (Exception ex)
            {
                
                throw;
            }
            

            

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var msg = dbUpdateConcurrencyException.Message;
            }

        }


        #endregion



        #region Organization


        public OrganizationViewModel GetOrganizationDetails(int id)
        {
            //throw new NotImplementedException();
            //return context.Organizations.FirstOrDefault(o => o.Id == id);

            var result = from org in _context.Organizations
                join addr in _context.Addresses on org.AddressId equals addr.Id
                where org.Id == id 
                select new OrganizationViewModel()
                {
                    Id = org.Id,
                    Name = org.Name,
                    Description = org.Description,
                    OrganizationType = org.OrganizationType,
                    LogoImgUrl = org.LogoImgUrl,
                    IsActive = org.IsActive,
                    AddressLine1 = addr.AddressLine1,
                    City = addr.City,
                    ProvState = addr.ProvState,
                    Telephone = org.Telephone,
                    PostZipCode = addr.PostZipCode,
                    MessageCode = org.MessageCode,
                    CreateDate = org.CreateDate,
                    UpdateDate = org.UpdateDate
                };

            return result.FirstOrDefault();


        }


        public void CreateOrganization(Organization organization, Address address)
        {
            organization.Address = address;

            _context.Organizations.Add(organization);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw;
            }
           

        }


        public IEnumerable<OrganizationViewModel> GetallOrganizations()
        {
            //throw new NotImplementedException();
            var result = from org in _context.Organizations
                         join addr in _context.Addresses on org.AddressId equals addr.Id
                         where org.IsActive
                         where org.Id != 1
                         select new OrganizationViewModel()
                         {
                             Id = org.Id,
                             Name = org.Name,
                             Description = org.Description,
                             LogoImgUrl = org.LogoImgUrl,
                             OrganizationType = org.OrganizationType,
                             IsActive = org.IsActive,
                             AddressLine1 = addr.AddressLine1,
                             City = addr.City,
                             ProvState = addr.ProvState,
                             Telephone = org.Telephone,
                             PostZipCode = addr.PostZipCode,
                             MessageCode = org.MessageCode
                         };

            return result;
        }
        

        public Organization GetOrganizationById(int id)
        {
            //throw new NotImplementedException();
            return _context.Organizations.Include("Address").FirstOrDefault(o => o.Id == id);
        }


        public void UpdateOrganization(Organization organization, Address address)
        {
            _context.Addresses.Attach(address);
            _context.Organizations.Attach(organization);

            var entry = _context.Entry(address);
            entry.State = EntityState.Modified;

            _context.SaveChanges();
        }


        public void UpdateOrganization(int id, Organization org, Address addr)
        {

            var orgForUpdate = _context.Organizations.FirstOrDefault(o => o.Id == id); //get existing organization

            orgForUpdate = org;

            var newAddr = addr;

            //addr = address;

            _context.Addresses.Attach(newAddr);
            _context.Organizations.Attach(orgForUpdate);

            var entryOrg = _context.Entry(orgForUpdate);
            entryOrg.State = EntityState.Modified;

            var entryAddr = _context.Entry(newAddr);
            entryAddr.State = EntityState.Modified;
            

            _context.SaveChanges();
        }


        public Address GetOrgAddress(int id)
        {
            throw new NotImplementedException();

            
        }


        public void UpdateUserOrgImgUrl(int id, string imgFileName)
        {
            //throw new NotImplementedException();

            try
            {
                var org = _context.Organizations.FirstOrDefault(o => o.Id == id);

                if (org != null) org.LogoImgUrl = imgFileName;

                var entry = _context.Entry(org);
                entry.Property(p => p.LogoImgUrl).IsModified = true;
            }
            catch (Exception ex)
            {

                throw;
            }




            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var msg = dbUpdateConcurrencyException.Message;
            }


        }

        public Organization GetOrganizationByCode(string code)
        {
            //throw new NotImplementedException();

            return _context.Organizations.FirstOrDefault(o => o.MessageCode == code);
        }

        public void CreateTrialRequest(TrialRequest request)
        {
            //throw new NotImplementedException();

            _context.TrialRequests.Add(request);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IEnumerable<TrialRequest> GetAllRequests()
        {

            var result = _context.TrialRequests.Where(p=>p.IsProcessed == false);

            return result;
        }

        public TrialRequest GetTrialRequestByrId(int id)
        {
            //throw new NotImplementedException();

            return _context.TrialRequests.FirstOrDefault(t => t.Id == id);
        }

        public Organization GetOrganizationByName(string name)
        {
            //throw new NotImplementedException();

            return _context.Organizations.FirstOrDefault(o => o.Name == name);
        }

        public void UpdateTrialRequest(int id)
        {
            //throw new NotImplementedException();
            var request = _context.TrialRequests.FirstOrDefault(r => r.Id == id);

            if (request != null)
            {
                request.IsProcessed = true;

                _context.SaveChanges();
            }

            
        }



        public void AddOrgAccount(int id)
        {
            //throw new NotImplementedException();

            Account account = new Account
            {
                OrganizationId = id,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                EffectiveDate = DateTime.Now,
                IsActive = true,
                PaymentHistoryId = 0
            };


            _context.Accounts.Add(account);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


        #endregion

















    }
}
