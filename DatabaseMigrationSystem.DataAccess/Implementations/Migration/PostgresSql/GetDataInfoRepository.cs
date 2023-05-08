using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.PostgresSql;

public class GetDataInfoRepository : IGetDataInfoRepository
{
    public Task<int> Get(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}