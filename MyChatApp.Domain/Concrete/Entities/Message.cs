using MyChatApp.Domain.Concrete.Base.Entities;

namespace MyChatApp.Domain.Concrete.Entities
{
    public class Message : Audit
    {
        public string Content { get; set; }
        public string Channel { get; set; }
        public DateTime SendDate { get; set; } = DateTime.Now;
        public string UserName { get; set; }
    }
}
