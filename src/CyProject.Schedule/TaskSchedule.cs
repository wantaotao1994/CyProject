using System;
using System.Collections.Generic;
using System.Text;

namespace CyProject.Schedule
{
   public static class TaskSchedule
    {
        private static TaskQueue<string, object> _queue;

        static TaskSchedule()
        {
            _queue = new TaskQueue<string, object>();
        }


        public static KeyValuePair<string, object> Prop() {
            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>();
            lock (_queue)
            {
                keyValuePair= _queue.Prop();
            }

            return keyValuePair;
        }

        public static int GetIndex(string key)
        {
            int reuslt = 0;
            lock (_queue)
            {
                reuslt = _queue.GetIndexByKey(key);
            }
            return reuslt;
        }

        public static void Push(KeyValuePair<string, object> item)
        {
            lock (_queue)
            {
                _queue.Push(item);
            }
        }
    }
}
