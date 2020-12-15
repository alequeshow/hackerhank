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

            for (var i = 0; i < this.Queue.Length; i++)
            {
                var diff = orderedQueue[i] - this.Queue[i];

                if (diff < -2)
                {
                    Console.WriteLine("Too chaotic");
                    return;
                }
                else if (diff < 0)
                {
                    bribes += (diff * -1);
                }
                else if (i < this.Queue.Length - 1 && this.Queue[i] > this.Queue[i + 1])
                {
                    bribes++;
                }
            }

            Console.WriteLine(bribes);
        }
    }
}