﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvmLessons.Models
{
    internal class Coin
    {
        public string CoinName { get; set; }

        public int CoinPrice { get; set; }


    }

    internal struct DataPoint
    {
        public double XValue { get; set; }

        public double YValue { get; set; }
    }
}