using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACTApp.Interfaces;
using ACTApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using OfficeOpenXml;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ACTApp.Controllers
{
    public class AccomodationController : Controller
    {
        private readonly IEvent @event;
        private readonly IHotel @hotel;

        public AccomodationController(IEvent @event, IHotel @hotel)
        {
            this.@event = @event;
            this.@hotel = @hotel;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.EventList = new SelectList(@event.GetEventForDropDown().ToList(),"EventId", "EventName");
            return View("~/Views/Accomodation/_Accomodation.cshtml");
        }
        [HttpPost]
        public IActionResult ImportExcel(FileUploadModel fileUpload)
        {
            DataTable table = new DataTable();
            try
            {
                if(fileUpload.File != null)
                {
                    using (var stream = fileUpload.File.OpenReadStream())
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelPackage package = new ExcelPackage();
                        package.Load(stream);
                        if(package.Workbook.Worksheets.Count > 0)
                        {
                            using (ExcelWorksheet workSheet = package.Workbook.Worksheets.First())
                            {
                                table = workSheet.Cells[1, 1, workSheet.Dimension.End.Row, workSheet.Dimension.End.Column].ToDataTable(c =>
                                {
                                    c.FirstRowIsColumnNames = true;
                                });
                            }
                        }
                    }
                    table.Columns.Add("EventID");
                    for(int i = 0; i < table.Rows.Count; i++)
                    {
                        table.Rows[i]["EventID"] = Request.Form["EventList"].ToString();
                    }
                    @hotel.AddHotelList(table);
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return null;
        }
    }
}

