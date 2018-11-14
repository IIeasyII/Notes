using N.DB.Models.Interfaces;

namespace N.DB.Models
{
    public class User : IEntity
    {
        public virtual long Id { get; set; }

        public virtual string Login { get; set; }

        public virtual string Password { get; set; }

        public virtual Role Role { get; set; }
    }
}
