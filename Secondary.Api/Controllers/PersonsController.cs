using Microsoft.AspNetCore.Mvc;
using Secondary.Api.Services;
using Secondary.Api.ViewModels;

namespace Secondary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonsController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonViewModel personViewModel)
        {
            await _personService.CreateAsync(personViewModel);

            return Accepted();
        }
    }
}