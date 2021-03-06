﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quZONE.Domain.Entities;
using quZONE.Domain.ViewModels;

namespace quZONE.Data.Interfaces
{
    public interface IUserProfileRepository
    {
        IEnumerable<UserProfileViewModel> GetAllUserProfiles();

        UserProfile GetUserProfileById(int id);

        string GetName();

        AspNetUser GetUser(string id);

        IEnumerable<UserProfileViewModel> GetAllUsers();

        UserProfileViewModel GetUserProfile(string username);

        //void UpdateUserProfile(string username, UserProfile profile);

        void UpdateUserProfile(string username);

        void UpdateUserProfile(string username, AspNetUser user);

        void UpdateUserAvatarImgUrl(string userName, string imgFileName);

        void UpdateUserOrgImgUrl(int id, string imgFileName);

        void UpdateOrganization(Organization organization, Address address);

        void UpdateOrganization(int id, Organization organization, Address address);

        IEnumerable<OrganizationViewModel> GetallOrganizations();

        Address GetOrgAddress(int id);

        Organization GetOrganizationById(int id);

        OrganizationViewModel GetOrganizationDetails(int id); //id: organization id

        void CreateOrganization(Organization organization, Address address);

        Organization GetOrganizationByCode(string code);
    }

}