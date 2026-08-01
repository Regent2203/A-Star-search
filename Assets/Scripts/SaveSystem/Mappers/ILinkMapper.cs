using ThisProject.Links;
using ThisProject.SaveSystem.Dto;

namespace ThisProject.SaveSystem.Mappers
{
    public interface ILinkMapper<TLinkData, TLinkDataDto, TId>
        where TLinkData : ILinkData<TId>
        where TLinkDataDto : LinkDataDto<TId>
    {
        public TLinkDataDto ToDto(TLinkData nodeData);
    }
}