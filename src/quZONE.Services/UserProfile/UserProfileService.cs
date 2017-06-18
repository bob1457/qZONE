using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using quZONE.Data.Interfaces;
using quZONE.Domain.Entities;
using quZONE.Domain.Services;
using quZONE.Domain.ViewModels;

namespace quZONE.Services.UserProfile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        //public string UserName()
        //{
        //    throw new NotImplementedException();
        //}

        #region User Profile

        public IEnumerable<UserProfileViewModel> GetAllUserProfile()
        {
            //throw new NotImplementedException();
            var profiles = _userProfileRepository.GetAllUserProfiles();
            return profiles;
        }


        public UserProfileViewModel GetUserProfile(string username)
        {
            //throw new NotImplementedException();
            var profile = _userProfileRepository.GetUserProfile(username);

            return profile;
        }

        //public void UpdateUserProfile(string username, Domain.Entities.UserProfile profile)
        //{
        //    var userProfile = new Domain.Entities.UserProfile();

        //    userProfile.AvatarImgUrl = profile.AvatarImgUrl;
        //    userProfile.AvatarImgUrlMd = "";
        //    userProfile.AvatarImgUrlSm = "";
        //    userProfile.OrgainzationId = profile.OrgainzationId;
        //    userProfile.ContactEmail = profile.ContactEmail;
        //    userProfile.PositionId = profile.PositionId;

        //    _userProfileRepository.UpdateUserProfile(username, userProfile);

        //}

        public void UpdateUserProfile(string username, string imgFileName)
        {
            //throw new NotImplementedException();
            _userProfileRepository.UpdateUserProfile(username);
        }
        

        public void UpdateUserAvatarImgUrl(string username, string imgFileName)
        {
            //throw new NotImplementedException();
            _userProfileRepository.UpdateUserAvatarImgUrl(username, imgFileName);
        }


        public Domain.Entities.AspNetUser GetUser(string id)
        {
            //throw new NotImplementedException();
            var user = _userProfileRepository.GetUser(id);

            return user;
        }



        public IEnumerable<UserProfileViewModel> GetAllUsers()
        {
            //throw new NotImplementedException();
            return _userProfileRepository.GetAllUsers();
        }
        public void UpdateUserProfile(string username, UserProfileViewModel profile)
        {
            //throw new NotImplementedException();

            var userInfo = new AspNetUser();

            userInfo.FirstName = profile.FirstName;
            userInfo.LastName = profile.LastName;
            userInfo.Email = profile.Email;

            _userProfileRepository.UpdateUserProfile(username, userInfo);

        }




        #endregion

        #region Organization

        public void AddOrganization(OrganizationViewModel organization )
        {
            //throw new NotImplementedException();
            Organization org = new Organization(organization.Notes, 
                organization.IsActive, 
                organization.LogoImgUrl, 
                organization.OrganizationType, 
                organization.Description, 
                organization.Name, 
                organization.Telephone,
                organization.MessageCode);

            org.CreateDate = DateTime.Now;
            org.UpdateDate = DateTime.Now;

            Address addr = new Address(organization.PostZipCode,
                organization.ProvState,
                organization.City,
                organization.AddressLine2,
                organization.AddressLine1);


            _userProfileRepository.CreateOrganization(org, addr);
            
        }

        public OrganizationViewModel ShowOrganization(int id)
        {
            //throw new NotImplementedException();

            return _userProfileRepository.GetOrganizationDetails(id);

        }

        public IEnumerable<OrganizationViewModel> GetallOrganizations()
        {
            //throw new NotImplementedException();
            return _userProfileRepository.GetallOrganizations();
        }


        public Organization GetOrganizationById(int id)
        {
            //throw new NotImplementedException();
            return _userProfileRepository.GetOrganizationById(id);
        }


        public void UpdateOrganization(OrganizationViewModel organization)
        {
            //throw new NotImplementedException();

            var orgForUpdate = _userProfileRepository.GetOrganizationById(organization.Id);

            try
            {
                orgForUpdate.Name = organization.Name;
                orgForUpdate.Description = organization.Description;
                orgForUpdate.Telephone = organization.Telephone;
                orgForUpdate.MessageCode = organization.MessageCode;
                organization.UpdateDate = DateTime.Now;

                orgForUpdate.Address.AddressLine1 = organization.AddressLine1;
                orgForUpdate.Address.City = organization.City;
                orgForUpdate.Address.ProvState = organization.ProvState;
                orgForUpdate.Address.PostZipCode = organization.PostZipCode;


                _userProfileRepository.UpdateOrganization(organization.Id, orgForUpdate, orgForUpdate.Address );
            }
            catch (Exception exception)
            {
                var msg = exception.Message;
                throw;
            }

            
            
        }

        public void UpdateOrganization(int id, OrganizationViewModel organization)
        {
            //throw new NotImplementedException();

            //var orgForUpdate = _userProfileRepository.GetOrganizationById(id);

            var orgUpdated = new Organization(organization.Notes,
                organization.IsActive,
                organization.LogoImgUrl,
                organization.OrganizationType,
                organization.Description,
                organization.Notes,
                organization.Telephone,
                organization.MessageCode);

            var address = new Address(organization.PostZipCode,
                organization.ProvState,
                organization.City,
                organization.AddressLine2,
                organization.AddressLine1);

            //organization.Name = organization.Name;
            //organization.Description = organization.Description;
            //organization.Telephone = organization.Telephone;
            //organization.MessageCode = organization.MessageCode;
            organization.UpdateDate = DateTime.Now;

            //organization.AddressLine1 = organization.AddressLine1;
            //organization.City = organization.City;
            //organization.ProvState = organization.ProvState;
            //organization.PostZipCode = organization.PostZipCode;

            _userProfileRepository.UpdateOrganization(id, orgUpdated, address);

        }

        public void UpdateUserOrgImgUrl(int id, string imgFileName)
        {
            //throw new NotImplementedException();

            _userProfileRepository.UpdateUserOrgImgUrl(id, imgFileName);
        }



        public Organization GetOrganizationByCode(string code)
        {
            //throw new NotImplementedException();
            return _userProfileRepository.GetOrganizationByCode(code);
        }


        public void AddRequest(TrialRequest request)
        {
            //throw new NotImplementedException();

            request.CreateDate = DateTime.Now;
            request.IsProcessed = false;

            _userProfileRepository.CreateTrialRequest(request);

        }


        public IEnumerable<TrialRequest> GetAllTrialRequests()
        {
            //throw new NotImplementedException();

            return _userProfileRepository.GetAllRequests();
        }



        public TrialRequest GetTrialRequestById(int id)
        {
            //throw new NotImplementedException();
            return _userProfileRepository.GetTrialRequestByrId(id);
        }



        public Organization GetOrganizationByName(string name)
        {
            //throw new NotImplementedException();
            return _userProfileRepository.GetOrganizationByName(name);
        }


        public void UpdateRequest(int id)
        {
            //throw new NotImplementedException();
            _userProfileRepository.UpdateTrialRequest(id);
        }



        public void ActivateAccount(int id)
        {
            //throw new NotImplementedException();

            _userProfileRepository.AddOrgAccount(id);
        }


        #endregion










































        
    }
}
