using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketLuanr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Base
{
    public class LunarServerBase : AppServer<LunarSession, LunarRequestInfo>
    {
        public LunarServerBase()
            : base(new DefaultReceiveFilterFactory<LunarReceiveFilter, LunarRequestInfo>())
        {
        }
    }
}
