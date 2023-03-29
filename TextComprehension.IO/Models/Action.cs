namespace TextComprehension.IO.Models
{
    public class Action : ValueBase
    {
        public static readonly Action Default = new Action(string.Empty);

        public Action(string value) : base(value)
        {
        }
    }
}