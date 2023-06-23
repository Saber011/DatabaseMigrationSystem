using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.UseCases.User.Commands;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.User.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRemoveUserRepository _removeUserRepository;
    
        public DeleteUserCommandHandler(IMapper mapper, IRemoveUserRepository removeUserRepository)
        {
            _mapper = mapper;
            _removeUserRepository = removeUserRepository;
        }
    
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _removeUserRepository.Mutate(request.UserId, cancellationToken);

            return default;
        }
    }
}