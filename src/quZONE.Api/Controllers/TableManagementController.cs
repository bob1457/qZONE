using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using quZONE.Domain.Entities;
using quZONE.Domain.Services;

namespace quZONE.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/tablemanager")]
    public class TableManagementController : ApiController
    {
        private readonly ITableManagementService _tableManagementService;
        private readonly IWaitListService _waitListService;

        public TableManagementController(ITableManagementService tableManagementService, IWaitListService waitListService)
        {
            _tableManagementService = tableManagementService;
            _waitListService = waitListService;
        }


        [Route("table/{id:int}")]
        public IHttpActionResult GetGuestTableByOrgId(int id)
        {
            var tables = _tableManagementService.GetGuestTablesByOrgId(id);

            return Ok(tables);
        }

        [Route("emptytable/{id:int}")]
        public IHttpActionResult GetEmptyTableByOrgId(int id)
        {
            var tables = _tableManagementService.GetEmptyTablesByOrgId(id);

            return Ok(tables);
        }


        //[Route("table/details/{id:int}/{oid:int}")]
        //public IHttpActionResult GetTableDetails(int id, int oid)
        //{
        //    var info = _tableManagementService.GetGuestTableDetails(id, oid);

        //    return Ok(info);
        //}

        [Route("table/details/{tableNo}/{oid:int}")]
        public IHttpActionResult GetTableDetails(string tableNo, int oid)
        {
            var info = _tableManagementService.GetServedGuestInfor(tableNo, oid);

            return Ok(info);
        }


        [Route("table/add")]
        public IHttpActionResult AddGuestTable(GuestTable table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //set default values
            //
            table.TableStatus = "Available";
            table.CreateDate = DateTime.Now;
            table.UpdateDate = DateTime.Now;
            table.LastUpdateTime = DateTime.Now;

            _tableManagementService.AddTable(table);
            

            return Ok();
        }

        [Route("table/clear/{tableNo}/{oid:int}")]
        public IHttpActionResult ClearTable(string tableNo, int oid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _waitListService.ClearTableInWaitList(tableNo, oid);

            return Ok();

        }

        [Route("table/update/{id:int}")]
        public IHttpActionResult UpdateTable(int id, GuestTable table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _tableManagementService.UpdateTable(id, table);

            return Ok();
        }

        [Route("table/updateStatus/{tNo}/{oid:int}")]
        public IHttpActionResult UpdateTableStatus(string tNo, int oid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _tableManagementService.UpdateTableStatus(tNo, oid);

            return Ok();
        }
    }
}
