using System;
using System.IO;

namespace PV178.Homeworks.HW02.Tests
{
    public static class TestHelperMethods
    {
        /// <summary>
        /// Captures console output for given action
        /// </summary>
        /// <param name="action">Action to perform</param>
        /// <returns>Console output (without \r\n)</returns>
        public static string CaptureConsoleOutput(Action action)
        {
            var originalOutput = Console.Out;
            string result;
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                action.Invoke();
                result = sw.ToString().Replace("\r\n", string.Empty);                        
            }
            Console.SetOut(originalOutput);
            return result;
        }
    }
}
