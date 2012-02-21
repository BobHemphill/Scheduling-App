using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Messages;
using DomainLayer;

namespace ServiceLayer {
    public static class MessageFactory {
        public static CalendarYearMessage CreateCalendarYearMessage() {
            return new CalendarYearMessage(DomainFactory.CreateCalendarYear());
        }
    }
}
