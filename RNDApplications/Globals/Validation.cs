using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RNDApplications.Globals
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false,
    AllowMultiple = false)]
    public sealed class DateOnlyAttribute : ValidationAttribute
    {
        public DateOnlyAttribute() :
            base("\"{0}\" must be a date without time portion.")
        {
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value.GetType() == typeof(DateTime))
                {
                    DateTime dateTime = (DateTime)value;
                    return dateTime.TimeOfDay == TimeSpan.Zero;
                }
                else if (value.GetType() == typeof(Nullable<DateTime>))
                {
                    DateTime? dateTime = (DateTime?)value;
                    return !dateTime.HasValue
                        || dateTime.Value.TimeOfDay == TimeSpan.Zero;
                }
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}