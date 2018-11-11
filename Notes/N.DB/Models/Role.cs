using N.DB.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.DB.Models
{
    public class Role : IEntity
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }
    }
}
