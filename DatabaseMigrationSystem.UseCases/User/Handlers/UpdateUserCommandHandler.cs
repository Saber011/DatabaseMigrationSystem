using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.UseCases.User.Commands;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.User.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUpdateUserInfoRepository _updateUserInfoRepository;

        private readonly IGetByIdUserGetRepository _getByIdUserGetRepository;
        
        public UpdateUserCommandHandler(IMapper mapper, IUpdateUserInfoRepository updateUserInfoRepository, IGetByIdUserGetRepository getByIdUserGetRepository)
        {
            _mapper = mapper;
            _updateUserInfoRepository = updateUserInfoRepository;
            _getByIdUserGetRepository = getByIdUserGetRepository;
        }
    
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _getByIdUserGetRepository.Get(request.UserId, cancellationToken);

            user.Login = request.Login;
            
            await _updateUserInfoRepository.Mutate(user, cancellationToken);

            return default;
        }
    }
}