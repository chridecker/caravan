using chd.UI.Base.Contracts.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chd.Caravan.Mobile.UI.Interfaces
{
     public interface ISettingManager : IBaseClientSettingManager
        {
            T? GetNativSetting<T>(string key) where T : class;
            void SetNativSetting<T>(string key, T value) where T : class;
        }
}
