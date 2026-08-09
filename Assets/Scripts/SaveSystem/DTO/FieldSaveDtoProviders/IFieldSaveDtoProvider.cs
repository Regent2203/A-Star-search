namespace EasyField.SaveSystem.Dto.FieldSaveDtoProviders
{
    public interface IFieldSaveDtoProvider<out TFieldSaveDto, TNodeDataDto, TLinkDataDto> : IFieldSaveDtoProvider<TFieldSaveDto>
        where TFieldSaveDto : FieldSaveDto<TNodeDataDto, TLinkDataDto>
    { }

    public interface IFieldSaveDtoProvider<out TFieldSaveDto, TNodeDataDto> : IFieldSaveDtoProvider<TFieldSaveDto>
        where TFieldSaveDto : FieldSaveDto<TNodeDataDto>
    { }

    public interface IFieldSaveDtoProvider<out TFieldSaveDto>
    {
        public TFieldSaveDto GetDto();
    }
}