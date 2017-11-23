using System;
using System.Collections.Generic;
using quZONE.Domain.Entities;
using quZONE.Domain.ViewModels;

namespace quZONE.Data.Interfaces
{
    public interface IWaitListRepository
    {
        IEnumerable<WaitListViewModel> GetAllWaitListByOrgnization(int id);

        IEnumerable<WaitListViewModel> GetCurrentWaitListByOrgnization(int id);

        IEnumerable<WaitListViewModel> AllWaitListByOrgnization(int id);

        WaitListViewModel GetWaitGuest(int id, int oid);
        WalkInWaitList GetWalkInWaitListByGuestId(int id);

        // IEnumerable<WalkInWaitList> GetWaitListByOrgByMonthYear(int id, string monthYear);

        IEnumerable<WalkInWaitList> GetWaitListByOrgByMonthYear(int id, DateTime monthYear);

        IEnumerable<WaitListViewModel> GetWaitListByOrgByMonthYear(int id, string monthYear);
        

        Guest GetGuestById(long id);

        Guest GetGuestByTel(string tele);

        int UpdateWaitList(int id, WalkInWaitList list);

        int GetNumberOfGuestInWaitList(int oid);

        void UpdateTableStatus(GuestTable table);

        void AddToWaitList(WalkInWaitList list, Guest guest);
        void UpdateInWaitListForClearTable(string tableNo, int oid);
    }
}