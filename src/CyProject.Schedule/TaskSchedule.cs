using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CyProject.Model;

namespace CyProject.Schedule
{
   public static class TaskSchedule
    {
        private static TaskQueue<string, Task2Model> _queue;


        public static TaskQueue<string, Task2Model> _excutingQueue;


        public static TaskQueue<string, Task2Model> _completeQueue;
        static TaskSchedule()
        {
            _queue = new TaskQueue<string, Task2Model>();
            
            _excutingQueue = new TaskQueue<string, Task2Model>();
            _completeQueue = new TaskQueue<string, Task2Model>();
        }


        
        
        public static KeyValuePair<string, Task2Model> Prop() {
            KeyValuePair<string, Task2Model> keyValuePair = new KeyValuePair<string, Task2Model>();
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

        public static void Push(KeyValuePair<string, Task2Model> item)
        {
            lock (_queue)
            {
                _queue.Push(item);
            }
        }
    }
}
