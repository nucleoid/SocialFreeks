
using System;

namespace SocialFreeks.Entities
{
    public class User
    {
        public User()
        {
            Created = DateTime.Now;
        }

        public virtual Guid Id { get; private set; }
        public virtual DateTime Created { get; private set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string UserName { get; set; }
    }
}
