using MediatR;
using project_get_discount_back.Interfaces;
using project_get_discount_back.Results;
using project_get_discount_back.ViewModel;
using project_get_discount_back.Helpers;

namespace project_get_discount_back.Queries
{
    public record GetLoginQuery(string email, string password) : IRequest<Result<LoginViewModel>>;

    public class GetLoginQueryHandler : IRequestHandler<GetLoginQuery, Result<LoginViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public GetLoginQueryHandler(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<Result<LoginViewModel>> Handle(GetLoginQuery request, CancellationToken cancellationToken)
        {
            var validationResult = ValidateRequest(request);
            if (validationResult != null)
            {
                return validationResult;
            }

            var user = await _userRepository.GetByEmail(request.email, cancellationToken);

            if (user == null)
            {
                return new Fail<LoginViewModel>(ResultError.NonExistentEmail);
            }

            if (user.Deleted)
            {
                return new Fail<LoginViewModel>(ResultError.EmailDeleted);
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
                return new Fail<LoginViewModel>(ResultError.WrongEmailOrPassword);
            }

        }

        private static Fail<LoginViewModel>? ValidateRequest(GetLoginQuery request)
        {
            if (string.IsNullOrEmpty(request.email))
            {
                return new Fail<LoginViewModel>(ResultError.LoginEmailEmpty);
            }

            if (string.IsNullOrEmpty(request.password))
            {
                return new Fail<LoginViewModel>(ResultError.LoginPasswordEmpty);
            }

            return null;
        }
    }
}
