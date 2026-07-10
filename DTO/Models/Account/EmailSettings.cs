namespace DTO.Models.Auth
{
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }

        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public string IMAPServer { get; set; }
        public int IMAPPort { get; set; }
    }

    public class EmailSendAndReceive
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public string Subject { get; set; }
        public string BodyHtml { get; set; }
        public DateTime Date { get; set; }
    }

    public class EmailConversationByDate
    {
        public DateTime Date { get; set; }
        public List<EmailSendAndReceive> Messages { get; set; }
    }

    public class NotificationDto
    {
        public string QueryId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }


}
