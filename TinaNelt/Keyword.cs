using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinaNelt
{
    public class Keyword
    {
        private Divisions division;
        private string actualKeyword;

        public Divisions Division { get; set; }
        public string ActualKeyword { get; set; }


        public Keyword(Divisions div, string keyword)
        {
            Division = div;
            ActualKeyword = keyword;
        }

    }
}
