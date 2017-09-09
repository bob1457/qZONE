using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using quZONE.Data.Interfaces;
using quZONE.Domain;
using quZONE.Domain.Entities;
using quZONE.Domain.ViewModels;
using EntityState = System.Data.Entity.EntityState;

namespace quZONE.Data.Repositories
{
    public class WaitListRepository : IWaitListRepository
    {
        private qZONEDbContext context = new qZONEDbContext();


        public IEnumerable<WaitListViewModel> GetAllWaitListByOrgnization(int id) 
        {
            DateTime today = DateTime.Today;


            var result = from waitlist in context.WalkInWaitLists
                join guest in context.Guests on waitlist.GuestId equals guest.Id
                         //where (waitlist.OrganizationId == id && DbFunctions.TruncateTime(waitlist.CreateDate) == today)// && waitlist.WaitingStatus == "Waiting")
                         where waitlist.WaitingStatus == "Served" && waitlist.OrganizationId == id //Get only served lists for billing purposes - may use a different method?
                select new WaitListViewModel()
                {
                    GuestFirstName = guest.GuestFirstName,
                    GuestLastName = guest.GuestLastName,
                    GuestContactTel = guest.GuestContactTel,
                    ArrivalTime = waitlist.ArrivalTime,
                    StatusChangeTime = waitlist.StatusChangeTime,
                    GuestId = guest.Id,
                    OrganizationId = waitlist.OrganizationId,
                    IsActive = waitlist.IsActive,
                    GuestGroupSize = waitlist.GuestGroupSize,
                    CreateDate = waitlist.CreateDate,
                    UpdateDate = waitlist.UpdateDate,
                    ServedTime = waitlist.ServedTime,
                    WaitingStatus = waitlist.WaitingStatus,
                    Notes = waitlist.Notes
                };

            return result;

        }

        public IEnumerable<WaitListViewModel> GetCurrentWaitListByOrgnization(int id) //Get the waitlist for the current day
        {
            DateTime today = DateTime.Today;


            var result = from waitlist in context.WalkInWaitLists
                         join guest in context.Guests on waitlist.GuestId equals guest.Id
                         where (waitlist.OrganizationId == id && DbFunctions.TruncateTime(waitlist.CreateDate) == today)// && waitlist.WaitingStatus == "Waiting")
                         select new WaitListViewModel()
                         {
                             GuestFirstName = guest.GuestFirstName,
                             GuestLastName = guest.GuestLastName,
                             GuestContactTel = guest.GuestContactTel,
                             ArrivalTime = waitlist.ArrivalTime,
                             StatusChangeTime = waitlist.StatusChangeTime,
                             GuestId = guest.Id,
                             OrganizationId = waitlist.OrganizationId,
                             IsActive = waitlist.IsActive,
                             GuestGroupSize = waitlist.GuestGroupSize,
                             CreateDate = waitlist.CreateDate,
                             UpdateDate = waitlist.UpdateDate,
                             ServedTime = waitlist.ServedTime,
                             WaitingStatus = waitlist.WaitingStatus,
                             Notes = waitlist.Notes
                         };

            return result;

        }


        public IEnumerable<WaitListViewModel> AllWaitListByOrgnization(int id)
        {
            //throw new NotImplementedException();

            var result = from waitlist in context.WalkInWaitLists
                         join guest in context.Guests on waitlist.GuestId equals guest.Id
                         //where (waitlist.OrganizationId == id && DbFunctions.TruncateTime(waitlist.CreateDate) == today)// && waitlist.WaitingStatus == "Waiting")
                         where waitlist.OrganizationId == id
                         select new WaitListViewModel()
                         {
                             GuestFirstName = guest.GuestFirstName,
                             GuestLastName = guest.GuestLastName,
                             GuestContactTel = guest.GuestContactTel,
                             ArrivalTime = waitlist.ArrivalTime,
                             StatusChangeTime = waitlist.StatusChangeTime,
                             GuestId = guest.Id,
                             OrganizationId = waitlist.OrganizationId,
                             IsActive = waitlist.IsActive,
                             GuestGroupSize = waitlist.GuestGroupSize,
                             CreateDate = waitlist.CreateDate,
                             UpdateDate = waitlist.UpdateDate,
                             ServedTime = waitlist.ServedTime,
                             WaitingStatus = waitlist.WaitingStatus,
                             Notes = waitlist.Notes
                         };

            return result;
        }



        public void AddToWaitList(WalkInWaitList list, Guest guest)
        {
            //throw new NotImplementedException();
            list.Guest = guest;
            context.WalkInWaitLists.Add(list);
            context.SaveChanges();
        }


        public WaitListViewModel GetWaitGuest(int id, int gid)
        {
            //return context.WalkInWaitLists.FirstOrDefault(l => l.Id == id && l.OrganizationId == oid);
            var result = from waitlist in context.WalkInWaitLists
                         join guest in context.Guests on waitlist.GuestId equals guest.Id
                         where (waitlist.OrganizationId == id && waitlist.GuestId == gid)
                         select new WaitListViewModel()
                         {
                             GuestFirstName = guest.GuestFirstName,
                             GuestLastName = guest.GuestLastName,
                             GuestContactTel = guest.GuestContactTel,
                             //ArrivalTime = waitlist.ArrivalTime,
                             StatusChangeTime = waitlist.StatusChangeTime,
                             GuestId = guest.Id,
                             OrganizationId = waitlist.OrganizationId,
                             //IsActive = waitlist.IsActive,
                             GuestGroupSize = waitlist.GuestGroupSize,
                             CreateDate = waitlist.CreateDate,
                             UpdateDate = waitlist.UpdateDate,
                             ServedTime = waitlist.ServedTime,
                             WaitingStatus = waitlist.WaitingStatus,
                             Notes = waitlist.Notes
                         };

            return result.FirstOrDefault();
        }


        public WalkInWaitList GetWalkInWaitListByGuestId(int id)
        {
            return context.WalkInWaitLists.FirstOrDefault(l =>l.GuestId == id);
        }

        public int UpdateWaitList(int id, WalkInWaitList list)
        {
            //throw new NotImplementedException();
            context.WalkInWaitLists.Attach(list);

            var entry = context.Entry(list);

            //Get assigned table
            //
            //var table = context.GuestTables.Where(t => t.TableNumber == list.ServedTableNumber);

            //var newTable = context.Entry(table);



            //newTable.State = EntityState.Modified;
            entry.State = EntityState.Modified;

            

            try
            {
                context.SaveChanges();

                return 0;
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var msg = dbUpdateConcurrencyException.Message;

                return 1;
            }

        }

        public void UpdateTableStatus(GuestTable table)
        {
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

                throw;
            }
        }


        public Guest GetGuestById(long id)
        {
            //throw new NotImplementedException();
            return context.Guests.FirstOrDefault(g => g.Id == id);
        }


        public void UpdateInWaitListForClearTable(string tableNo, int oid)
        {
            //throw new NotImplementedException();

            var list =
                context.WalkInWaitLists.FirstOrDefault(l => l.ServedTableNumber == tableNo && l.OrganizationId == oid);


            if (list != null)
            {
                list.ServedTableNumber = null;
                list.StatusChangeTime = DateTime.Now;
                list.UpdateDate = DateTime.Now;

                context.WalkInWaitLists.Attach(list);

                var entry = context.Entry(list);
                
                entry.State = EntityState.Modified;
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;

                throw;
            }
        }


        public Guest GetGuestByTel(string tele)
        {
            //throw new NotImplementedException();
            return context.Guests.FirstOrDefault(t => t.GuestContactTel == tele);
        }


        public int GetNumberOfGuestInWaitList(int oid)
        {
            //throw new NotImplementedException();
            var guests = context.WalkInWaitLists.Count(o => o.OrganizationId == oid);

            return guests;
        }


        
    }
}
