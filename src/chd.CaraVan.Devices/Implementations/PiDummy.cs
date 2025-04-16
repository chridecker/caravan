using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Devices.Implementations
{
    public class PiDummy : IPiManager
    {
        public event EventHandler<PinChangedEventArgs> PinChanged;

        public Task<bool> Read(int pin, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Start(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Stop(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Write(PinWriteDto dto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
