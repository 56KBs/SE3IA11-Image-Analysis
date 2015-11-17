﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompression.Encoders
{
    public interface ILZ77Store<T> where T : Interfaces.ILZ77able
    {
        bool shortForm { get; }
    }
}
