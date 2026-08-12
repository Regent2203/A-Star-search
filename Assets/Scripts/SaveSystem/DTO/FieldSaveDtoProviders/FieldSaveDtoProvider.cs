using EasyField.Implementations.Links;
using EasyField.Links;
using EasyField.Nodes;
using EasyField.Nodes.Dto;
using EasyField.ObjectsStorages;
using EasyField.SaveSystem.Dto.Mappers;
using System.Linq;

namespace EasyField.SaveSystem.Dto.FieldSaveDtoProviders
{
    public class FieldSaveDtoProvider<TFieldSaveDto, TNodeData, TNodeDataDto, TLinkData, TLinkDataDto, TId>
        : FieldSaveDtoProvider<TFieldSaveDto, TNodeData, TNodeDataDto, TId>,
          IFieldSaveDtoProvider<TFieldSaveDto, TNodeDataDto, TLinkDataDto>
        where TFieldSaveDto : FieldSaveDto<TNodeDataDto, TLinkDataDto>, new()
        where TNodeData : INodeData<TId>
        where TNodeDataDto : NodeDataDto<TId>
        where TLinkData : ILinkData<TId>
        where TLinkDataDto : LinkDataDto<TId>
    {
        protected readonly IObjectsStorage<TLinkData, DualKey<TId>> _linkDatas;
        protected readonly ILinkDataMapper<TLinkData, TLinkDataDto, TId> _linksMapper;


        public FieldSaveDtoProvider(IObjectsStorage<TNodeData, TId> nodeDatas, IObjectsStorage<TLinkData, DualKey<TId>> linkDatas, 
            INodeDataMapper<TNodeData, TNodeDataDto, TId> nodesMapper, ILinkDataMapper<TLinkData, TLinkDataDto, TId> linksMapper)
            : base(nodeDatas, nodesMapper)
        {
            _linkDatas = linkDatas;
            _linksMapper = linksMapper;
        }

        public override TFieldSaveDto GetDto()
        {
            var dto = new TFieldSaveDto();

            PrepareNodes(dto);
            PrepareLinks(dto);            

            return dto;
        }

        protected void PrepareLinks(FieldSaveDto<TNodeDataDto, TLinkDataDto> dto)
        {
            dto.Links = _linkDatas.AllItems.Select(link => _linksMapper.ToDto(link)).ToList();
        }
    }

    public class FieldSaveDtoProvider<TFieldSaveDto, TNodeData, TNodeDataDto, TId>
        : IFieldSaveDtoProvider<TFieldSaveDto, TNodeDataDto>
        where TFieldSaveDto : FieldSaveDto<TNodeDataDto>, new()
        where TNodeData : INodeData<TId>
        where TNodeDataDto : NodeDataDto<TId>
    {
        protected readonly IObjectsStorage<TNodeData, TId> _nodeDatas;
        protected readonly INodeDataMapper<TNodeData, TNodeDataDto, TId> _nodesMapper;


        public FieldSaveDtoProvider(IObjectsStorage<TNodeData, TId> nodeDatas, INodeDataMapper<TNodeData, TNodeDataDto, TId> nodesMapper)
        {
            _nodeDatas = nodeDatas;
            _nodesMapper = nodesMapper;
        }

        public virtual TFieldSaveDto GetDto()
        {
            var dto = new TFieldSaveDto();

            PrepareNodes(dto);

            return dto;
        }

        protected void PrepareNodes(FieldSaveDto<TNodeDataDto> dto)
        {
            dto.Nodes = _nodeDatas.AllItems.Select(node => _nodesMapper.ToDto(node)).ToList();
        }
    }
}