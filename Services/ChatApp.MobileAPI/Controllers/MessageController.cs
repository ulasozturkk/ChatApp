using ChatApp.MobileAPI.Business.Commands;
using ChatApp.MobileAPI.Business.Queries;
using ChatApp.Utils.BaseController;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.MobileAPI.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class MessageController : CustomBaseController {

    private IMediator _mediator;
    public MessageController(IMediator mediator) {
      _mediator = mediator;
    }

    [HttpPost("AddMessage")]
    public async Task<IActionResult> AddMessage(AddMessageCommand request) {
      var res = await _mediator.Send(request);
      return Ok(res);
    }

    [HttpPost("CreateChatRoom")]
    public async Task<IActionResult> CreateChatRoom(CreateChatRoomCommand request) {
      var res = await _mediator.Send(request);
      return Ok(res);
    }

    [HttpPost("GetMessageByChatRoomID")]
    public async Task<IActionResult> GetMessageByChatRoomID(GetMessagesByChatRoomID request) {
      var res = await _mediator.Send(request);
      return Ok(res);
    }
  }
}
