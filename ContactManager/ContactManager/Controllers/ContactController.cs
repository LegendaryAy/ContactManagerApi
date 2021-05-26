using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ContactManager.DTO;
using ContactManager.Models;
using ContactManager.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //Default endpoint
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //Authorization with JWT
    
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepo;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        //Contact Controller Constructor
        public ContactController(IContactRepository contactRepository, IMapper mapper, IConfiguration config)
        {
            _contactRepo = contactRepository;
            _mapper = mapper;
            Account account = new Account
            {
                Cloud = config.GetSection("CloudinarySettings:CloudName").Value,
                ApiKey = config.GetSection("CloudinarySettings:ApiKey").Value,
                ApiSecret = config.GetSection("CloudinarySettings:ApiSecret").Value,
            };
            _cloudinary = new Cloudinary(account);
        }
      
        /// <summary>
        /// Gets Paginated List of Contacts
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetContacts([FromQuery] PageModel model)
        {
            try
            {
                var contacts = _contactRepo.GetContacts().ToList();
                if (contacts.Count <= 0)
                {
                    return NotFound("No Contacts Available in your phone book");
                }
                
                //Pagination Details
                var count = contacts.Count();
                var currentPage = model.PageNumber;
                var pageSize = model.PageSize;
                var totalCount = count;
                var totalPages = (int)Math.Ceiling(count / (double)pageSize);
                var items = contacts.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                var previousPage = currentPage > 1 ? "Yes" : "No";
                var nextPage = currentPage < totalPages ? "Yes" : "No";
                var pagination = new
                {
                    TotalCount = totalCount,
                    PageSize = pageSize,
                    CurrentPage = currentPage,
                    TotalPages = totalPages,
                    previousPage,
                    nextPage,
                };
                HttpContext.Response.Headers.Add("Pagination-Header", JsonConvert.SerializeObject(pagination));
                if (items.Count == 0) return NotFound("No items to display");
                return Ok(items);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Fatal Error");
            }
        }

        /// <summary>
        /// Gets single Contact by ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetContact(int id)
        {
            try
            {
                var contact = _contactRepo.GetContactById(id);
                var model = _mapper.Map<ContactDto>(contact);
                if (contact == null) return NotFound("Contact Not Found");
                return Ok(model);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Fatal Error");
            }
        }

        /// <summary>
        /// Gets single Contact by Email
        /// </summary>
        [HttpGet("email/{email}")]        
        [Authorize(Roles = "Admin")]
        public IActionResult GetContactbyEmail(string email)
        {
            try
            {
                var contact = _contactRepo.GetContactByEmail(email);
                var model = _mapper.Map<ContactDto>(contact);
                if (contact == null) return NotFound("Contact Not Found");
                return Ok(model);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Fatal Error");
            }
            
        }

        /// <summary>
        /// Gets Paginated List according to Search Query
        /// </summary>
        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public IActionResult SearchContacts([FromQuery] SearchModel model)
        {
            try
            {
                var contacts = _contactRepo.GetContacts().ToList();
                if (contacts.Count <= 0)
                {
                    return NotFound("No Contacts Available in your phone book");
                }
                //Check for valid search query
                if (!string.IsNullOrEmpty(model.SearchQuery))
                {
                    contacts = contacts.Where(x => x.FirstName.ToLower().Contains(model.SearchQuery.ToLower())
                    || x.LastName.ToLower().Contains(model.SearchQuery.ToLower()) 
                    || x.PhoneNumber.Contains(model.SearchQuery)).ToList();
                }
                
                //Pagination Details
                var count = contacts.Count();
                var currentPage = model.PageNumber;
                var pageSize = model.PageSize;
                var totalCount = count;
                var totalPages = (int)Math.Ceiling(count / (double)pageSize);
                var items = contacts.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                var previousPage = currentPage > 1 ? "Yes" : "No";
                var nextPage = currentPage < totalPages ? "Yes" : "No";
                var pagination = new
                {
                    TotalCount = totalCount,
                    PageSize = pageSize,
                    CurrentPage = currentPage,
                    TotalPages = totalPages,
                    previousPage,
                    nextPage,
                    QuerySearch = string.IsNullOrEmpty(model.SearchQuery) ? "No Paramater Passed" : model.SearchQuery
                };
                HttpContext.Response.Headers.Add("SearchPagination-Header", JsonConvert.SerializeObject(pagination));
                if (items.Count == 0) return NotFound("No items to display");
                return Ok(items);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Fatal Error");
            }
        }

        /// <summary>
        /// Create New Contact
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody]ContactDto contact)
        {
            try
            {
                var count = _contactRepo.GetContactCount();
                var con = new Contact
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber
                };
                _contactRepo.AddContact(con);
                
                //Check to see if Contact was Added
                if (_contactRepo.GetContactCount() > count)
                {
                    return Created($"api/Contact/{con.Id}", _mapper.Map<ContactDto>(con));
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Fatal Error");
            }
            return BadRequest();
        }

        /// <summary>
        /// Update Contact Details
        /// </summary>
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, ContactDto contact)
        {
            try
            {
                var oldContact = _contactRepo.GetContactById(id);
                if (oldContact == null)
                {
                    return NotFound($"Could not find contact with id of {id}");
                }
                oldContact.FirstName = contact.FirstName;
                oldContact.LastName = contact.LastName;
                oldContact.Email = contact.Email;
                oldContact.PhoneNumber = contact.PhoneNumber;
                _contactRepo.UpdateContact(oldContact);

                return Ok(_mapper.Map<ContactDto>(oldContact));

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Fatal Error");
            }

        }

        /// <summary>
        /// Delete Contact by Id
        /// </summary>
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var count = _contactRepo.GetContactCount();
                var contact = _contactRepo.GetContactById(id);
                if (contact == null) return NotFound("Contact Not Found");
                _contactRepo.RemoveContact(contact);
                if(_contactRepo.GetContactCount() < count)
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Fatal Error");
            }
            return BadRequest();
        }

        /// <summary>
        /// Add Photo to Contact
        /// </summary>
        [HttpPatch("photo/{id}")]
        [AllowAnonymous]
        public IActionResult AddPhoto(int id, [FromForm]PhotoToAddDto model)
        {
            var user = _contactRepo.GetContactById(id);
            if (user == null)
            {
                return NotFound($"Could not find contact with id of {id}");
            }

            
            var file = model.PhotoFile;
            //Check to see photo exists
            if (file.Length <= 0) return BadRequest("Invalid File Size");

            var imageUploadResult = new ImageUploadResult();
            using(var fs = file.OpenReadStream())
            {
                var imageUploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, fs),
                    Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("face")
                };

                imageUploadResult = _cloudinary.Upload(imageUploadParams);
            }
            
            //Cloudinary Details of Photo
            var publicId = imageUploadResult.PublicId;
            var Url = imageUploadResult.Url;

            user.PhotoUrl = Url.ToString();
            _contactRepo.UpdateContact(user);
            return Ok(new { id = publicId, Url });
        }
    }
}