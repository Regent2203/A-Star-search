using System;
using System.Collections.Generic;

namespace EasyField.BrushManagers
{
    public class BrushManager<TBrushType> : IBrushManager<TBrushType>
    {
        private readonly Dictionary<int, TBrushType> _brushes = new();

        public event Action<int, TBrushType> BrushChanged;


        public TBrushType GetBrush(int brushIndex)
        {
            return _brushes[brushIndex];
        }

        public void SetBrush(int brushIndex, TBrushType cellType)
        {
            _brushes[brushIndex] = cellType;
            BrushChanged?.Invoke(brushIndex, cellType);
        }
    }
}