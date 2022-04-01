using Flurl.Http;
using Main.Api.Models;
using Main.Api.ViewModels;

namespace Main.Api.Services
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
                var basePerson = new BasePerson();

                await _session.SaveAsync(basePerson);

                personViewModel.Id = basePerson.Id;

                await CreatePersonInSecondaryApiAsync(personViewModel);

                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task CreateWithMainErrorAsync(PersonViewModel personViewModel)
        {
            var transaction = _session.BeginTransaction();

            try
            {
                var basePerson = new BasePerson();

                await _session.SaveAsync(basePerson);

                personViewModel.Id = basePerson.Id;

                await CreatePersonInSecondaryApiAsync(personViewModel);

                throw new Exception("Main Error");

                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        private async Task CreatePersonInSecondaryApiAsync(PersonViewModel personViewModel)
        {
            await "https://localhost:7203/api/persons".PostJsonAsync(personViewModel);
        }
    }
}