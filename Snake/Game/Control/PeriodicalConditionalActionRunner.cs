using System;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
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
