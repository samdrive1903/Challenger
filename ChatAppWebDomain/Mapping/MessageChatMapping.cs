using AutoMapper;
using ChatAppWebDomain.Entities.MessageChat;
using ChatAppWebDomain.Shared.ViewModel;

public class MessageChatMapping : Profile
{
    public MessageChatMapping()
    {
        CreateMap<MessageChatViewModel, MessageChatEntity>().ReverseMap()
            .ForMember(dest => dest.Id, mf => mf.MapFrom(src => src.Id))
            .ForMember(dest => dest.Source, mf => mf.MapFrom(src => src.Source))
            .ForMember(dest => dest.Destination, mf => mf.MapFrom(src => src.Destination))
            .ForMember(dest => dest.SentOn, mf => mf.MapFrom(src => src.SentOn))
            .ForMember(dest => dest.IsRead, option => option.Ignore());

        CreateMap<MessageChatEntity, MessageChatViewModel>().ReverseMap()
            .ForMember(dest => dest.Id, mf => mf.MapFrom(src => src.Id))
            .ForMember(dest => dest.Source, mf => mf.MapFrom(src => src.Source))
            .ForMember(dest => dest.Destination, mf => mf.MapFrom(src => src.Destination))
            .ForMember(dest => dest.SentOn, mf => mf.MapFrom(src => src.SentOn))
            .ForMember(dest => dest.IsRead, option => option.Ignore());
    }
}