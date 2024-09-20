using ChatApp.MobileAPI.Business.Commands;
using ChatApp.MobileAPI.Business.Queries;
using ChatApp.Utils.BaseController;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.MobileAPI.Controllers {
  [Route("api/[controller]/[Action]")]
  [ApiController]
  public class MessageController : CustomBaseController {

    private IMediator _mediator;
    public MessageController(IMediator mediator) {
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddMessage(AddMessageCommand request) {
      var res = await _mediator.Send(request);
      return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChatRoom(CreateChatRoomCommand request) {
      var res = await _mediator.Send(request);
      return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> GetMessageByChatRoomID(GetMessagesByChatRoomID request) {
      var res = await _mediator.Send(request);
      return Ok(res);
    }
  }
}
