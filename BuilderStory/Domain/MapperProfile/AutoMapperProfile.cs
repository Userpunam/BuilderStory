using AutoMapper;
using BuilderStory.Contract;
using BuilderStory.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BuilderStory.MapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Simple mapping when property names match
            CreateMap<CreateStoryRequestDto, Story>();

            // Bidirectional mapping for CountWord DTOs
            CreateMap<CountWordRequestDto, CountWordResponseDto>().ReverseMap();

            // StoryImage → UploadImageResponseDto
            CreateMap<StoryImage, UploadImageResponseDto>()
                .ForMember(dest => dest.Storyid, opt => opt.MapFrom(src => src.StoryId))
                .ForMember(dest => dest.UploadAt, opt => opt.MapFrom(src => src.UploadedAt));
        }
    }
}
