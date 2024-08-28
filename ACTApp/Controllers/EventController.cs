using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ACTApp.Interfaces;
using ACTApp.Models.Event;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ACTApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IEvent @event;

        public EventController(IEvent @event)
        {
            this.@event = @event;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var getEvents = @event.GetAllEvents();
            return View("~/Views/Events/_Events.cshtml", getEvents.ToList());
        }
        [AllowAnonymous]
        public IActionResult Add()
        {
            return PartialView("~/Views/Events/_Add.cshtml", new AddEvent());
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Add(AddEvent addEvent)
        {
            if (ModelState.IsValid)
            {
                var AddEvent = new AddEvent
                {
                    EventName = addEvent.EventName,
                    StartDate = addEvent.StartDate,
                    EndDate = addEvent.EndDate,
                    Location = addEvent.Location,
                    Status = addEvent.Status
                };
                var addevent = @event.AddEvent(addEvent);

                var events = @event.GetAllEvents();
                return View("~/Views/Events/_Events.cshtml", events.ToList());
            }
            else
            {
                return View("~/Views/Events/_Add.cshtml", new AddEvent());
            }
        }
        [AllowAnonymous]
        public IActionResult Edit(int Id)
        {
            AddEvent getEvent = new AddEvent();
            getEvent = @event.GetEventById(Id);
                
            return View("~/Views/Events/_Edit.cshtml", getEvent);
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult EditEvent(AddEvent editEvent)
        {
            var EditEvent = new AddEvent {
                EventId = editEvent.EventId,
                EventName = editEvent.EventName,
                StartDate = editEvent.StartDate,
                EndDate = editEvent.EndDate,
                Location = editEvent.Location,
                Status = editEvent.Status
            };

            var updateEvent = @event.EditEvent(EditEvent);

            var events = @event.GetAllEvents();

            return View("~/Views/Events/_Events.cshtml", events.ToList()); ;
        }
    }
}

