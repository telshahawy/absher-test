﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Utility.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
           : base(message)
        {
        }

        public BusinessException(string message, Exception ex)
           : base(message, ex)
        {
        }

    }
}
