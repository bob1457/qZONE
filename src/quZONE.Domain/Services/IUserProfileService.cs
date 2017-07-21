﻿using System.Collections;
using System.Collections.Generic;
using quZONE.Domain.Entities;
using quZONE.Domain.ViewModels;

namespace quZONE.Domain.Services
{
    public interface IUserProfileService
    {
        //UserProfile GetUserProfile(int id);
        //string UserName();

        IEnumerable<UserProfileViewModel> GetAllUserProfile();

        UserProfileViewModel GetUserProfile(string username);

        //void UpdateUserProfile(string username, UserProfile profile);

        void UpdateUserProfile(string username, string imgFileName);

        void UpdateUserProfile(string username, UserProfileViewModel profile);
        

        void UpdateUserAvatarImgUrl(string username, string imgFileName);

        void UpdateUserOrgImgUrl(int  id, string imgFileName);

        void UpdateOrganization(OrganizationViewModel organization);

        void UpdateOrganization(int id, OrganizationViewModel organization);

        AspNetUser GetUser(string id);

        Organization GetOrganizationById(int id);

        Account GetOrgAccount(int id);

        AccountViewModel GetAccountInfo(int id);

        Organization GetOrganizationByName(string name);

        void AddRequest(TrialRequest request);

        void UpdateRequest(int id);

        void ActivateAccount(int id, int pymnt);

        IEnumerable<UserProfileViewModel> GetAllUsers();

        IEnumerable<OrganizationViewModel> GetallOrganizations();
        void AddOrganization(OrganizationViewModel organization);
        OrganizationViewModel ShowOrganization(int id);

        Organization GetOrganizationByCode(string code);

        IEnumerable<TrialRequest> GetAllTrialRequests();

        TrialRequest GetTrialRequestById(int id);


    }
}