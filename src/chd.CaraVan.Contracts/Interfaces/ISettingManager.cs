﻿using chd.UI.Base.Contracts.Interfaces.Services.Base;

namespace chd.CaraVan.Contracts.Interfaces
{
    public interface ISettingManager : IBaseClientSettingManager
    {
        Task<string> GetIPAddress(CancellationToken cancellationToken = default);
    }
}
