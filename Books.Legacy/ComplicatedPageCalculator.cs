using System.Diagnostics;

namespace Books.Legacy
{
    /// <summary>
    /// Long-running leacy code via a CPU thread.
    /// </summary>
    public class ComplicatedPageCalculator
    {
        public int CalculateBookPages(Guid id)
        {
            var watch = new Stopwatch { };
            watch.Start();

            while (true)
            {
                if (watch.ElapsedMilliseconds > 5000)
                {
                    break;
                }
            }

            return 40;
        }
    }
}