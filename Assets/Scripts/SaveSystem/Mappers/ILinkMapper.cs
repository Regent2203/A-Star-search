using EasyField.Links;
using EasyField.SaveSystem.Dto;

namespace EasyField.SaveSystem.Mappers
{
    public interface ILinkMapper<TLinkData, TLinkDataDto, TId>
        where TLinkData : ILinkData<TId>
        where TLinkDataDto : LinkDataDto<TId>
    {
        public TLinkDataDto ToDto(TLinkData nodeData);
    }
}