using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quZONE.Domain.Services
{
    public interface IWaitTimeService
    {
        //string GetWaitTimeOnHourOfDay( int HourOfDay, int weekDay);

        string GetWaitTime(int hourOfDay, DayOfWeek weekDay);

        string CalculateWaitTime(int oid, int groupSize, int tableCapacity);
    }
}
