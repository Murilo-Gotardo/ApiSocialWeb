namespace apiSocialWeb.Exceptions
{
    public class SearchEmptyException : Exception
    {
        public SearchEmptyException(string message, Exception? innerException = null) : base(message, innerException)
        {

        }
    }
}
