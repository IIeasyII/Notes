using N.DB.Models.Interfaces;
using System;

namespace N.DB.Models
{
    public class Note : IEntity
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Content { get; set; }

        public virtual bool Flag { get; set; }

        public virtual string TagList { get; set; }

        public virtual DateTime? Date { get; set; }

        public virtual string File { get; set; }

        public virtual User User { get; set; }
    }
}
