using AutoMapper;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.UseCases.User.Queries;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.User.Handlers;

public class GetUsersQueryHandler: IRequestHandler<GetUsersQuery, UserDto[]>
{
    private readonly IMapper _mapper;
    private readonly IGetAllUsersRepository _getAllUsersRepository;
    
    public GetUsersQueryHandler(IMapper mapper, IGetByIdUserGetRepository getByIdUserGetRepository, IGetAllUsersRepository getAllUsersRepository)
    {
        _mapper = mapper;
        _getAllUsersRepository = getAllUsersRepository;
    }
    
    public async Task<UserDto[]> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _getAllUsersRepository.Get(cancellationToken);
        
        return _mapper.Map<UserDto[]>(users);
    }
}