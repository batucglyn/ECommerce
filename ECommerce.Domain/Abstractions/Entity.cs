using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.CreateVersion7();
            CreateAt = DateTimeOffset.UtcNow;
            IsActive = true;
        }
        public Guid Id { get; set; }

        public bool IsActive { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public Guid CreateUserId { get; set; }

        public DateTimeOffset? UpdateAt { get; set; }
        public Guid? UpdateUserId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeleteAt { get; set; }
        public Guid? DeleteUserId { get; set; }
    }
}
