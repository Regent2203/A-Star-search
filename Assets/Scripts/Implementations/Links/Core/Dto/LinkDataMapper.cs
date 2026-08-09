using EasyField.SaveSystem.Dto;
using EasyField.SaveSystem.Dto.Mappers;

namespace EasyField.Links.Implementations
{
    public class LinkDataMapper : ILinkDataMapper<LinkData<int>, LinkDataDto<int>, int>
    {
        public LinkDataDto<int> ToDto(LinkData<int> nodeData)
        {
            return new LinkDataDto<int>(nodeData.From, nodeData.To, nodeData.Cost);
        }
    }
}