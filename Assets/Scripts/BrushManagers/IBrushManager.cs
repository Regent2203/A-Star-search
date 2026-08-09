using System;

namespace EasyField.BrushManagers
{
    public interface IBrushManager<TBrushType>
    {
        public TBrushType GetBrush(int brushIndex);
        public void SetBrush(int brushIndex, TBrushType cellType);

        public event Action<int, TBrushType> BrushChanged;
    }
}