using Application.Common.Models;
using Application.DTOs.PersonDtos;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{//TODO:add versioning and rate limit
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParameters parameters)
        {
            var result = await _personService.GetAllAsync(parameters);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _personService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonDto dto)
        {
            var result = await _personService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonDto dto)
        {
            await _personService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personService.DeleteAsync(id);
            return NoContent();
        }
    }
}


