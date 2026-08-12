namespace EasyField.Fields.FieldBuilders
{
    public interface IFieldBuilder
    {
        //public void BuildFromDto<TFieldSaveDto>(TFieldSaveDto fieldSaveDto);
        public void CreateNewField(int sizeX, int sizeY);
        public void ClearAll();
    }
}
