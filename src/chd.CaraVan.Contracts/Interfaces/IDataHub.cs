﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Contracts.Interfaces
{
     public interface IDataHub
    {
        Task RuuviTagData();
        Task VotronicData();
    }
}
