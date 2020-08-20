using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentMe.Data;
using RentMe.Helpers;
using RentMe.Models;
using RentMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.Services
{
    public interface IMessageService
    {
        public Task<bool> AddMessage(Message message);
        public Task<Message> GetMessage(Guid id);
        public Task MarkMessageAsRead(Message message);
        public Task<bool> SaveAll();
        public void Delete(Message messageFromRepo);
        public Task<IEnumerable<Message>> GetMessageThread(string userId, string recipientId);
        public Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        public Task<IEnumerable<MessageForList>> GetMessagesList(string userId);
    }

    public class MessageService : IMessageService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public MessageService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> AddMessage(Message message)
        {
            message.Id = new Guid();
            await _context.Messages.AddAsync(message);
            return await _context.SaveChangesAsync() > 0;
        }

        public void Delete(Message messageFromRepo)
        {
            _context.Messages.Remove(messageFromRepo);
        }

        public async Task<Message> GetMessage(Guid id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(string userId, string recipientId)
        {
            var messages = await _context.Messages
                                                  .Where(m => m.RecipientId == userId && m.RecipientDeleted == false
                                                       && m.SenderId == recipientId
                                                       || m.RecipientId == recipientId && m.SenderId == userId
                                                       && m.SenderDeleted == false)
                                                  .Include(m => m.Sender)
                                                  .Include(m => m.Recipient)
                                                  //.OrderByDescending(m => m.MessageSent)
                                                  .OrderBy(m => m.MessageSent)
                                                  .ToListAsync();
            return messages;
        }

        public async Task MarkMessageAsRead(Message message)
        {
            message.IsRead = true;
            message.DateRead = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Messages
                                        .AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(m => m.RecipientId == messageParams.UserId
                        && m.RecipientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(m => m.SenderId == messageParams.UserId
                        && m.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where(m => m.RecipientId == messageParams.UserId
                        && m.RecipientDeleted == false && m.IsRead == false);
                    break;
            }
            messages = messages.OrderByDescending(d => d.MessageSent);

            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageForList>> GetMessagesList(string userId)
        {
            var userIds = await _context.Messages
                                    .Where(m => m.SenderId == userId && m.SenderDeleted == false ||
                                                m.RecipientId == userId && m.RecipientDeleted == false)
                                    .Select(m => m.SenderId == userId ? m.RecipientId : m.SenderId)
                                    .Distinct()
                                    .ToListAsync();

            var messages = new List<MessageForList>();

            foreach (var id in userIds)
            {
                var message = await _context.Messages
                                        .Where(m => m.SenderId == id && m.RecipientId == userId ||
                                                m.SenderId == userId && m.RecipientId == id)
                                        .OrderByDescending(m => m.MessageSent)
                                        .Include(m => m.Sender)
                                        .Include(m => m.Recipient)
                                        .FirstOrDefaultAsync();

                var messageForList = _mapper.Map<MessageForList>(message);
                if (message.SenderId == id)
                {
                    messageForList.UserId = message.SenderId;
                    messageForList.UserName = message.Sender.UserName;
                }
                else
                {
                    messageForList.UserId = message.RecipientId;
                    messageForList.UserName = message.Recipient.UserName;
                }

                messages.Add(messageForList);
            }

            messages = messages.OrderByDescending(m => m.MessageSent).ToList();
            return messages;
        }
    }
}
