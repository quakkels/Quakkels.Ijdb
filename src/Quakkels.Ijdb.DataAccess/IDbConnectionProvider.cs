namespace Quakkels.Ijdb.DataAccess
{
    public interface IDbConnectionProvider
    {
        IDatabaseWrapper GetDbConnection();
    }
}