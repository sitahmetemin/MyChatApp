using MyChatApp.Domain.Concrete.Base.Entities;

namespace MyChatApp.Domain.Concrete.Entities
{
    public class ActionLog : Audit
    {
        public string ContextId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }
}
