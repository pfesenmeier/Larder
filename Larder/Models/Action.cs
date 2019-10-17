using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Larder.Models
{
    public class Action
    {
        //Property
        public string Text { get; set; }

        //Navigation Object
        public virtual Recipe Recipe { get; set; }
    }
}