using EasyField.Links;
using EasyField.SaveSystem.Dto.Mappers;

namespace EasyField.Implementations.Links
{
    public class LinkDataMapper<TId> : ILinkDataMapper<LinkData<TId>, LinkDataDto<TId>, TId>
    {
        public LinkDataDto<TId> ToDto(LinkData<TId> nodeData)
        {
            return new LinkDataDto<TId>(nodeData.From, nodeData.To, nodeData.Cost);
        }
    }
}