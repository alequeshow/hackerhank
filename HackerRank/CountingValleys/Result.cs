namespace CountingValleys
{
    public class Result
    {
        public Result(int steps, string path)
        {
            this.Steps = steps;
            this.Path = path;
            this.CurrentSeaDistance = 0;
        }

        int Steps { get; set; }

        string Path { get; set; }

        int CurrentSeaDistance { get; set; }

        char[] PathArray => Path?.ToCharArray();

        public int GetValue()
        {
            int result = 0;

            var array = Path.ToCharArray();

            for (int i = 0; i <= array.Length; i++)
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
            if (step == 0)
            {
                return false;
            }

            return PathArray[step - 1] == 'U';
        }

        bool LastStep(int step)
        {
            return (step == PathArray.Length);
        }
    }
}