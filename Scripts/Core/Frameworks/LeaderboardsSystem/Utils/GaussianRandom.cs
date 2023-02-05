using System;

namespace GeekBox.LeaderboardsSystem
{
    /// <summary>
    /// Metoda z tego co widze korzysta z metody eliminacji podczas generacji liczb pseudolosowych.
    /// Rozwazyc wykorzystanie w trakcie runtime dla duzej ilosci danych.
    /// 
    /// Source: https://stackoverflow.com/questions/218060/random-gaussian-variables
    /// </summary>
    public sealed class GaussianRandom
    {
        private bool _hasDeviate;
        private float _storedDeviate;
        private readonly Random _random;

        public GaussianRandom(Random random = null)
        {
            _random = random ?? new Random();
        }

        /// <summary>
        /// Obtains normally (Gaussian) distributed random numbers, using the Box-Muller
        /// transformation.  This transformation takes two uniformly distributed deviates
        /// within the unit circle, and transforms them into two independently
        /// distributed normal deviates.
        /// </summary>
        /// <param name="mu">The mean of the distribution.  Default is zero.</param>
        /// <param name="sigma">The standard deviation of the distribution.  Default is one.</param>
        /// <returns></returns>
        public float NextGaussian(float mu = 0, float sigma = 1)
        {
            if (sigma <= 0)
                throw new ArgumentOutOfRangeException("sigma", "Must be greater than zero.");

            if (_hasDeviate)
            {
                _hasDeviate = false;
                return _storedDeviate * sigma + mu;
            }

            float v1, v2, rSquared;
            do
            {
                // two random values between -1.0 and 1.0
                v1 = (float)_random.NextDouble();
                v2 = (float)_random.NextDouble();
                rSquared = v1 * v1 + v2 * v2;
                // ensure within the unit circle
            } while (rSquared >= 1 || rSquared == 0);

            // calculate polar tranformation for each deviate
            float polar = (float)Math.Sqrt(-2 * Math.Log(rSquared) / rSquared);
            // store first deviate
            _storedDeviate = v2 * polar;
            _hasDeviate = true;
            // return second deviate
            return v1 * polar * sigma + mu;
        }

        public float NextGaussian(float min, float max, float mu = 0, float sigma = 1)
        {
            float r;
            do
            {
                r = NextGaussian(mu, sigma);
            } while (r < 0 || r > 1);

            //float gausianRandom = (NextGaussian(mu, sigma) / 4);
            return  (max-min)*r + min;
        }
    }
}
