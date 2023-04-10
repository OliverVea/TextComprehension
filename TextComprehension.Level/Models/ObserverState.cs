namespace TextComprehension.Level.Models
{
    public class ObserverState
    {
        public int Ring { get; set; }
        public Heading Heading { get; set; }
        public int Spur { get; set; }
        
        /// <summary>
        /// Normalized time of day. 0 = 12 am, 0.5 = 12 pm, 1 = 12 am.
        /// </summary>
        public double TimeOfDay { get; set; } = 0.5;
    }
}