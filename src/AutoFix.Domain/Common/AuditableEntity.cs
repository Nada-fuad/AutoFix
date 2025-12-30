using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Domain.Common
{
    public abstract class AuditableEntity:Entity
    {
        protected AuditableEntity() { }
        protected AuditableEntity (Guid id) : base(id) { }
        public string? CreatedBy {  get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTimeOffset CreatedAtUtc { get; set; }
        public DateTimeOffset LastModifiedAtUtc { get; private set; }
    }
}
