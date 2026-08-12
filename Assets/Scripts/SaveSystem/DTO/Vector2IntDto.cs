using System;
using UnityEngine;

namespace EasyField.SaveSystem.Dto
{
    [Serializable]
    public struct Vector2IntDto
    {
        public int X;
        public int Y;

        public Vector2IntDto(Vector2Int vector)
        {
            X = vector.x;
            Y = vector.y;
        }

        public static explicit operator Vector2Int(Vector2IntDto dto) => new Vector2Int(dto.X, dto.Y);
        public static implicit operator Vector2IntDto(Vector2Int vector) => new Vector2IntDto(vector);
    }
}