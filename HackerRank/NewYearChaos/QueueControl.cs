namespace NewYearChaos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class QueueControl
    {
        private int[] Queue;

        public QueueControl(int[] queue)
        {
            this.Queue = queue;
        }

        public void CheckBribes()
        {
            var orderedQueue = this.Queue.OrderBy(q => q).ToArray();
            var bribes = 0;

            for (var i = this.Queue.Length - 1; i >= 0; i--)
            {
                var originalValue = orderedQueue[i];
                var currentValuePlace = this.Queue.ToList().IndexOf(originalValue);
                var diff = currentValuePlace - i;

                if (diff < -2)
                {
                    Console.WriteLine("Too chaotic");
                    return;
                }
                else if (diff < 0)
                {
                    bribes += (diff * -1);
                }
            }

            Console.WriteLine(bribes);
        }
    }
}