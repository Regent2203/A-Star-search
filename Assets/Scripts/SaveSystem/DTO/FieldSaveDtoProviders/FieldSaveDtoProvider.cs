using System.Linq;
using ThisProject.Links;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.SaveSystem.Dto;
using ThisProject.SaveSystem.Mappers;

namespace ThisProject.SaveSystem
{
    public class FieldSaveDtoProvider<TFieldSaveDto, TNodeData, TNodeDataDto, TLinkData, TLinkDataDto, TId>
        : IFieldSaveDtoProvider<TFieldSaveDto, TNodeDataDto, TLinkDataDto>
        where TFieldSaveDto : FieldSaveDto<TNodeDataDto, TLinkDataDto>, new()
        where TNodeData : INodeData<TId>
        where TNodeDataDto : NodeDataDto<TId>
        where TLinkData : ILinkData<TId>
        where TLinkDataDto : LinkDataDto<TId>
    {
        private readonly IObjectsStorage<TNodeData, TId> _nodes;
        private readonly IObjectsStorage<ILinkData<TId>, LinkKey<TId>> _links;
        private readonly IMapper<TNodeData, TNodeDataDto, TId> _nodeMapper;


        public FieldSaveDtoProvider(
            IObjectsStorage<TNodeData, TId> nodes,
            IObjectsStorage<ILinkData<TId>, LinkKey<TId>> links,
            IMapper<TNodeData, TNodeDataDto, TId> nodeMapper)
        {
            _nodes = nodes;
            _links = links;
            _nodeMapper = nodeMapper;
        }

        public virtual TFieldSaveDto GetDto()
        {
            var fieldSaveDto = new TFieldSaveDto
            {
                Nodes = _nodes.AllItems.Select(node => _nodeMapper.ToDto(node)).ToList(),
                //Links = _linkDatas.AllItems.Select(link => _linkMapper.ToDto(link)).ToList(), //todo
            };

            return fieldSaveDto;
        }
    }
}