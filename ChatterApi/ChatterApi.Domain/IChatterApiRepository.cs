using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatterApi.Domain.Entities;

namespace ChatterApi.Domain
{
    public interface IChatterApiRepository
    {
        User GetUser(int id);

        Message GetMessage(int id);
        IQueryable<Message> GetMessages();

        IQueryable<Message> GetMessages(int userId);

        RepositoryActionResult<Message> InsertMessage(Message message);
        RepositoryActionResult<Message> UpdateMessage(Message chatMessage);
        RepositoryActionResult<Message> DeleteMessage(int id);
    }
}
