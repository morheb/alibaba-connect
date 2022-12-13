namespace alibaba.Common
{
    using System.Data;

    public interface IDbSettings
    {
        IDbConnection Connection();
    }
}
