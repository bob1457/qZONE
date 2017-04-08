using System.Collections.Generic;
using quZONE.Domain.Entities;
using quZONE.Domain.ViewModels;


namespace quZONE.Domain.Services
{
    public interface ITableManagementService
    {
        IEnumerable<GuestTable> GetGuestTablesByOrgId(int id);
        IEnumerable<GuestTable> GetEmptyTablesByOrgId(int id);
        GuestTableViewMode GetGuestTableDetails(int id, int oid); //id: table id, oid: org id

        GuestTableViewMode GetServedGuestInfor(string tableNo, int oik);

        IEnumerable<GuestTable> GetNumberOfAvailableTables(int oid, int groupSize);

        IEnumerable<GuestTable> GetTablesByCapacity(int oid, int capacity);

        void UpdateTable(int id, GuestTable table);
        void UpdateTableStatus(string tNo, int oid);
        

        void AddTable(GuestTable table);
    }
}