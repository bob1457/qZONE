﻿using System.Collections.Generic;
using quZONE.Domain.Entities;
using quZONE.Domain.ViewModels;

namespace quZONE.Domain.Services
{
    public interface IWaitListService
    {
        IEnumerable<WaitListViewModel> GetWaitListByOrgId(int id);

        WaitListViewModel GetWaitGuest(int id, int oid);

        Guest GetGuestByTel(string tele);

        WalkInWaitList GetWaitList(string tableNo, int oid);

        Guest GetGuestFromWaitList(long id); //id; guest Id from the wait list

        void UpdateWaitList(int id, WalkInWaitList list);

        void AddToWaitList(WaitListViewModel waitList);

        void ClearTableInWaitList(string tableNo, int oid);
    }
}
