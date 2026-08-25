using EasyField.SaveSystem;
using EasyField.SceneControllers;

namespace EasyField.Implementations.Cells.DynamicCells
{
    public class DynamicCellsSaveLoadManager : SaveLoadManager<DynamicCellsFieldSaveDto>
    {
        public DynamicCellsSaveLoadManager(ISaver saver, ILoader loader, DynamicCellsFieldSaveDtoProvider dtoProvider) : base(saver, loader, dtoProvider)
        {
        }
    }
}