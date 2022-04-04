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

        public string BID { set; get; }

        public string PImgCover { set; get; }
    }


    public class PImgs
    {
        public string Pimg { set; get; }
    }

    public class Colors
    {
        public string PColor { set; get; }
    }
    public class Sizes
    {
        public string PSize { set; get; }
    }
}
