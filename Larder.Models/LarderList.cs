using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public class LarderList
    {
        public IEnumerable<LarderListItem> Larders { get; set; }
        public SeasonFilter SeasonFilter { get; set; }
    }
}
