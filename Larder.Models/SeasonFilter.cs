using Larder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class SeasonFilter
    {
        public bool Winter { get; set; }
        public bool Spring { get; set; }
        public bool Fall { get; set; }
        public bool Summer { get; set; }

        public string ControllerName { get; set; }
    }
}
