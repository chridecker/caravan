﻿using chd.CaraVan.Contracts.Dtos;
using chd.CaraVan.Devices.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.CaraVan.Devices.Implementations
{
    public class VotronicDataService : IVotronicDataService
    {
        private VotronicBatteryData _votronicBatteryData;

        private VotronicSolarData _votronicSolarData;

        public Task AddData(VotronicSolarData votronicSolarData, CancellationToken cancellationToken = default) => Task.Run(() => { this._votronicSolarData = votronicSolarData; }, cancellationToken);
        public Task AddData(VotronicBatteryData votronicBatteryData, CancellationToken cancellationToken = default) => Task.Run(() => { this._votronicBatteryData = votronicBatteryData; }, cancellationToken);
        public Task<VotronicBatteryData> GetBatteryData(CancellationToken cancellationToken = default) => Task.FromResult(this._votronicBatteryData);
        public Task<VotronicSolarData> GetSolarData(CancellationToken cancellationToken = default) => Task.FromResult(this._votronicSolarData);
    }
}