using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController] //It reinforces attribute routing, rather than using conventinal routing   | It automatically validates our request.
    //Note: ControllerBase | This gives access to things like, HttpResponses and Actions | MVC Controller Without View Support
    //Note: Controller | MVC Controller with View support
    public class ValuesController : ControllerBase
    {
       //Note: If the field is private field as a naming convention
       // an _fieldName is used
        private readonly DataContext _context;

        public ValuesController(DataContext context){
            _context = context;
        }

        //Note: Guidance is to use Async code as much as possible
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }
        [AllowAnonymous]
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
