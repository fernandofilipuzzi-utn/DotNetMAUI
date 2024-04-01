using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public partial class CameraAndStorage : Permissions.BasePlatformPermission
    {
        protected override Func<IEnumerable<string>> RequiredInfoPlistKeys =>
               () => new string[] { "NSBluetoothAlwaysUsageDescription" };
    }
}
