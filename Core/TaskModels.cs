using System;

namespace Core
{
    public abstract class BaseTask : IShow
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual void PrintStatus()
        {
            Console.WriteLine($"Task [{Id}]: {Title}");
        }

        public abstract int GetEstimatedHours();
        public abstract void ShowDetails();
    }

    public class SimpleTask : BaseTask
    {
        public int Hours { get; set; }
        public override int GetEstimatedHours() => Hours;

        public override void ShowDetails()
        {
            Console.WriteLine($"[Simple Task] {Title} - Estimated Time: {Hours} hours.");
        }
    }

    public class EpicTask : BaseTask
    {
        public int ComplexityLevel { get; set; }
        public override int GetEstimatedHours() => ComplexityLevel * 5;

        public override void PrintStatus()
        {
            base.PrintStatus();
            Console.WriteLine("WARNING: This is a large-scale (Epic) task!");
        }

        public override void ShowDetails()
        {
            Console.WriteLine($"[Epic Task] {Title} - Complexity Level: {ComplexityLevel} | Estimated Time: {GetEstimatedHours()} hours.");
        }
    }
}