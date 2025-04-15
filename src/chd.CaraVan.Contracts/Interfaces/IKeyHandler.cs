using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Contracts.Interfaces
{
    public interface IKeyHandler
    {
        event EventHandler<bool> Key1;
        event EventHandler<bool> Key2;
        event EventHandler<bool> Key3;
        event EventHandler<bool> Key4;
    }
}
