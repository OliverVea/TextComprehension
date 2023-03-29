namespace TextComprehension.IO.Models
{
    public class Argument : ValueBase
    {
        public static readonly Argument Default = new Argument(string.Empty);

        public Argument(string value) : base(value)
        {
        }
    }
}

