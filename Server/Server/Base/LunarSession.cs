using SuperSocket.SocketBase;
using SuperSocket.SocketLuanr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Base
{
    /// <summary>
    /// 服务器连接
    /// </summary>
    public class LunarSession : AppSession<LunarSession, LunarRequestInfo>
    {
        /// <summary>
        /// 唯一ID(当类型为客户端时为用户唯一ID)
        /// </summary>
        public long SessionUuid { get; set; }

        /// <summary>
        /// 连接状态 0=未登陆 1=已登陆 2=GM登陆
        /// </summary>
        public short SessionState { get; set; } = 0;
        #region 接收消息队列

        //消息锁
        public object LockerReq = new object();
        //消息队列
        public List<LunarRequestInfo> ListReq = new List<LunarRequestInfo>();

        #endregion

        #region 发送消息队列

        public class LunarSendInfo
        {
            public string ObjMsg;
            public bool IsShow;

            public LunarSendInfo(string msg, bool isshow)
            {
                ObjMsg = msg;
                IsShow = isshow;
            }
        }
        
        //消息锁
        public object LockerSend = new object();
        //消息队列
        public List<LunarSendInfo> ListSend = new List<LunarSendInfo>();
        //补发队列
        public List<LunarSendInfo> ListReSend = new List<LunarSendInfo>();

        #endregion

        // 发送消息
        public void Send(string objMsg, bool IsShow = true)
        {
            if (Connected)
            {
                lock (LockerSend)
                {
                    ListSend.Add(new LunarSendInfo(objMsg, IsShow));
                }
            }
        }
    }    
}
