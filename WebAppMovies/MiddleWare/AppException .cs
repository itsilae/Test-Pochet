namespace WebAppMovies.MiddleWare
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message)
        {
        }
    }
}
