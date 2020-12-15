namespace NewYearChaos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
                for (var i = 0; i < this.OrderedQueue.Length; i++)
                {
                    var orderedValue = this.OrderedQueue[i];
                    var currentValue = this.Queue[i];

                    if (orderedValue - currentValue < 0)
                    {
                        SwapNext(i);
                    }

                    if (CheckChaos())
                    {
                        Console.WriteLine("Too chaotic");
                        return;
                    }

                    Console.WriteLine(string.Join(' ', this.Queue));
                }
            }

            Console.WriteLine(this.Bribes.Count());
            return;

            //for (var i = this.Queue.Length - 1; i >= 0; i--)
            //{
            //    var originalValue = this.OrderedQueue[i];
            //    var currentValuePlace = this.Queue.ToList().IndexOf(originalValue);
            //    var diff = currentValuePlace - i;

            //    if (diff < -2)
            //    {
            //        Console.WriteLine("Too chaotic");
            //        return;
            //    }
            //    else if (diff < 0)
            //    {
            //        bribes += (diff * -1);
            //    }
            //}

            //Console.WriteLine(bribes);
        }

        private bool AssertQueues()
        {
            return Enumerable.SequenceEqual(OrderedQueue, Queue);
        }

        private bool isLast(int i)
        {
            return i == this.Queue.Length - 1;
        }

        private void SwapNext(int i)
        {
            if (isLast(i))
            {
                return;
            }
            var aux = this.Queue[i];
            this.Queue[i] = this.Queue[i + i];
            this.Queue[i + i] = aux;

            // Store the one who bribed
            this.Bribes.Add(aux);
        }

        private bool CheckChaos()
        {
            var mostBriber = this.Bribes.GroupBy(i => i)
                .OrderByDescending(g => g.Count())
                .Take(1)
                .Select(g => g.Key).FirstOrDefault();

            return mostBriber >= 3;
        }
    }
}