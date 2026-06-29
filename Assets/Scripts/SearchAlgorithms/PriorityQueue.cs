using System;
using System.Collections.Generic;

namespace ThisProject.SearchAlgorithms
{
    public class PriorityQueue<T>
    {
        private readonly List<(T Obj, float Priority)> _heap = new();

        public int Count => _heap.Count;

        public void Enqueue(T item, float priority)
        {
            _heap.Add((item, priority));
            MoveUp(_heap.Count - 1);
        }

        public T Dequeue()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T rootItem = _heap[0].Obj;
            int lastIndex = _heap.Count - 1;

            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);

            if (_heap.Count > 0)
                MoveDown(0);

            return rootItem;
        }

        public void Clear() => _heap.Clear();

        private void MoveUp(int childIndex)
        {
            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;

                if (_heap[childIndex].Priority >= _heap[parentIndex].Priority)
                    break;

                Swap(childIndex, parentIndex);
                childIndex = parentIndex;
            }
        }

        private void MoveDown(int parentIndex)
        {
            int lastIndex = _heap.Count - 1;

            while (true)
            {
                int leftChild = 2 * parentIndex + 1;
                int rightChild = leftChild + 1;
                int bestIndex = parentIndex;

                if (leftChild <= lastIndex && _heap[leftChild].Priority < _heap[bestIndex].Priority)
                    bestIndex = leftChild;

                if (rightChild <= lastIndex && _heap[rightChild].Priority < _heap[bestIndex].Priority)
                    bestIndex = rightChild;

                if (bestIndex == parentIndex)
                    break;

                Swap(parentIndex, bestIndex);
                parentIndex = bestIndex;
            }
        }

        private void Swap(int i, int j)
        {
            var temp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = temp;
        }
    }
}