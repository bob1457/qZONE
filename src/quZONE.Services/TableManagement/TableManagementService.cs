using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quZONE.Data.Interfaces;
using quZONE.Domain.Entities;
using quZONE.Domain.Services;

namespace quZONE.Services.TableManagement
{
    public class TableManagementService : ITableManagementService
    {
        private readonly ITableManagementRepository _tableManagementRepository;

        public TableManagementService(ITableManagementRepository tableManagementRepository)
        {
            _tableManagementRepository = tableManagementRepository;
        }


        public IEnumerable<GuestTable> GetGuestTablesByOrgId(int id)
        {
            var tables = _tableManagementRepository.GetGuestTablesByOrgId(id);
            return tables;
        }



        public void AddTable(GuestTable table)
        {
            _tableManagementRepository.AddGuestTable(table);
        }


        public IEnumerable<GuestTable> GetEmptyTablesByOrgId(int id)
        {
            //throw new NotImplementedException();
            return _tableManagementRepository.GetEmptyTablesByOrgId(id);
        }


        public void UpdateTable(int id, GuestTable table)
        {
            //throw new NotImplementedException();

            var newTable = _tableManagementRepository.GetGuestTableById(id);

            newTable.UpdateDate = DateTime.Now;
            newTable.LastUpdateTime = DateTime.Now;
            newTable.TableStatus = table.TableStatus;
            newTable.TableSize = table.TableSize;
            newTable.MaxSeatsCapacity = table.MaxSeatsCapacity;

            _tableManagementRepository.UpdateTable(id, newTable);
        }


        public Domain.ViewModels.GuestTableViewMode GetGuestTableDetails(int id, int oid)
        {
            //throw new NotImplementedException();
            return _tableManagementRepository.GetGuestTableDetails(id, oid);
        }


        public Domain.ViewModels.GuestTableViewMode GetServedGuestInfor(string tableNo, int oik)
        {
            //throw new NotImplementedException();
            return _tableManagementRepository.GetServedGuestInfo(tableNo, oik);
        }


        //public GuestTable GetTableByNumberAndOrg(string tableNo, int oid)
        //{
        //    throw new NotImplementedException();

        //}


        public void UpdateTableStatus(string tNo, int oid) //For anonymous walk-in guests only
        {
            //throw new NotImplementedException();

            _tableManagementRepository.UpdateTableStatus(tNo, oid);
        }


        public IEnumerable<GuestTable> GetNumberOfAvailableTables(int oid, int groupSize)
        {
            //throw new NotImplementedException();

            return _tableManagementRepository.GetNumberOfAvailableTables(oid, groupSize);
            
        }

        public IEnumerable<GuestTable> GetTablesByCapacity(int oid, int capacity)
        {
            return _tableManagementRepository.GetTablesByCapacity(oid, capacity);
        }
    }
}
