using System;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Allows to run action in infinite loop in new thread.
    /// Period can be specified. Action will be run only if condition is met.
    /// </summary>
    public static class PeriodicalConditionalActionRunner
    {
        public static void RunInNewThread(Action action, int period, Func<bool> condition)
        {
            Task.Run(() => Run(action, period, condition));
        }
        private static void Run(Action action, int period, Func<bool> condition)
        {
            while (true)
            {
                if (condition())
                {
                    action();
                }
                Thread.Sleep(period);
            }
        }
    }
}
