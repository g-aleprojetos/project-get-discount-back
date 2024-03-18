using MediatR;
using Microsoft.AspNetCore.Mvc;
using project_get_discount_back._1_Domain.Queries;
using project_get_discount_back._2_Infrastructure.Schemas.Responses;
using project_get_discount_back.Queries;
using project_get_discount_back.Results;

namespace project_get_discount_back._3_Presentation.V1.Controllers
{
    /// <summary>
    /// Controller para criar e atualizar a senha do usuário.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RegisterPasswordController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Construtor do RegisterPasswordController.
        /// </summary>
        /// <param name="mediator">Mediator para enviar a query de registro de senha do usuario.</param>
        public RegisterPasswordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cadastra senha do usuário.
        /// </summary>
        /// <param name="email">O email do usuário.</param>
        /// <param name="password">A senha do usuário</param>
        /// <param name="cancellationToken">O token de cancelamento de operação assíncrona.</param>
        /// <returns>O resultado da operação de cadastro da senha do usuário.</returns>
        [HttpPost("/registerPassword")]
        public async Task<ActionResult> Post(string email, string password, CancellationToken cancellationToken)
        {
            var query = new RegisterPasswordQuery(email, password);
            var result = await _mediator.Send(query, cancellationToken);
            if (result is Fail fail)
            {
                return BadRequest(new ErrosResponse(fail.Code, fail.Message));
            }
            return Ok();
        }
    }
}
