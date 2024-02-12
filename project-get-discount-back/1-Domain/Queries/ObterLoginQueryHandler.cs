using MediatR;
using project_get_discount_back.Interfaces;
using project_get_discount_back.Result;
using project_get_discount_back.ViewModel;
using project_get_discount_back.Helpers;

namespace project_get_discount_back.Queries
{
    public record ObterLoginQuery(string email, string password) : IRequest<Result<LoginViewModel>>;

    public class ObterLoginQueryHandler : IRequestHandler<ObterLoginQuery, Result<LoginViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public ObterLoginQueryHandler(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<Result<LoginViewModel>> Handle(ObterLoginQuery request, CancellationToken cancellationToken)
        {
            var validationResult = ValidateRequest(request);
            if (validationResult != null)
            {
                return validationResult;
            }

            var user = await _userRepository.GetByEmail(request.email, cancellationToken);

            if (user == null)
            {
                return new Fail<LoginViewModel>(ResultError.EmailNaoExiste);
            }

            if (user.Deleted)
            {
                return new Fail<LoginViewModel>(ResultError.EmailDeletado);
            }

            var encryptedPassword = new Cryptography();
            if (user.Password == encryptedPassword.Encrypt(request.password))
            {
                var token = _tokenService.GenerateToken(user);
                var loginViewModel = new LoginViewModel
                {
                    Auth = true,
                    AccessToken = token
                };
                return new Success<LoginViewModel>(loginViewModel);
            }

            else
            {
                return new Fail<LoginViewModel>(ResultError.EmailOuPasswordErrado);
            }

        }

        private static Fail<LoginViewModel>? ValidateRequest(ObterLoginQuery request)
        {
            if (string.IsNullOrEmpty(request.email))
            {
                return new Fail<LoginViewModel>(ResultError.EmailVazio);
            }

            if (string.IsNullOrEmpty(request.password))
            {
                return new Fail<LoginViewModel>(ResultError.PasswordVazio);
            }

            return null;
        }
    }
}
