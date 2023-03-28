namespace TextComprehension.Models
{
    public class Target : ValueBase
    {
        public static readonly Target Default = new Target(string.Empty);

        public Target(string value) : base(value)
        {
        }
    }
}