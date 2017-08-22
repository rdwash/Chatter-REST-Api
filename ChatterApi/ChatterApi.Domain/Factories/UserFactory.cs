using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChatterApi.Domain.Entities;

namespace ChatterApi.Domain.Factories
{
    public class UserFactory
    {
        public UserFactory()
        {

        }

        public DTO.User CreateUser(User user)
        {
            var userDTO = new DTO.User();
            userDTO.Id = user.Id;
            userDTO.Type = user.Type;
            userDTO.Attributes.Username = user.Username;
            userDTO.Links.Self = "";

            return userDTO;
        }

        public User CreateUser(DTO.User user)
        {
            return new User()
            {
                Id = user.Id,
                Type = user.Type,
                Username = user.Attributes.Username
            };
        }
    }
}