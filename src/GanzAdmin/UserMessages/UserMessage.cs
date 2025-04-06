namespace GanzAdmin.UserMessages
{
    public class UserMessage
    {
        public enum Severities
        {
            Info,
            Warning,
            Success,
            Error
        }

        public string Type { get; set; }
        public Severities Severity { get; set; }
        public string Text { get; set; }
    }
}
