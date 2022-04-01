using Main.Api.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Main.Api.Mappings
{
    public class BasePersonMapping : ClassMapping<BasePerson>
    {
        public BasePersonMapping()
        {
            Id(x => x.Id, map =>
            {
                map.Column("BasePersonId");
                map.Generator(Generators.Guid);
            });

            Property(x => x.ToDelete);
        }
    }
}