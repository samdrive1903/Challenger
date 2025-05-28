namespace ChatAppServerWebSocket.Console.Model
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Body { get; set; }
        public DateTime SentOn { get; set; }
        public bool IsRead { get; set; }
 
    }
}
