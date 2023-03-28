namespace TextComprehension.Models
{
    public class Choice
    {
        public Option Option { get; set; } = new Option();
        public Argument Argument { get; set; } = Argument.Default;
        public Target Target { get; set; } = Target.Default;
    }
}

