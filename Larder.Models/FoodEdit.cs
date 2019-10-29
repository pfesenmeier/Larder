using Larder.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Larder.Models
{
    public abstract class FoodEdit : FoodCreate, INameDescription
    {
        public int ID { get; set; }
    }
}
