using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RentMe.Helpers;
using RentMe.Hubs;
using RentMe.Models;
using RentMe.Services;
using RentMe.ViewModels;

namespace RentMe.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHubContext<MessageHub> _hubContext;

        public MessagesController(IMessageService messageService, IUserService userService,
                                    IMapper mapper, IHubContext<MessageHub> hubContext)
        {
            _messageService = messageService;
            _userService = userService;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet("{messageId}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(string userId, Guid messageId)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            var messageFromRepo = await _messageService.GetMessage(messageId);
            if (messageFromRepo == null)
            {
                return NotFound();
            }

            return Ok(messageFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(string userId,
       [FromQuery] MessageParams messageParams)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            messageParams.UserId = userId;

            var messagesFromRepo = await _messageService.GetMessagesForUser(messageParams);

            var messages = _mapper.Map<IEnumerable<MessageToReturn>>(messagesFromRepo);

            Response.AddPagination(messagesFromRepo.CurrentPage, messagesFromRepo.PageSize,
                messagesFromRepo.TotalCount, messagesFromRepo.TotalPages);

            return Ok(messages);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(string userId, string recipientId)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            var messagesFromRepo = await _messageService.GetMessageThread(userId, recipientId);

            var messageThread = _mapper.Map<IEnumerable<MessageToReturn>>(messagesFromRepo);

            return Ok(messageThread);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(string userId, MessageForCreation messageForCreation)
        {
            var sender = await _userService.GetUser(userId);

            if (sender.Id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            messageForCreation.SenderId = userId;

            var recipient = await _userService.GetUser(messageForCreation.RecipientId);

            if (recipient == null)
            {
                return BadRequest("Could not find user");
            }

            var message = _mapper.Map<Message>(messageForCreation);

            if (await _messageService.AddMessage(message))
            {
                var messageToReturn = _mapper.Map<MessageToReturn>(message);


                if (MessageHub._userConnections.ContainsKey(messageForCreation.RecipientId))
                {
                    await _hubContext.Clients.Clients(MessageHub._userConnections[messageForCreation.RecipientId])
                                            .SendAsync("SignalMessageReceived", messageToReturn);
                }

                if (MessageHub._userConnections.ContainsKey(userId))
                {
                    await _hubContext.Clients.Clients(MessageHub._userConnections[userId])
                                               .SendAsync("SignalMessageReceived", messageToReturn);
                }

                return CreatedAtRoute("GetMessage", new { userId, messageId = message.Id }, messageToReturn);
            }

            throw new Exception("Creating the message failed on save");

        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteMessage(Guid messageId, string userId)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            var messageFromRepo = await _messageService.GetMessage(messageId);

            if (messageFromRepo.SenderId == userId)
            {
                messageFromRepo.SenderDeleted = true;
            }

            if (messageFromRepo.RecipientId == userId)
            {
                messageFromRepo.RecipientDeleted = true;
            }

            if (messageFromRepo.SenderDeleted && messageFromRepo.RecipientDeleted)
            {
                _messageService.Delete(messageFromRepo);
            }

            if (await _messageService.SaveAll())
            {
                return NoContent();
            }
            throw new Exception("Error deleting the message");
        }


        [HttpPost("{messageId}/read")]
        public async Task<IActionResult> MarkMessageAsRead(string userId, Guid messageId)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            var message = await _messageService.GetMessage(messageId);

            if (message.RecipientId != userId)
            {
                return Unauthorized();
            }

            await _messageService.MarkMessageAsRead(message);

            await _hubContext.Clients.Clients(MessageHub._userConnections[message.SenderId])
                .SendAsync("MessageRead", userId);

            //await _hubContext.Clients.Clients(MessageHub._userConnections[userId])
            //                            .SendAsync("MessageRead");

            return NoContent();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetMessagesList(string userId)
        {
            var messagesToReturn = await _messageService.GetMessagesList(userId);
            return Ok(messagesToReturn);
        }
    }
}
