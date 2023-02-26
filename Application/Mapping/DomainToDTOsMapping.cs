using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.CommentsAggregate;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using AutoMapper;

namespace apiSocialWeb.Application.Mapping
{
    public class DomainToDTOsMapping : Profile
    {
        public DomainToDTOsMapping() 
        { 
            CreateMap<User, UserDTO>();

            CreateMap<Posts, PostDTO>();

            CreateMap<Comments, CommentsDTO>();
        }
    }
}
