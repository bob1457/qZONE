using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using quZONE.Data.Interfaces;
using quZONE.Domain.Services;

namespace quZONE.Services.WaitList
{
    public class WaitTimeService : IWaitTimeService
    {
        private readonly IWaitListRepository _waitListRepository;
        private readonly ITableManagementRepository _tableManagementRepository;

        public WaitTimeService(IWaitListRepository waitListRepository, ITableManagementRepository tableManagementRepository)
        {
            _waitListRepository = waitListRepository;
            _tableManagementRepository = tableManagementRepository;
        }

        private string GetWaitTimeOnWeekDay(int HourOfDay)
        {
            

            var WaitTime = "";

            if (HourOfDay < 11)
            {
                WaitTime = "10";
            }

            if (HourOfDay >= 11 && HourOfDay < 12)
            {
                WaitTime = "20";
            }

            if (HourOfDay >= 12 && HourOfDay < 14)
            {
                WaitTime = "30";
            }

            if (HourOfDay >= 14 && HourOfDay < 16)
            {
                WaitTime = "10";
            }

            if (HourOfDay > 16 && HourOfDay < 21)
            {
                WaitTime = "30";
            }


            if (HourOfDay > 21 )
            {
                WaitTime = "0";
            }

            return WaitTime;
        }


        private string GetWaitTimeOnWeekend(int HourOfDay)
        {


            var WaitTime = "";

            if (HourOfDay < 11)
            {
                WaitTime = "20";
            }

            if (HourOfDay >= 11 && HourOfDay < 12)
            {
                WaitTime = "30";
            }

            if (HourOfDay >= 12 && HourOfDay < 14)
            {
                WaitTime = "40";
            }

            if (HourOfDay >= 14 && HourOfDay < 16)
            {
                WaitTime = "20";
            }

            if (HourOfDay > 16 && HourOfDay < 21)
            {
                WaitTime = "30";
            }


            if (HourOfDay > 21)
            {
                WaitTime = "0";
            }

            return WaitTime;
        }


        public string GetWaitTime(int hourOfDay, DayOfWeek weekDay)
        {
            //throw new NotImplementedException();

            //var waitTime = "";

            if ((weekDay == DayOfWeek.Saturday) || (weekDay == DayOfWeek.Sunday))
            {
                return GetWaitTimeOnWeekend(hourOfDay);
                
            }
            else
            {
                return GetWaitTimeOnWeekDay(hourOfDay);
                
            }

            
        }


        public string CalculateWaitTime(int oid, int groupSize, int tableCapacity)
        {
            //Get number of guest group ahead in the waitlist

            var numberOfGuests = _waitListRepository.GetNumberOfGuestInWaitList(oid);

            //required table capacity

            var requiredTableSize = 0;

            if (groupSize == 1 || groupSize ==2)
            {
                requiredTableSize = 2;
            }


            if (groupSize == 3 || groupSize == 4)
            {
                requiredTableSize = 4;
            }


            if (groupSize == 5 || groupSize == 6)
            {
                requiredTableSize = 6;
            }


            if (groupSize == 7 || groupSize == 8)
            {
                requiredTableSize = 8;
            }


            if (groupSize >=9)
            {
                requiredTableSize = 10;
            }

            

            //To find number of available tables

            var availableTables = _tableManagementRepository.GetNumberOfAvailableTables(oid, requiredTableSize).Count();


            //Get all table with this capacity

            var tableWithCapacity = _tableManagementRepository.GetTablesByCapacity(oid, tableCapacity).Count();



            if (availableTables == 0) //if there is no empty tables, calculate time
            {

                return "";
            }
            else
            {
                return "5";
            }

            
        }

        

        
    }
}
