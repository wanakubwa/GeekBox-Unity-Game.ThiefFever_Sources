using System;

namespace GeekBox.LeaderboardsSystem
{
    public sealed class SystemRandom
    {
        private readonly Random _random;

        public SystemRandom(Random random = null)
        {
            _random = random ?? new Random();
        }

        public float NextFloat(float min, float max)
        {
            float result =  (float)_random.NextDouble();
            return (max - min) * result + min;
        }
    }
}
