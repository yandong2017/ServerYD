using Server.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketLuanr;
using ServerModel;

namespace Server
{
    class Program
    {
        private static Dictionary<long, LunarSession> m_DictSession = new Dictionary<long, LunarSession>();
        public static int ServerPort = 1111;
        // 构造一个EntityBase对象
        public static EntityBase ety = new EntityBase();
        static void Main(string[] args)
        {

            var server = new LunarServerBase();

            server.NewSessionConnected += Server_NewSessionConnected;
            server.SessionClosed += Server_SessionClosed;
            server.NewRequestReceived += Server_NewRequestReceived;
            if (!server.Setup(ServerPort))
            {
                Console.WriteLine("服务器设置失败！");
                Console.ReadKey();
                return;
            }

            if (!server.Start())
            {
                Console.WriteLine("服务器启动失败！");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("服务器启动成功！");


            // 添加TestComponent组件，可以移到EntityBase的继承类的构造函数中
            //TestComponent testComp = new TestComponent();
            //ety.AddComponent("TestComponent", testComp);

            Reload();

            while (true)
            {

            }


            

            //// 发送消息Event_01
            //Event evt1 = new Event();
            //evt1.EventID = (int)EventDefines.Event_01;
            //evt1.PushUserData<string>("SomeData For Event01");
            //ety.DispatchEvent(evt1);

            //// 发送消息Event_02
            //Event evt2 = new Event();
            //evt2.EventID = (int)EventDefines.Event_02;
            //evt2.PushUserData<string>("SomeData For Event02");
            //ety.DispatchEvent(evt2);

            //// 发送消息Event_03
            //Event evt3 = new Event();
            //evt3.EventID = (int)EventDefines.Event_03;
            //evt3.PushUserData<float>(0.123f);
            //ety.DispatchEvent(evt3);

            //Console.Read();
        }
        public static LunarSession ThisSession = new LunarSession();
        private static void Server_NewRequestReceived(LunarSession session, LunarRequestInfo requestInfo)
        {
            Reload();
            // 发送消息Event_01
            Event evt1 = new Event();
            evt1.EventID = (int)EventDefines.Event_01;
            evt1.PushUserData<string>("SomeData For Event01");
            
            ThisSession = session;

            ety.DispatchEvent(evt1);
        }

        private static void Reload()
        {
            var hot = DllHelper.GetHotfixAssembly();

            Type[] types = hot.GetTypes();
            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(ObjectEventAttribute), false);

                if (attrs.Length == 0)
                {
                    continue;
                }

                object obj = Activator.CreateInstance(type);
                ComponentBase objectEvent = obj as ComponentBase;
                if (objectEvent == null)
                {
                    Console.WriteLine($"组件事件没有继承IObjectEvent: {type.Name}");
                    continue;
                }
                ety.AddComponent(objectEvent.GetType(), objectEvent);
            }
        }

        private static void Server_NewSessionConnected(LunarSession session)
        {
            m_DictSession[1] = (session);
            Console.WriteLine("接收到连接\r\n当前用户数：{0}", m_DictSession.Count);
                        
        }

        private static void Server_SessionClosed(LunarSession session, CloseReason value)
        {
            throw new NotImplementedException();
        }
    }
}
