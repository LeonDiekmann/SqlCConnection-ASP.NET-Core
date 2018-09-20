using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlCConnection_ASP_Net_Core.Interfaces
{
    public interface IMessageHelper
    {
        bool SendIntercom(string message);

    }
}
