using CrudAPI.Data;
using CrudAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly CrudAPIDbContext dbContext;

        public ContactsController(CrudAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var crud = await dbContext.Contacts.FindAsync(id);

            if(crud == null)
            {
                return NotFound();
            }
            return Ok(crud);

        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var crud = new Crud()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone
            };

            await dbContext.Contacts.AddAsync(crud);
            await dbContext.SaveChangesAsync();

            return Ok(crud);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var crud = await dbContext.Contacts.FindAsync(id);

            if(crud != null)
            {
                crud.FullName = updateContactRequest.FullName;
                crud.Address = updateContactRequest.Address;
                crud.Phone = updateContactRequest.Phone;
                crud.Email = updateContactRequest.Email;

               await dbContext.SaveChangesAsync();

                return Ok(crud);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var crud = await dbContext.Contacts.FindAsync(id);

            if(crud != null)
            {
                dbContext.Remove(crud);
                await dbContext.SaveChangesAsync();
                return Ok(crud);
            }
            return NotFound();
        }
    }
}
