using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.Validation {
    public interface IValidate {
        bool Validate();
    }

    public interface IValidateChildren<T> {
        bool ValidateChildren(List<BaseObject<T>> children, T validationParams);
    }

    public class EmptyValidationParams{
    }

    public class DateRangeValidationParams {
        readonly DateTime startDate;
        public DateTime StartDate { get { return startDate; } }

        readonly DateTime endDate;
        public DateTime EndDate { get { return endDate; } }

        public DateRangeValidationParams(DateTime startDate, DateTime endDate) {
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}
