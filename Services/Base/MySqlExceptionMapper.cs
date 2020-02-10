using MySql.Data.MySqlClient;
using System.Net;

namespace Services
{
    public static class MySqlExpcetionMapper
    {
        public static HttpStatusCode ExceptionHttpCode(MySqlException e)
        {
            switch (e.Number)
            {
                case 1062:  // Duplicate Key
                    return HttpStatusCode.Conflict;

                case 1064: // Bad SQL Query
                    return HttpStatusCode.UnprocessableEntity;

                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
