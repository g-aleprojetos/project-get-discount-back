using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_get_discount_back.Queries;

namespace project_get_discount_back.V1.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de login de usu�rios.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Construtor do LoginController.
        /// </summary>
        /// <param name="mediator">Mediator para enviar a query de login.</param>
        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obt�m o login do usu�rio.
        /// </summary>
        /// <param name="email">O email do usu�rio.</param>
        /// <param name="password">A senha do usu�rio.</param>
        /// <param name="cancellationToken">O token de cancelamento de opera��o ass�ncrona.</param>
        /// <returns>O resultado da opera��o de login.</returns>
        [HttpPost("/Login")]
        public async Task<ActionResult> Post(string email, string password,  CancellationToken cancellationToken)
        {
            var query = new GetLoginQuery(email, password);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
