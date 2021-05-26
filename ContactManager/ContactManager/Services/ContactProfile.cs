using AutoMapper;
using ContactManager.DTO;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public class ContactProfile : Profile //Auto Mapper Class
    {
        public ContactProfile()
        {
            //Creates Mapping Link from Source to Destination
            this.CreateMap<Contact, ContactDto>();
        }
    }
}
