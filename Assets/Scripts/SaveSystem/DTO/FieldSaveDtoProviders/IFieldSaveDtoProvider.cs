using EasyField.SaveSystem.Dto;

namespace EasyField.SaveSystem
{
    public interface IFieldSaveDtoProvider<TFieldSaveDto, TNodeDataDto, TLinkDataDto>
        where TFieldSaveDto : FieldSaveDto<TNodeDataDto, TLinkDataDto>, new()
    {
        public TFieldSaveDto GetDto();
    }
}