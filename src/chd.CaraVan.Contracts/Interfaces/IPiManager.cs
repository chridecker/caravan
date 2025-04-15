using chd.CaraVan.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Contracts.Interfaces
{
     public interface IPiManager
     {
        event EventHandler<PinChangedEventArgs> PinChanged;

        Task<bool> Read(int pin, CancellationToken cancellationToken = default);
         Task Start(CancellationToken cancellationToken = default);
         Task Stop(CancellationToken cancellationToken = default);
         Task Write(PinWriteDto dto, CancellationToken cancellationToken = default);
     }
}
