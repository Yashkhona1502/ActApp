using System;
using ACTApp.Interfaces;
using ACTApp.Models.Event;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ACTApp.Models.DataContext;


namespace ACTApp.Repository.Event
{
    public class EventRepo : IEvent
    {
        public readonly DataContext context;

        public EventRepo(DataContext context, IDataProtectionProvider provider)
        {
            this.context = context;
        }
        public AddEvent AddEvent(AddEvent addEvent)
        {
            var result = context.tb_Event.Add(addEvent);
            context.SaveChanges();

            return new AddEvent();
        }
        public List<AddEvent> GetAllEvents()
        {
            var result = context.tb_Event.ToList();

            return result;
        }
        public AddEvent GetEventById(int Id)
        {
            var result = context.tb_Event.Where(x => x.EventId == Id).FirstOrDefault();

            return result;
        }
        public AddEvent EditEvent(AddEvent editEvent)
        {
            AddEvent events = context.tb_Event.Where(x => x.EventId == editEvent.EventId).FirstOrDefault();

            if (events != null)
            {
                events.EventName = editEvent.EventName;
                events.StartDate = editEvent.StartDate;
                events.EndDate = editEvent.EndDate;
                events.Location = editEvent.Location;
                events.Status = editEvent.Status;

                this.context.SaveChanges();
            }

            return new AddEvent();
        }
        public List<AddEvent> GetEventForDropDown()
        {
            var result = context.tb_Event.Select(x => new AddEvent()
            {
                EventId = x.EventId,
                EventName = x.EventName
            }).ToList();

            return result;
        }
    }
}

