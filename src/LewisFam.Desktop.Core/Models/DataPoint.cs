using System;
using System.Collections.ObjectModel;

namespace LewisFam.Desktop.Core.Models
{       
    public class DataPoint
    {
        public virtual double Value { get; set; }

        public virtual string Category { get; set; }
    }
}
