using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Secondary.Api.Models;

namespace Secondary.Api.Mappings
{
    public class PersonMapping : ClassMapping<Person>
    {
        public PersonMapping()
        {
            Id(x => x.Id, map =>
            {
                map.Column("PersonId");
                map.Generator(Generators.Assigned);
            });

            Property(x => x.Name);

            Property(x => x.BirthDate);
        }
    }
}