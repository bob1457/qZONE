using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quZONE.Data.Interfaces;
using quZONE.Domain;
using quZONE.Domain.Entities;
using quZONE.Domain.ViewModels;
using EntityState = System.Data.Entity.EntityState;


namespace quZONE.Data.Repositories
{
    public class TableManagementRepository : ITableManagementRepository
    {
        private qZONEDbContext context = new qZONEDbContext();

        public IEnumerable<GuestTable> GetGuestTablesByOrgId(int id)
        {
            return context.GuestTables.Where(t => t.OrganizationId == id && t.TableStatus != "Removed");
        }



        public void AddGuestTable(GuestTable table)
        {
            context.GuestTables.Add(table);
            context.SaveChanges();
        }


        public IEnumerable<GuestTable> GetEmptyTablesByOrgId(int id)
        {
            return context.GuestTables.Where(t => t.OrganizationId == id && t.TableStatus == "Available");
        }


        public GuestTable GetGuestTable(string tableNumber, int orgId)
        {
            //throw new NotImplementedException();
            return context.GuestTables.FirstOrDefault(t => t.TableNumber == tableNumber && t.OrganizationId == orgId);
        }


        public void UpdateTable(int id, GuestTable table)
        {
            //throw new NotImplementedException();

            context.GuestTables.Attach(table);

            var entry = context.Entry(table);

            entry.State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var msg = dbUpdateConcurrencyException.Message;
            }

        }


        public GuestTable GetGuestTableById(int id)
        {
            //throw new NotImplementedException();

            return context.GuestTables.FirstOrDefault(t => t.Id == id);
        }


        public Domain.ViewModels.GuestTableViewMode GetGuestTableDetails(int id, int oid)
        {
            //throw new NotImplementedException();
            var result = from guest in context.Guests
                join wlist in context.WalkInWaitLists on guest.Id equals wlist.GuestId
                join table in context.GuestTables on wlist.ServedTableNumber equals table.TableNumber
                where table.Id == id && wlist.OrganizationId == oid
                select new GuestTableViewMode()
                {
                    GuestFirstName = guest.GuestFirstName,
                    GuestContactEmail = guest.GuestContactEmail,
                    GuestId = guest.Id,
                    GuestContactTel = guest.GuestContactTel,
                    GuestLastName = guest.GuestLastName,
                    GuestGroupSize = wlist.GuestGroupSize,
                    ServedTime = wlist.ServedTime
                    //ServedTime = table.LastUpdateTime
                    
                };

            return result.FirstOrDefault();

        }


        public GuestTableViewMode GetServedGuestInfo(string tableNo, int oid)
        {
            //throw new NotImplementedException();
            var result = from guest in context.Guests
                         join wlist in context.WalkInWaitLists on guest.Id equals wlist.GuestId
                        
                         join table in context.GuestTables on wlist.ServedTableNumber equals table.TableNumber
                     
                         where table.TableNumber == tableNo && wlist.OrganizationId == oid
                         select new GuestTableViewMode()
                         {
                             GuestFirstName = guest.GuestFirstName,
                             GuestId = guest.Id,
                             GuestContactTel = guest.GuestContactTel,
                             GuestLastName = guest.GuestLastName,
                             GuestGroupSize = wlist.GuestGroupSize,
                             ServedTime = wlist.ServedTime
                             //ServedTime = table.LastUpdateTime

                         };

            return result.FirstOrDefault();
        }


        //public GuestTable GetTableByNumberAndOrg(string tableNo, int oid)
        //{
        //    //throw new NotImplementedException();
        //    var table = context.GuestTables.FirstOrDefault(t => t.TableNumber == tableNo && t.OrganizationId == oid);

        //    return table;
        //}


        public void UpdateTableStatus(string tableNo, int oid)
        {
            //throw new NotImplementedException();
            var table =
                context.GuestTables.FirstOrDefault(t => t.TableNumber == tableNo && t.OrganizationId == oid);

            if (table != null)
            {
                table.TableStatus = "Occupied";

                context.GuestTables.Attach(table);
                var entry = context.Entry(table);
                entry.State = EntityState.Modified;

                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
                {
                    var msg = dbUpdateConcurrencyException.Message;
                }
            }
        }


        public IEnumerable<GuestTable> GetNumberOfAvailableTables(int oid, int capacity)
        {
            //throw new NotImplementedException();

            var talbeList = context.GuestTables.Where(t => t.OrganizationId == oid && t.MaxSeatsCapacity == capacity && t.TableStatus == "available");
            var tableCount = talbeList.Count();

            return talbeList;
        }


        public IEnumerable<GuestTable> GetTablesByCapacity(int oid, int capacity)
        {
            //throw new NotImplementedException();

            return context.GuestTables.Where(t => t.OrganizationId == oid && t.MaxSeatsCapacity == capacity);
        }


        
    }
}
