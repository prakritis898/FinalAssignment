using FinalAssignment.Data;
using FinalAssignment.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalAssignment.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactController : Controller
    {
        private readonly ContactAPIDbContext dbContext;

        public ContactController(ContactAPIDbContext dbContext)

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
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                return Ok(contact);                
            }
            return NotFound();

        }
    

        [HttpPost]
        public async Task<IActionResult> PostContacts(AddContacts addContacts)
        {
            var contacts = new Contact()
            {
                id = Guid.NewGuid(),
                Fullname = addContacts.Fullname,
                Address = addContacts.Address,
                Email = addContacts.Email,
                Phone = addContacts.Phone
            };
            await dbContext.Contacts.AddAsync(contacts);
            await dbContext.SaveChangesAsync();

            return Ok(contacts);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> PutContact([FromRoute] Guid id,UpdateContact updateContact)
        {
          var contact= await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.Fullname = updateContact.Fullname;
                contact.Phone = updateContact.Phone;
                contact.Email = updateContact.Email;
                contact.Address = updateContact.Address;

               await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
               dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                    return Ok(contact);
            }
            return NotFound();

        }
    }
}
