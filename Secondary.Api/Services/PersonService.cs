using Secondary.Api.Models;
using Secondary.Api.ViewModels;

namespace Secondary.Api.Services
{
    public class PersonService
    {
        private readonly NHibernate.ISession _session;

        public PersonService(NHibernate.ISession session)
        {
            _session = session;
        }

        public async Task CreateAsync(PersonViewModel personViewModel)
        {
            var transaction = _session.BeginTransaction();

            try
            {
                var person = new Person
                {
                    Id = personViewModel.Id,
                    Name = personViewModel.Name,
                    BirthDate = personViewModel.BirthDate
                };

                await _session.SaveAsync(person);

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                throw;
            }
        }
    }
}