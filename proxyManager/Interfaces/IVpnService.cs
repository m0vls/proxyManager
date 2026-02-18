using System;
using System.Collections.Generic;
using System.Text;

namespace proxyManager.Interfaces
{
    public interface IVpnService
    {
        void StartVpn();
        
        void StopVpn();
        bool IsRunning {  get; }
    }
}
