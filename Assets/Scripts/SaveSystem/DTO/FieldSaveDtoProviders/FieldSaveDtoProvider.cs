using System.Linq;
using EasyField.Links;
using EasyField.Nodes;
using EasyField.ObjectsStorages;
using EasyField.SaveSystem.Dto;
using EasyField.SaveSystem.Mappers;

namespace EasyField.SaveSystem
{
    public class FieldSaveDtoProvider<TFieldSaveDto, TNodeData, TNodeDataDto, TLinkData, TLinkDataDto, TId>
        : IFieldSaveDtoProvider<TFieldSaveDto, TNodeDataDto, TLinkDataDto>
        where TFieldSaveDto : FieldSaveDto<TNodeDataDto, TLinkDataDto>, new()
        where TNodeData : INodeData<TId>
        where TNodeDataDto : NodeDataDto<TId>
        where TLinkData : ILinkData<TId>
        where TLinkDataDto : LinkDataDto<TId>
    {
        private readonly IObjectsStorage<TNodeData, TId> _nodeDatas;
        private readonly IObjectsStorage<TLinkData, LinkKey<TId>> _linkDatas;
        private readonly INodeDataMapper<TNodeData, TNodeDataDto, TId> _nodesMapper;
        private readonly ILinkDataMapper<TLinkData, TLinkDataDto, TId> _linksMapper;


        public FieldSaveDtoProvider(
            IObjectsStorage<TNodeData, TId> nodeDatas,
            IObjectsStorage<TLinkData, LinkKey<TId>> linkDatas,
            INodeDataMapper<TNodeData, TNodeDataDto, TId> nodesMapper,
            ILinkDataMapper<TLinkData, TLinkDataDto, TId> linksMapper)
        {
            _nodeDatas = nodeDatas;
            _linkDatas = linkDatas;
            _nodesMapper = nodesMapper;
            _linksMapper = linksMapper;
        }

        public virtual TFieldSaveDto GetDto()
        {
            var fieldSaveDto = new TFieldSaveDto
            {
                Nodes = _nodeDatas.AllItems.Select(node => _nodesMapper.ToDto(node)).ToList(),
                Links = _linkDatas.AllItems.Select(link => _linksMapper.ToDto(link)).ToList(),
            };

            return fieldSaveDto;
        }
    }
}