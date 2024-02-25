using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_get_discount_back._2_Infrastructure.Schemas.Responses;
using project_get_discount_back.Queries;
using project_get_discount_back.Results;
using static project_get_discount_back.Entities.User;

namespace project_get_discount_back._3_Presentation.V1.Controllers
{
    /// <summary>
    /// Controller para criar usuário.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
     public class RegisterUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Construtor do RegisterUserController.
        /// </summary>
        /// <param name="mediator">Mediator para enviar a query de registro de usuário.</param>
        public RegisterUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cadastra usuário.
        /// </summary>
        /// <param name="name">O nome do usuário</param>
        /// <param name="email">O email do usuário.</param>
        /// <param name="role">O tipo de acesso do usuário</param>
        /// <param name="cancellationToken">O token de cancelamento de operação assíncrona.</param>
        /// <returns>O resultado da operação de login.</returns>
        [HttpPost("/register")]
        //[Authorize(Roles = "ADM")]
        public async Task<ActionResult> Post(string name, string email, string role, CancellationToken cancellationToken)
        {
            var query = new RegisterUserQuery(name, email, role);
            var result = await _mediator.Send(query, cancellationToken);
            if (result is Fail fail)
            {
                return BadRequest(new ErrosResponse(fail.Code, fail.Message));
            }
            return Ok(result);
        }
    }
}
