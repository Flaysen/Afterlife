using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration 
{
    public class Room
    {
        public bool DoorTop { get; set; }
        public bool DoorBot { get; set; }
        public bool DoorLeft { get; set; }
        public bool DoorRight { get; set; }
        public Vector2 GridPosition { get; set; }
        public int RoomType { get; set; }

        private bool _doorTop => DoorTop; 
        private bool _doorBot => DoorBot; 
        private bool _doorLeft => DoorLeft; 
        private bool _doorRight => DoorRight; 
        private Vector2 _gridPosition => GridPosition;
        private int _roomType => RoomType;
 
        public Room(Vector2 gridPosition, int roomType)
        {
            GridPosition = gridPosition;
            RoomType = roomType;
        }
    }
}
