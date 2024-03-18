using MediatR;
using project_get_discount_back._1_Domain.Entities;
using project_get_discount_back._1_Domain.Interfaces;
using project_get_discount_back.Entities;
using project_get_discount_back.Interfaces;
using project_get_discount_back.Results;
using static project_get_discount_back._1_Domain.Helpers.Email;

namespace project_get_discount_back._1_Domain.Queries
{
    public record RegisterPasswordQuery(string email, string password) : IRequest<Result>;
    public class RegisterPasswordQueryHandler : IRequestHandler<RegisterPasswordQuery, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmail _email;

        public RegisterPasswordQueryHandler(IUserRepository userRepository, IUserService userService, IUnitOfWork unitOfWork, IEmail email)
        {
            _userRepository = userRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _email = email;
        }
        public async Task<Result> Handle(RegisterPasswordQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.email))
            {
                return new Fail(ResultError.RegisterEmailPasswordEmpty);
            }

            if (string.IsNullOrEmpty(request.password))
            {
                return new Fail(ResultError.RegisterPasswordEmpty);
            }
            var user = await _userRepository.GetByEmail(request.email, cancellationToken);

            if (user == null)
            {
                return new Fail(ResultError.UserNotFound);
            }

            var password = new Password(request.password, user.Id);

            user.SetPassword(password);

            _userRepository.Update(user);

            await _unitOfWork.Commit(cancellationToken);

            if (await SendEmailPassword(user))
            {
                return new Success();
            }

            return new Fail(ResultError.EmailNotDelivered);
        }

        private async Task<bool> SendEmailPassword(User user)
        {
            try
            {
                return await _email.SendEmailAsync(user, EmailType.CREATERPASSWORD);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
