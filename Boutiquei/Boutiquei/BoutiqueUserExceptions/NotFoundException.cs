using System;
using System.Collections.Generic;
using System.Text;

namespace Boutiquei.BoutiqueUserExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException (string message) : base(message)
        {

        }
    }
}
