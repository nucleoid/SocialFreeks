
using FluentNHibernate.Mapping;
using SocialFreeks.Entities;

namespace SocialFreeks.Repository.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Created).Insert().Not.Update();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.UserName).Unique();
        }
    }
}
