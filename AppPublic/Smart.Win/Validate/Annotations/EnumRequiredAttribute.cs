using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSolution.Utilities.Validate.Annotations
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && !"0".Equals(value.ToString());
        }
    }
}
