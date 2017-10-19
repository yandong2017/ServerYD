using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerModel
{
    public class EntityBase
    {
        // 组件列表，未实现RTTI，故用string做索引
        // 如果有RTTI，可以直接存储List<ComponentBase>，并且大多是这么做的
        public Dictionary<Type, ComponentBase> ComponentArray = new Dictionary<Type, ComponentBase>();

        // 通过此函数将消息分发给各个Component，各个Component各自处理此消息
        // 通过DisptachEvetn，每个Compoent都会收到此消息，但是否处理此消息，由各组件自己决定
        public virtual bool DispatchEvent(Event evt)
        {
            bool bHandled = false;
            foreach (ComponentBase comp in ComponentArray.Values)
            {
                bool ret = comp.OnEvent(evt);
                bHandled = (bHandled || ret);
            }
            if (bHandled == false)
                Console.WriteLine("Receive an unhandled event: id = " + evt.EventID.ToString());

            return bHandled;
        }

        public bool AddComponent(Type compName, ComponentBase compObject)
        {
            if (compObject == null)
            {
                Console.Write("AddComponent Failed, Component.Name=" + compName);
                return false;
            }

            ComponentArray.Add(compName, compObject);
            compObject.OnAttachToEntity(this);
            OnComponentAttached(compObject);

            return true;
        }

        public bool RemoveComponent(Type compName)
        {
            if (!ComponentArray.ContainsKey(compName))
            {
                Console.Write("RemoveComponent Failed, Component.Name=" + compName);
                return false;
            }

            ComponentArray[compName].OnDetachFromEntity(this);
            OnComponentDetached(ComponentArray[compName]);
            ComponentArray.Remove(compName);
            return true;
        }

        public virtual void OnComponentAttached(ComponentBase compObject)
        {
        }

        public virtual void OnComponentDetached(ComponentBase compObject)
        {
        }

    }
}
