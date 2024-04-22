namespace NatixisWebChatInfrastructure.Mappers
{
    using AutoMapper;
    using NatixisWebChatDomain.AppEntities;
    using NatixisWebChatModels.AppModels;

    public class AppMappers : Profile
    {
        public AppMappers()
        {
            CreateMap<UserEntity, UsersModel>().ReverseMap();

            CreateMap<GroupEntity, GroupModel>().ReverseMap();
        }
    }
}
