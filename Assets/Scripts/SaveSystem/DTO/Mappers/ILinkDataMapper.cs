using EasyField.Links;

namespace EasyField.SaveSystem.Dto.Mappers
{
    public interface ILinkDataMapper<TLinkData, TLinkDataDto, TId>
        where TLinkData : ILinkData<TId>
        where TLinkDataDto : LinkDataDto<TId>
    {
        public TLinkDataDto ToDto(TLinkData nodeData);
    }
}