using System;

namespace orchestration.service.app.Infrastructure
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteServiceState(string state)
        {
            Console.WriteLine($"Service is {state}!");
        }
    }
}