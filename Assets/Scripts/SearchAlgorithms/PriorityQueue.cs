using System;
using System.Collections.Generic;

namespace Core.SearchAlgorithms
{
    public class PriorityQueue<T> //AI-generated
    {
        private List<(T Obj, float Priority)> _items = new();

        public int Count => _items.Count;

        public void Enqueue(T item, float priority) => _items.Add((item, priority));

        public T Dequeue()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            int bestIndex = 0;
            for (int i = 1; i < _items.Count; i++)
            {
                if (_items[i].Priority < _items[bestIndex].Priority)
                    bestIndex = i;
            }

            var bestItem = _items[bestIndex].Obj;

            // optimization of deleting from list
            _items[bestIndex] = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);

            return bestItem;
        }

        public void Clear() => _items.Clear();
    }
}