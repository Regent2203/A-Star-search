using System;
using UnityEngine;

namespace ThisProject.SaveSystem.Dto
{
    [Serializable]
    public struct Vector2Dto
    {
        public float X;
        public float Y;

        public Vector2Dto(Vector2 vector)
        {
            X = vector.x;
            Y = vector.y;
        }

        public static explicit operator Vector2(Vector2Dto dto) => new Vector2(dto.X, dto.Y);
        public static implicit operator Vector2Dto(Vector2 vector) => new Vector2Dto(vector);
    }
}