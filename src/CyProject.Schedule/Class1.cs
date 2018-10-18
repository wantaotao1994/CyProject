using System;
using System.Collections.Generic;
using System.Linq;

namespace CyProject.Schedule
{
    public  class TaskQueue<T,TValue>:Dictionary<T, TValue>
    {

        public int GetIndexByKey(T key)  
        {
            if (!ContainsKey(key))
            {
                return -1;
            }
            int result = 0;
            foreach (var item in this)
            {
                if (item.Key.Equals(key))
                {
                    return result;
                }
                else
                {
                    result++;
                }
            }
            return -1;
        }


        public KeyValuePair<T,TValue> Prop() {
            var  result = this.FirstOrDefault();
            return result;
        }

        public void Push(KeyValuePair<T, TValue> item)
        {
            this.Add(item.Key,item.Value);
        }
    }
}
