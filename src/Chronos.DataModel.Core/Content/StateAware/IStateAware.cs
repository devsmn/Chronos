﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.DataModel.Core
{
    public interface IStateAware
    {
        CancellationToken CancellationToken { get; set; }
    }
}