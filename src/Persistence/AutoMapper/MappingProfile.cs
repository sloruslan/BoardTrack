using Application.DTO.BoardType;
using AutoMapper;
using Domain.DTO.Database.Models;

namespace Persistence.AutoMapper.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBoardTypeRequest, BoardType>();
        CreateMap<BoardType, BoardTypeResponse>();
        CreateMap<UpdateBoardTypeRequest, BoardType>();
    }

}