using System;
using System.Collections.Generic;
using System.Text;

namespace Boutiquei.Models
{
    public class Product
    {
        public string PID { set; get; }

        public string PName { set; get; }

        public string Price { set; get; }

        public List<string> PImgs { get; set; }

        public List<string> Sizes { set; get; }

        public List<string> Colors { set; get; }
    }
}
