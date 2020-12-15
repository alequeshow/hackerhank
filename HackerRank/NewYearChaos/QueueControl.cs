namespace NewYearChaos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class QueueControl
    {
        private int[] Queue;
        private int[] OrderedQueue;
        private List<int> Bribes;

        public QueueControl(int[] queue)
        {
            this.Queue = queue;
            this.OrderedQueue = this.Queue.OrderBy(q => q).ToArray();
            this.Bribes = new List<int>();
        }

        public void CheckBribes()
        {
            while (!AssertQueues())
            {
                for (var i = this.OrderedQueue.Length - 1; i >= 0; i--)
                {
                    var orderedValue = this.OrderedQueue[i];
                    var currentValue = this.Queue[i];

                    if (orderedValue - currentValue > 0)
                    {
                        SwapNext(i);
                    }

                    if (CheckChaos())
                    {
                        Console.WriteLine("Too chaotic");
                        return;
                    }

                    // Debug console
                    // Console.WriteLine(string.Join(' ', this.Queue));
                }
            }

            Console.WriteLine(this.Bribes.Count());
            return;
        }

        private bool AssertQueues()
        {
            return Enumerable.SequenceEqual(OrderedQueue, Queue);
        }

        private bool isFirst(int i)
        {
            return i == 0;
        }

        private void SwapNext(int i)
        {
            if (isFirst(i))
            {
                return;
            }

            if (IsOrdered(i))
            {
                return;
            }

            var aux = this.Queue[i - 1];
            this.Queue[i - 1] = this.Queue[i];
            this.Queue[i] = aux;

            // Store the one who bribed
            this.Bribes.Add(aux);
        }

        private bool CheckChaos()
        {
            var mostBriber = this.Bribes.GroupBy(i => i)
                .OrderByDescending(g => g.Count())
                .Take(1)
                .Select(g => g.Count()).FirstOrDefault();

            return mostBriber >= 3;
        }

        private bool IsOrdered(int i)
        {
            return this.Queue[i] > this.Queue[i - 1];
        }
    }
}