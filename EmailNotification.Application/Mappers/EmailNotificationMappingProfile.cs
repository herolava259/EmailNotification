
using AutoMapper;
using EmailNotification.Application.Responses;
using EmailNotification.Core.Entities;

namespace EmailNotification.Application.Mappers;

public class EmailNotificationMappingProfile: AutoMapper.Profile
{
    public EmailNotificationMappingProfile()
    {
        CreateMap<UserAccount, AccountResponse>().ReverseMap();
    }
}
