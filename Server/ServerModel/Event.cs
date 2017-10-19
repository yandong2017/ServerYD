using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerModel
{
    public class Event
    {
        public int EventID = -1;
        public ArrayList EventArgs = new ArrayList();

        public void PushUserData<T>(T data)
        {
            EventArgs.Add(data);
        }

        public T GetUserData<T>(int index)
        {
            if (index >= EventArgs.Count)
            {
                Console.Write("GetUserData Error!");
                return default(T);
            }
            return (T)EventArgs[index];
        }
    }
}
