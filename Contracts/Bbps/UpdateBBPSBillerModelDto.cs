﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Bbps
{
    public class UpdateBBPSBillerModelDto
    {
        public string billerid { get; set; }
        public int status { get; set; }
    }
}
