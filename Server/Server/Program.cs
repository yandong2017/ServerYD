using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // 构造一个EntityBase对象
            EntityBase ety = new EntityBase();
            // 添加TestComponent组件，可以移到EntityBase的继承类的构造函数中
            TestComponent testComp = new TestComponent();
            ety.AddComponent("TestComponent", testComp);

            // 发送消息Event_01
            Event evt1 = new Event();
            evt1.EventID = (int)EventDefines.Event_01;
            evt1.PushUserData<string>("SomeData For Event01");
            ety.DispatchEvent(evt1);

            // 发送消息Event_02
            Event evt2 = new Event();
            evt2.EventID = (int)EventDefines.Event_02;
            evt2.PushUserData<string>("SomeData For Event02");
            ety.DispatchEvent(evt2);

            // 发送消息Event_03
            Event evt3 = new Event();
            evt3.EventID = (int)EventDefines.Event_03;
            evt3.PushUserData<float>(0.123f);
            ety.DispatchEvent(evt3);

            Console.Read();
        }
    }
}
