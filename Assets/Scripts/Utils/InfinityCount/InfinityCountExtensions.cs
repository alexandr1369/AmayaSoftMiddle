using Utils;

namespace Editor.Tests
{
    public static class InfinityCountExtensions
    {
        private const int POWER_OF_TEN_STEP = 3;
        private const int MAX_POWER_OF_TEN_AMOUNT_CLAMP = 18;
        private const double MIN_VALUE = 1D;
        private const double THOUSAND = 1000D;

        public static InfinityCount Clamp(this InfinityCount self)
        {
            while (true)
            {
                if (self.Rate < MIN_VALUE)
                {
                    if (self.PowerOfTen >= POWER_OF_TEN_STEP)
                    {
                        self.PowerOfTen -= POWER_OF_TEN_STEP;
                        self.Rate *= THOUSAND;
                    }
                    else if(self.Rate <= GameUtils.TOLERANCE)
                        self.Rate = 0;
                }
                else if (self.Rate >= THOUSAND)
                {
                    if (self.PowerOfTen >= MAX_POWER_OF_TEN_AMOUNT_CLAMP)
                    {
                        self.Rate = THOUSAND - 1;
                    }
                    else
                    {
                        self.PowerOfTen += POWER_OF_TEN_STEP;
                        self.Rate /= THOUSAND;
                    }
                }

                if (self.Rate is > 0 and < MIN_VALUE && self.PowerOfTen >= POWER_OF_TEN_STEP
                    || self.Rate >= THOUSAND)
                {
                    continue;
                } 

                return self;
            }
        }
    }
}