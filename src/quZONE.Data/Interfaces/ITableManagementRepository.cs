using System.Collections.Generic;
using quZONE.Domain.Entities;
using quZONE.Domain.ViewModels;

namespace quZONE.Data.Interfaces
{
    public interface ITableManagementRepository
    {
        IEnumerable<GuestTable> GetGuestTablesByOrgId(int id);

        IEnumerable<GuestTable> GetEmptyTablesByOrgId(int id);

        GuestTable GetGuestTable(string tableNumber, int orgId);

        GuestTable GetGuestTableById(int id);

        IEnumerable<GuestTable> GetTablesByCapacity(int oid, int capacity);

        IEnumerable<GuestTable>  GetNumberOfAvailableTables(int oid, int capacity);

        GuestTableViewMode GetGuestTableDetails(int id, int oid); //id: table id, oid: org id

        GuestTableViewMode GetServedGuestInfo(string tableNo, int oid);

        void UpdateTable(int id, GuestTable table);
        
        void UpdateTableStatus(string tableNo, int oid);

        void AddGuestTable(GuestTable table);
    }
}