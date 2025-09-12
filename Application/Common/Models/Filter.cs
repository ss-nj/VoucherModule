using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Filter
    {//TODO create filter or use a package for filter all getall apis
        public string FieldName { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
