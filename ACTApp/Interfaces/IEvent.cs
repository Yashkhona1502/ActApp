using System;
using ACTApp.Models.Event;

namespace ACTApp.Interfaces
{
    public interface IEvent
    {
        public AddEvent AddEvent(AddEvent addEvent);

        public List<AddEvent> GetAllEvents();

        public AddEvent GetEventById(int Id);

        public AddEvent EditEvent(AddEvent editEvent);

        public List<AddEvent> GetEventForDropDown();
    }
}

