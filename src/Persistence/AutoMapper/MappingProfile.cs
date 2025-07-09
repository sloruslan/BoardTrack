using Application.DTO.Board;
using Application.DTO.BoardHistory;
using Application.DTO.BoardType;
using Application.Extensions;
using AutoMapper;
using Domain.DTO.Database.Models;
using Domain.Enums;

namespace Persistence.AutoMapper.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBoardTypeRequest, BoardType>();
        CreateMap<BoardType, BoardTypeResponse>();
        CreateMap<UpdateBoardTypeRequest, BoardType>();

        CreateMap<CreateBoardRequest, Board>();
        CreateMap<Board, BoardResponse>()
           .ForMember(x => x.CurrentStepName, opt => opt.MapFrom(y => ((ProductionStepEnum)y.CurrentStepId).ToRussianName()));

        CreateMap<BoardHistory, BoardHistoryResponse>();

    }

}