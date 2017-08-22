using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return new DTO.User()
            {
                Id = user.Id,
                Type = user.Type,
                Username = user.Username
            };
        }

        public User CreateUser(DTO.User user)
        {
            return new User()
            {
                Id = user.Id,
                Type = user.Type,
                Username = user.Username
            };
        }
    }
}
