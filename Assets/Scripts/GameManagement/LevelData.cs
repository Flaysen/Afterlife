using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "level_data", menuName = "Levels/Level Data", order = 55)]
public class LevelData : ScriptableObject
{
    [SerializeField] private Vector2 _mazeSize;
    [SerializeField] private int _roomsCount;
    [SerializeField] private int _obstacleRoomsCount;

    public Vector2 MazeSize => _mazeSize;
    public int RoomsCount => _roomsCount;
    public int ObstacleRoomsCount => _obstacleRoomsCount;
}
