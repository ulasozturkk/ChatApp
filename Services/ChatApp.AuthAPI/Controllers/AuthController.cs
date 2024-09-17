using ChatApp.AuthAPI.Business.Commands;
using ChatApp.Utils.BaseController;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.AuthAPI.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : CustomBaseController {

    private IMediator _mediator;

    public AuthController(IMediator mediator) {
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommand request) {
      var result = await _mediator.Send(request);
      return Ok(result);
    }
  }
}
