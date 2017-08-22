using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatterApi.Domain.Entities;

namespace ChatterApi.Domain
{
    public class ChatterApiRepository : IChatterApiRepository
    {
        public static ChatterApiContext _ctx;

        public ChatterApiRepository(ChatterApiContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public Message GetMessage(int id)
        {
            var message = _ctx.Messages.Where(m => m.Id == id).FirstOrDefault();
            return (message == null) ? null : message;
        }

        public IQueryable<Message> GetMessages(int userId)
        {
            var correctMessages = _ctx.Messages.Where(eg => eg.UserId == userId).FirstOrDefault();
            if (correctMessages == null)
                return null;

            return _ctx.Messages.Where(e => e.UserId == userId);
        }

        public IQueryable<Message> GetMessages()
        {
            return _ctx.Messages;
        }

        public RepositoryActionResult<Message> InsertMessage(Message message)
        {
            try
            {
                _ctx.Messages.Add(message);
                var result = _ctx.SaveChanges();
                if(result > 0)
                {
                    return new RepositoryActionResult<Message>(message, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Message>(message, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Message>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Message> UpdateMessage(Message message)
        {
            try
            {
                var existingMessage = _ctx.Messages.Where(m => m.Id == message.Id).FirstOrDefault();
                if (existingMessage == null)
                    return new RepositoryActionResult<Message>(message, RepositoryActionStatus.NotFound);

                _ctx.Entry(existingMessage).State = EntityState.Detached;

                _ctx.Messages.Attach(message);

                _ctx.Entry(message).State = EntityState.Modified;

                if (_ctx.SaveChanges() > 0)
                    return new RepositoryActionResult<Message>(message, RepositoryActionStatus.Updated);

                return new RepositoryActionResult<Message>(null, RepositoryActionStatus.NothingModified, null);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Message>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<Message> DeleteMessage(int id)
        {
            try
            {
                var existingMessage = _ctx.Messages.Where(m => m.Id == id).FirstOrDefault();
                if (existingMessage == null)
                    return new RepositoryActionResult<Message>(null, RepositoryActionStatus.NotFound);

                _ctx.Messages.Remove(existingMessage);
                _ctx.SaveChanges();

                return new RepositoryActionResult<Message>(null, RepositoryActionStatus.Deleted);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Message>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}
