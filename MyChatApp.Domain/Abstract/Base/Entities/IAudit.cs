using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChatApp.Domain.Abstract.Base.Entities
{
    public interface IAudit : IBaseEntity
    {
        string? Id { get; set; }

        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
