using Microsoft.VisualStudio.Services.Commerce;

namespace apiSocialWeb.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(int id, Exception? innerException = null) 
            : base($"Erro ao tentar achar o objeto de ID {id}", innerException)
        {
            ResourceId = id;
        }

        public int ResourceId { get; private set;}
    }
}
