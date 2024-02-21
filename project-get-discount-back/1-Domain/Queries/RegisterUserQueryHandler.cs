﻿using MediatR;
using project_get_discount_back._1_Domain.Interfaces;
using project_get_discount_back.Entities;
using project_get_discount_back.Interfaces;
using project_get_discount_back.Results;
using static project_get_discount_back.Entities.User;

namespace project_get_discount_back.Queries
{
    public record RegisterUserQuery(string name, string email, string role) : IRequest<Result>;

    public class RegisterUserQueryHandler : IRequestHandler<RegisterUserQuery, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserQueryHandler(IUserRepository userRepository, IUserService userService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(RegisterUserQuery request, CancellationToken cancellationToken)
        {
            var validationResult = ValidateRequest(request);
            if (validationResult != null)
            {
                return validationResult;
            }

            var user = await _userRepository.GetByEmail(request.email, cancellationToken);
            var userCreate = _userService.ObterUsername();

            if (user == null)
            {

                AccessType role = (AccessType)Enum.Parse(typeof(AccessType), request.role);

                var newUser = new User(request.name, request.email, role, userCreate);
                _userRepository.Create(newUser);
                await _unitOfWork.Commit(cancellationToken);
                return new Success();

            }
            else
            {
                if (user.Deleted)
                {
                    user.ActivateUser(userCreate);
                    _userRepository.Update(user);
                    await _unitOfWork.Commit(cancellationToken);
                    return new Success();
                }
                else
                {
                    return new Fail(ResultError.AlreadyRegisteredUser);
                }

            }
        }

        private static Fail? ValidateRequest(RegisterUserQuery request)
        {
            if (string.IsNullOrEmpty(request.name))
            {
                return new Fail(ResultError.RegisterNameEmpty);
            }

            if (string.IsNullOrEmpty(request.email))
            {
                return new Fail(ResultError.RegisterEmailEmpty);
            }

            if (string.IsNullOrEmpty(request.role) || !Enum.IsDefined(typeof(AccessType), request.role))
            {
                return new Fail(ResultError.RegisterRoleInvalido);
            }

            return null;
        }
    }
}