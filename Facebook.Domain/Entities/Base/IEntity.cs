using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Entities.Base
{
    public interface IEntity
    {
        public Guid Id { get; set; }

        public Guid GetId();

        public void SetId(Guid id);
    }
}
