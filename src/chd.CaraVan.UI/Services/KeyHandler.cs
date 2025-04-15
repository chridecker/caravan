using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.UI.Services
{
    public class KeyHandler : IKeyHandler
    {
        private readonly IPiManager _piManager;

        public event EventHandler<bool> Key1;
        public event EventHandler<bool> Key2;
        public event EventHandler<bool> Key3;
        public event EventHandler<bool> Key4;

        public KeyHandler(IPiManager piManager)
        {
            this._piManager = piManager;
            piManager.PinChanged += this.PiManager_PinChanged;
        }

        private void PiManager_PinChanged(object? sender, PinChangedEventArgs e)
        {
            switch (e.Pin, e.Rising)
            {
                case (17, _):
                    this.Key1?.Invoke(this, e.Rising);
                    break;
                case (22, _):
                    this.Key1?.Invoke(this, e.Rising);
                    break;
                case (23, _):
                    this.Key1?.Invoke(this, e.Rising);
                    break;
                case (27, _):
                    this.Key1?.Invoke(this, e.Rising);
                    break;
                default:
                    break;
            }
        }
    }
}
