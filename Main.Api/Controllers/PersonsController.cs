using Main.Api.Services;
using Main.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Main.Api.Controllers
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



        [HttpPost("main-error")]
        public async Task<IActionResult> CreateWithMainError(PersonViewModel personViewModel)
        {
            await _personService.CreateWithMainErrorAsync(personViewModel);

            return Accepted();
        }
    }
}