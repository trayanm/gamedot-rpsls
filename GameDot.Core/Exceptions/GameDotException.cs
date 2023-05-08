namespace GameDot.Core.Exceptions
{
    public class GameDotException : Exception
    {
        public string Code { get; private set; }

        public GameDotException(string code, string message) : base(message)
        {
            this.Code = code;
        }

        public GameDotException(string code, string message, Exception inner) : base(message, inner)
        {
            this.Code = code;
        }
    }
}
