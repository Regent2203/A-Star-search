using System;
using System.Collections.Generic;

namespace EasyField
{
    public readonly struct DualKey<TId> : IEquatable<DualKey<TId>>
    {
        public TId From { get; }
        public TId To { get; }


        public DualKey(TId fromId, TId toId)
        {
            From = fromId;
            To = toId;
        }

        public bool Equals(DualKey<TId> other)
        {
            return EqualityComparer<TId>.Default.Equals(From, other.From) &&
                   EqualityComparer<TId>.Default.Equals(To, other.To);
        }

        public override bool Equals(object obj) => obj is DualKey<TId> other && Equals(other);

        public override int GetHashCode()
        {
            return HashCode.Combine(From, To);
        }

        public override string ToString()
        {
            return $"DualKey({From}->{To})";
        }
    }
}