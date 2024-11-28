namespace MicroDotNet.Packages.Cqrs
{
    public class Message
    {
        private Message(
            MessageLevel level,
            string code,
            string text)
        {
            this.Level = level;
            this.Code = code;
            this.Text = text;
        }

        public MessageLevel Level { get; }

        public string Code { get; }

        public string Text { get; }

        public static Message CreateInformation(string code, string text) =>
            new Message(MessageLevel.Information, code, text);

        public static Message CreateWarning(string code, string text) =>
            new Message(MessageLevel.Warning, code, text);

        public static Message CreateError(string code, string text) =>
            new Message(MessageLevel.Error, code, text);
    }
}
