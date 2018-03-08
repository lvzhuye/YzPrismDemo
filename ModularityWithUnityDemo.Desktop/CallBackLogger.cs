using Microsoft.Practices.Prism.Logging;
using System;
using System.Collections.Generic;

namespace ModularityWithUnityDemo.Desktop
{
    public class CallBackLogger:ILoggerFacade
    {
        private Queue<Tuple<string, Category, Priority>> saveLogs = new Queue<Tuple<string, Category, Priority>>();

        private Action<string, Category, Priority> callBack;


        public Action<string, Category, Priority> CallBack
        {
            get
            {
                return this.callBack;
            }
            set
            {
                this.callBack = value;
            }
        }

        public void Log(string message, Category category, Priority priority)
        {
            if (this.CallBack != null)
            {
                this.CallBack(message, category, priority);
            }
            else
            {
                saveLogs.Enqueue(new Tuple<string, Category, Priority>(message, category, priority));
            }
        }

        public void ReplaySavedLogs()
        {
            if(this.CallBack != null)
            {
                while(this.saveLogs.Count > 0)
                {
                    var log = this.saveLogs.Dequeue();
                    this.CallBack(log.Item1,log.Item2,log.Item3);
                }
            }
        }
    }
}