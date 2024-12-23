using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Entities.Base
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid GetId()
        {
            return Id;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
