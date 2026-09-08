using System;
using System.Collections.Generic;

namespace Core
{
    public class TaskConfig
    {
        public string ConfigName { get; set; } = "Main Setup";
        public void Load() => Console.WriteLine($"Configuration loaded: {ConfigName}");
    }

    public class TaskController
    {
        private TaskConfig _config;

        public TaskController()
        {
            _config = new TaskConfig(); 
        }

        public void StartController()
        {
            _config.Load();
            Console.WriteLine("Controller successfully started.");
        }
    }

    public class TaskGroup
    {
        public string GroupName { get; set; }
        public List<BaseTask> Tasks { get; set; } = new List<BaseTask>();

        public void AddTask(BaseTask task)
        {
            Tasks.Add(task);
        }
    }
}