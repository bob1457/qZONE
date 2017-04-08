using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quZONE.Data.Interfaces;
using quZONE.Domain.Entities;
using quZONE.Domain.Services;
using quZONE.Domain.ViewModels;

namespace quZONE.Services.WaitList
{
    public class WaitListService : IWaitListService
    {
        private readonly IWaitListRepository _waitListRepository;
        private readonly ITableManagementRepository _tableManagementRepository;

        public WaitListService(IWaitListRepository waitListRepository, ITableManagementRepository tableManagementRepository)
        {
            _waitListRepository = waitListRepository;
            _tableManagementRepository = tableManagementRepository;
        }

        public void AddToWaitList(Domain.ViewModels.WaitListViewModel waitList)
        {
            //throw new NotImplementedException();

            WalkInWaitList list = new WalkInWaitList(
                waitList.GuestGroupSize,
                waitList.OrganizationId,
                waitList.WaitingStatus,
                waitList.IsActive
                
                );

            Guest guest = new Guest(
                waitList.GuestFirstName,
                waitList.GuestLastName,
                waitList.GuestContactTel,
                waitList.GuestContactEmail
                );

            
            _waitListRepository.AddToWaitList(list, guest);

        }

        public IEnumerable<Domain.ViewModels.WaitListViewModel>  GetWaitListByOrgId(int id)
        {
            //throw new NotImplementedException();
            var list = _waitListRepository.GetAllWaitListByOrgnization(id);

            return list;
        }

        public WaitListViewModel GetWaitGuest(int id, int gid)
        {
            var waitguest = _waitListRepository.GetWaitGuest(id, gid);

            return waitguest;
        }



        public void UpdateWaitList(int id, WalkInWaitList list)
        {
            //throw new NotImplementedException();

            //Get the list based on guestId
            //
            WalkInWaitList wList = _waitListRepository.GetWalkInWaitListByGuestId(id);

            wList.UpdateDate = DateTime.Now;
            wList.StatusChangeTime = DateTime.Now;
            wList.WaitingStatus = list.WaitingStatus;
            wList.ServedTime = DateTime.Now;

            if (wList.WaitingStatus == "Waiting" || wList.WaitingStatus == "Available")
            {
                wList.ServedTableNumber = "";
            }
            else
            {
                wList.ServedTableNumber = list.ServedTableNumber;
            }

            
            

            var completeCode = _waitListRepository.UpdateWaitList(id, wList);

            if (wList.WaitingStatus == "Served")
            {
                if (completeCode == 0)
                {
                    //Get table by table number
                    //
                    var table = _tableManagementRepository.GetGuestTable(wList.ServedTableNumber, wList.OrganizationId);

                    if (table != null)
                    {
                        table.TableStatus = "Occupied";
                        table.UpdateDate = DateTime.Now;
                        table.LastUpdateTime = DateTime.Now;
                        wList.ServedTime = DateTime.Now;

                        _waitListRepository.UpdateTableStatus(table);
                    }


                }
            }
            

        }

        public WalkInWaitList GetWaitList(string tableNo, int oid)
        {
            throw new NotImplementedException();



        }

        public void ClearTableInWaitList(string tableNo, int oid)
        {
            //throw new NotImplementedException();

            _waitListRepository.UpdateInWaitListForClearTable(tableNo, oid);
        }

        #region Guest

        public Guest GetGuestFromWaitList(long id)
        {
            //throw new NotImplementedException();
            return _waitListRepository.GetGuestById(id);
        }

        public Guest GetGuestByTel(string tele)
        {
            //throw new NotImplementedException();
            return _waitListRepository.GetGuestByTel(tele);
        }


        #endregion









        
    }
}
