namespace CountingValleys
{
    using System;

    public class Result
    {
        public Result(int steps, string path)
        {
            if (steps != path.Length)
            {
                throw new ArgumentException($"Parameters {nameof(steps)} and {nameof(path)} must match");
            }

            if (steps < 2 || steps > 1000000)
            {
                throw new ArgumentException($"Parameter {nameof(steps)} must be between 2 and 1000000");
            }

            this.Path = path;
            this.CurrentSeaDistance = 0;
        }

        string Path { get; set; }

        int CurrentSeaDistance { get; set; }

        char[] PathArray => Path?.ToCharArray();

        public int GetValue()
        {
            var result = 0;

            for (var i = 0; i <= PathArray.Length; i++)
            {
                if (HikeChanged(i) && WasClimbing(i))
                {
                    result++;
                }

                if (!LastStep(i))
                {
                    UpdateCurrentSeaLevel(i);
                }
            }

            return result;
        }

        void UpdateCurrentSeaLevel(int step)
        {
            var unit = GetCurrentUnit(step);

            this.CurrentSeaDistance = this.CurrentSeaDistance + unit;
        }

        int GetCurrentUnit(int step)
        {
            var stepType = PathArray[step];

            switch (stepType)
            {
                case 'U':
                    return 1;

                case 'D':
                default:
                    return -1;
            }
        }

        bool HikeChanged(int step)
        {
            return (
                this.CurrentSeaDistance == 0
                && step != 0
                && step != (this.PathArray.Length - 1)
            );
        }

        bool WasClimbing(int step)
        {
            return (step != 0 && PathArray[step - 1] == 'U');
        }

        bool LastStep(int step)
        {
            return (step == PathArray.Length);
        }
    }
}