﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Services;
using LibraryApp.Data;
using LibraryApp.Models;
using LibraryApp.Dto.Person;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PersonTbController : ControllerBase
    {

        private readonly ILogger<PersonTbController> _logger;
        private readonly LibraryAppContext _context;
        private readonly PersonServices personService;

        public PersonTbController(ILogger<PersonTbController> logger, LibraryAppContext context)
        {
            _logger = logger;
            _context = context;
            personService = new PersonServices(_context);
        }
       

        [HttpGet("GetAllPeople")]
        public async Task<ActionResult> GetAllPeople()
        {
            _logger.LogInformation("GetAllPeople");
            try
            {
                var person = personService.GetAllPeople();
                if (person == null)
                {
                    _logger.LogWarning("No Content");
                    return NoContent();
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet("GetPersonFromName/{name}")]
        public async Task<ActionResult> GetPersonFromName([FromRoute] string name)
        {
            _logger.LogInformation("GetPersonFromName");
            try
            {
                var person = personService.GetPersonFromName(name);
                if (person == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet("GetPersonFromCPF/{cpf}")]
        public async Task<ActionResult> GetPersonFromCpf([FromRoute] long cpf)
        {
            _logger.LogInformation("GetPersonFromCpf");

            try
            {
                var person = personService.GetPersonFromCpf(cpf);
                if (person == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                return Ok(person);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreatePerson([FromBody] PersonTbDto person)
        {
            _logger.LogInformation("CreatePerson");

            try
            {
                var personReturn = await personService.CreatePerson(person);
                if (personReturn == null)
                {
                    _logger.LogWarning("No Content");
                    return NoContent();
                }
                return CreatedAtAction("PostPerson", new { name = personReturn.Name }, personReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePerson([FromRoute] int id, [FromBody] PersonTb person)
        {
            _logger.LogInformation("UpdatePerson");

            if (id != person.Cpf)
            {
                _logger.LogWarning("No Content");
                return BadRequest();
            }
            try
            {
                var personReturn = await personService.UpdatePerson(person);
                if (personReturn == null)
                {
                    return NotFound();
                }
                return CreatedAtAction("UpdatePerson", new { CPF = person.Cpf, name = personReturn.Name }, personReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson([FromRoute] int id)
        {
            _logger.LogInformation("DeletePerson");

            try
            {
                var personReturn = await personService.DeletePerson(id);
                if (personReturn == null)
                {
                    _logger.LogWarning("No Content");
                    return NotFound();
                }
                return CreatedAtAction("DeletePerson", new { CPF = personReturn.Cpf, name = personReturn.Name }, personReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}
