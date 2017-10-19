using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerModel
{
    public class ComponentBase
    {
        public delegate void EventHandleFunction(Event evt);
                
        private EntityBase Owner = null;
        // 事件映射表
        private Dictionary<int, EventHandleFunction> EventHandlerMap = new Dictionary<int, EventHandleFunction>();

        public virtual bool OnEvent(Event evt)
        {
            EventHandleFunction function = null;
            if (EventHandlerMap.TryGetValue(evt.EventID, out function) == false)
            {
                return false;
            }

            if (function == null)
                return false;

            function(evt);
            return true;
        }

        public virtual void OnAttachToEntity(EntityBase ety)
        {
            Owner = ety;
        }

        public virtual void OnDetachFromEntity(EntityBase ety)
        {
            Owner = null;
        }

        // 注册事件处理函数，同一事件注册两次，则后面的覆盖前面的
        protected void RegisterEvent(int eventID, EventHandleFunction handlerFunc)
        {
            if (EventHandlerMap.ContainsKey(eventID))
                EventHandlerMap.Remove(eventID);
            EventHandlerMap.Add(eventID, handlerFunc);
        }

        protected void UnregiterEvent(int eventID)
        {
            if (EventHandlerMap.ContainsKey(eventID))
                EventHandlerMap.Remove(eventID);
        }
    }
}
