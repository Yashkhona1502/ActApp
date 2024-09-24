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
using ACTApp.Models;
using Newtonsoft.Json;

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
            ViewBag.EventList = "";
            ViewBag.EventList = new SelectList(@event.GetEventForDropDown().ToList(), "EventId", "EventName");
            return View("~/Views/Accomodation/_Accomodation.cshtml");
        }
        [HttpPost]
        public IActionResult ImportExcel(FileUploadModel fileUpload)
        {
            DataTable table = new DataTable();
            ViewBag.EventList = "";
            try
            {
                if (fileUpload.File != null)
                {
                    using (var stream = fileUpload.File.OpenReadStream())
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelPackage package = new ExcelPackage();
                        package.Load(stream);
                        if (package.Workbook.Worksheets.Count > 0)
                        {
                            using (ExcelWorksheet workSheet = package.Workbook.Worksheets.First())
                            {
                                table = workSheet.Cells[1, 1, workSheet.Dimension.End.Row, workSheet.Dimension.End.Column].ToDataTable(c =>
                                {
                                    c.FirstRowIsColumnNames = true;
                                });
                            }
                        }
                        stream.Dispose();
                    }
                    table.Columns.Add("EventID");
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        table.Rows[i]["EventID"] = Request.Form["EventList"].ToString();
                    }
                    @hotel.AddHotelList(table);
                    ViewBag.EventList = new SelectList(@event.GetEventForDropDown().ToList(), "EventId", "EventName");
                    TempData["AlertSuccessMessage"] = "Sucessfully Saved";
                }
            }
            catch (Exception ex)
            {
                TempData["AlertErrorMessage"] = "Error! Failed to load data";
            }
            return RedirectToAction("Index", "Accomodation");
        }
        [HttpPost]
        public IActionResult GetHotelList(int eventId)
        {
            if (eventId != 0)
            {
                var hotelList = hotel.GetHotelList(eventId);
                return Json(hotelList);
            }
            else
            {
                return Json(new { Error = "" });
            }
        }
        [HttpPost]
        public ActionResult GetHotelListView([FromBody]List<AccommodationModel> hotelModel)
        {
            string json = JsonConvert.SerializeObject(hotelModel);
            return Content(json, "application/json");
        }
    }
}

