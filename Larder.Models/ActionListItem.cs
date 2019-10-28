using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class ActionListItem
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int LarderId { get; set; }
        public override string ToString() => Description;
    }
}
