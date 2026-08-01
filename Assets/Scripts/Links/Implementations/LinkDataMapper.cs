using ThisProject.SaveSystem.Dto;
using ThisProject.SaveSystem.Mappers;

namespace ThisProject.Links.Implementations
{
    public class LinkDataMapper : ILinkMapper<LinkData<int>, LinkDataDto<int>, int>
    {
        public LinkDataDto<int> ToDto(LinkData<int> nodeData)
        {
            return new LinkDataDto<int>(nodeData.From, nodeData.To, nodeData.Cost);
        }
    }
}