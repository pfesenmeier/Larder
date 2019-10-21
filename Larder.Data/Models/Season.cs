using Larder.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Larder.Data.Models
{
    public class Season : IAuthorID
    {
        //Properties
        public int ID { get; set; }
        public Guid AuthorID { get; set; }
        public bool Winter { get; set; }
        public bool Summer { get; set; }
        public bool Spring { get; set; }
        public bool Fall { get; set; }

        //Methods
        public List<string> GetSeasons()
        {
            List<string> Seasons = new List<string>();
            if (Winter) { Seasons.Add("Winter"); }
            if (Summer) { Seasons.Add("Summer"); }
            if (Spring) { Seasons.Add("Spring"); }
            if (Fall) { Seasons.Add("Fall"); }

            return Seasons;
        }
        public override string ToString()
        {
            var Seasons = GetSeasons();

            return string.Join(", ", Seasons);
        }
    }
}