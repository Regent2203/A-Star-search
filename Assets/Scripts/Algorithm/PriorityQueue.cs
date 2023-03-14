using System.Collections.Generic;

namespace Algorithm
{
    public class PriorityQueue<T>
    {
        private class Item
        {
            public readonly T Obj;
            public readonly float Priority;

            public Item(T I, float p)
            {
                Obj = I;
                Priority = p;
            }
        }

        private List<Item> _items = new List<Item>();

        public int Count => _items.Count;

        public void Enqueue(T item, float priority)
        {
            _items.Add(new Item(item, priority));
        }

        public T Dequeue()
        {
            int bestIndex = 0;

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Priority < _items[bestIndex].Priority)
                {
                    bestIndex = i;
                }
            }

            T bestItem = _items[bestIndex].Obj;
            _items.RemoveAt(bestIndex);
            return bestItem;
        }

        public void Clear()
        {
            for (int i = _items.Count - 1; i >= 0; i--)
            {
                _items.RemoveAt(i);
            }
        }
    }
}