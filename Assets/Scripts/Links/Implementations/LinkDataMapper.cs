using EasyField.SaveSystem.Dto;
using EasyField.SaveSystem.Mappers;

namespace EasyField.Links.Implementations
{
    public class LinkDataMapper : ILinkMapper<LinkData<int>, LinkDataDto<int>, int>
    {
        public LinkDataDto<int> ToDto(LinkData<int> nodeData)
        {
            return new LinkDataDto<int>(nodeData.From, nodeData.To, nodeData.Cost);
        }
    }
}