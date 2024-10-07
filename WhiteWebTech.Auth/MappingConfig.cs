using AutoMapper;
using WhiteWebTech.Auth.Models;

namespace WhiteWebTech.Auth
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ApplicationUsers, UserDto>();
                config.CreateMap<UserDto, ApplicationUsers>();
            });
            return mappingConfig;

        }

    }
}
