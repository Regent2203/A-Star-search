using EasyField.SaveSystem;
using EasyField.SceneControllers;

namespace EasyField.Implementations.Cells
{
    public class CellsSaveLoadManager : SaveLoadManager<CellsFieldSaveDto>
    {
        public CellsSaveLoadManager(ISaver saver, ILoader loader, CellsFieldSaveDtoProvider dtoProvider) : base(saver, loader, dtoProvider)
        {
        }
    }
}