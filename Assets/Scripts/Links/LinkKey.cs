using System;
using System.Collections.Generic;

namespace ThisProject.Links
{
    public readonly struct LinkKey<TId> : IEquatable<LinkKey<TId>>
        //where TId : IEquatable<TId>
    {
        public TId From { get; }
        public TId To { get; }


        public LinkKey(TId fromId, TId toId)
        {
            From = fromId;
            To = toId;
        }

        public bool Equals(LinkKey<TId> other)
        {
            return EqualityComparer<TId>.Default.Equals(From, other.From) &&
                   EqualityComparer<TId>.Default.Equals(To, other.To);
        }

        public override bool Equals(object obj)
        {
            return obj is LinkKey<TId> other && Equals(other);
        }

        public override int GetHashCode()
        {            
            return HashCode.Combine(From, To);
        }

        public override string ToString()
        {
            return $"LinkKey({From}->{To})";
        }
    }
}
