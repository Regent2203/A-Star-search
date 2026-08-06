namespace EasyField.Fields.FieldBuilders
{
    //todo
    public interface IFieldBuilder
    {
        public void CreateFromDto();
        public void CreateEmpty();
        public void ClearAll();
        
        public void CreateFieldItem();
        public void DeleteFieldItem();
    }
}