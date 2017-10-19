using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientLink
    {
        private Socket clientSocket = null;
        private byte[] ReadM = new byte[65536];
        public int recvoffest = 0;
        public bool Start()
        {
            try
            {
                //if (clientSocket != null)
                //{
                //    clientSocket.Close();
                //}
                clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);

                clientSocket.Connect("127.0.0.1", 1111);

                //连接后开始从服务器读取网络消息
                clientSocket.BeginReceive(ReadM, recvoffest, 65536 - recvoffest, SocketFlags.None, new System.AsyncCallback(ReceiveCallBack), ReadM);

                return true;
            }
            catch (SocketException)
            {
                clientSocket = null;
                return false;
            }
        }

        //接收网络消息回调函数
        private void ReceiveCallBack(System.IAsyncResult ar)
        {
            int readCount = 0;

            try
            {
                //读取消息长度
                readCount = clientSocket.EndReceive(ar);//调用这个函数来结束本次接收并返回接收到的数据长度。
                recvoffest += readCount;
                if (recvoffest > 65536)
                {
                    return;
                }

                if (readCount <= 0)
                {
                    recvoffest = 0;
                    return;
                }

                //DeCodePacket(ref ReadM, ref recvoffest);
            }
            catch (Exception ex)//出现Socket异常就关闭连接
            {
                recvoffest = 0;
                if (clientSocket == null) return;
                if (clientSocket.Connected)
                {
                    clientSocket.Shutdown(SocketShutdown.Receive);
                    clientSocket.Close(0);
                }
                else
                {
                    clientSocket.Close();
                }
                return;
            }
            clientSocket.BeginReceive(ReadM, recvoffest, 65536 - recvoffest, SocketFlags.None, new System.AsyncCallback(ReceiveCallBack), ReadM);
        }
        //向登录服务器发送数据
        public void Send(byte[] buf)
        {
            if (clientSocket == null)
            {
                return;
            }
            if (!clientSocket.Connected)
                return;
            //创建NetWorkStream
            NetworkStream ns;
            //加锁，避免在多线程下出问题
            lock (clientSocket)
            {
                ns = new NetworkStream(clientSocket);
            }
            if (ns.CanWrite)
            {
                try
                {
                    ns.BeginWrite(buf, 0, buf.Length, new System.AsyncCallback(SendCallBack), ns);
                }
                catch
                {
                    Disconnect(0);
                }
            }
        }

        private void SendCallBack(System.IAsyncResult ar)
        {
            NetworkStream ns = (NetworkStream)ar.AsyncState;
            try
            {
                ns.EndWrite(ar);
                ns.Flush();
                ns.Close();
            }
            catch
            {
                Disconnect(0);
            }
        }

        //关闭登录连接
        public void Disconnect(int timeout)
        {
            try
            {
                if (clientSocket == null) return;
                if (clientSocket.Connected)
                {
                    clientSocket.Shutdown(SocketShutdown.Receive);
                    clientSocket.Close(timeout);
                }
                else
                {
                    clientSocket.Close();
                }
            }
            catch
            {
            }
        }
    }
}
