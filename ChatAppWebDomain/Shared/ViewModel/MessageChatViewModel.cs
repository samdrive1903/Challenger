using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppWebDomain.Shared.ViewModel
{
    public class MessageChatViewModel
    {
        [Column("Código")]
        public int? Id { get; set; }

        [Column("Remetente")]
        public string Source { get; set; }

        [Column("Destinatário")]
        public string Destination { get; set; }

        [Column("Mensagem")]
        public string Body { get; set; }

        [Column("Data Envio")]
        public DateTime SentOn { get; set; }

        [Column("Lida?")]
        public bool IsRead { get; set; }

    }
}
