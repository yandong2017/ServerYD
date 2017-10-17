using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class TestComponent : ComponentBase
    {
        public override void OnAttachToEntity(EntityBase ety)
        {
            base.OnAttachToEntity(ety);

            // 注册事件响应函数
            RegisterEvent((int)EventDefines.Event_01, DealEvent01);
            RegisterEvent((int)EventDefines.Event_03, DealEvent03);
        }

        public override void OnDetachFromEntity(EntityBase ety)
        {
            base.OnDetachFromEntity(ety);
        }

        private void DealEvent01(Event evt)
        {
            string evtData = evt.GetUserData<string>(0);
            Console.WriteLine("TestComponent Handle Event: ID = " + evt.EventID.ToString());
            Console.WriteLine("TestComponent Handle Event: Data = " + evtData);
        }

        private void DealEvent03(Event evt)
        {
            float evtData = evt.GetUserData<float>(0);
            Console.WriteLine("TestComponent Handle Event: ID = " + evt.EventID.ToString());
            Console.WriteLine("TestComponent Handle Event: Data = " + evtData);
        }
    }
}
