using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2 _mazeSize;
        [SerializeField] private int _roomsCount;
        [SerializeField] private GameObject _roomSpawner;
        [SerializeField] private GameObject _entryRoom;

        private List<Vector2> _takenPositions = new List<Vector2>();
        private Room[,] _rooms; 
        private int _gridX, _gridY;  
        public event Action<List<Vector2>> OnMazeGenerated;

        private void Start()
        {

            if (_roomsCount >= (_mazeSize.x * 2) * (_mazeSize.y * 2))
            { 
                _roomsCount = Mathf.RoundToInt((_mazeSize.x * 2) * (_mazeSize.y * 2));
            }  

            _gridX = Mathf.RoundToInt(_mazeSize.x);
            _gridY = Mathf.RoundToInt(_mazeSize.y);

            GenerateRooms();
            SetRoomDoors();
            InstantiateMaze();

            OnMazeGenerated?.Invoke(_takenPositions);
        }

        private void GenerateRooms() 
        {
            _rooms = new Room[_gridX * 2, _gridY * 2];;

            _rooms[_gridX, _gridY] = new Room(Vector2.zero, 1);

            _takenPositions.Insert(0, Vector2.zero);

            Vector2 checkPos = Vector2.zero;
            
            //magic numbers
            float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

            for(int i = 0; i < _roomsCount - 1; i++)
            {
                float randomPerc = (i / (_roomsCount - 1));

                randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);

                checkPos = NewPosition(false);

                if (GetNumberOfNeighbors(checkPos, _takenPositions) > 1 && UnityEngine.Random.value > randomCompare)
                {
                    int j = 0;
                    do
                    {
                        checkPos = NewPosition(true);
                        j++;
                    } while (GetNumberOfNeighbors(checkPos, _takenPositions) > 1 && j < 100);
                }
                 _rooms[(int)checkPos.x + _gridX, (int)checkPos.y + _gridY] = new Room(checkPos, 0);
                _takenPositions.Insert(0, checkPos);
            }      
        }

        private Vector2 NewPosition(bool lookForOneNeighbor)
        {
            int x = 0, y = 0;
            int i = 0;
            Vector2 checkingPos = Vector2.zero;

            do
            {
                int attempts = 0;  

                if(lookForOneNeighbor)
                {
                    do
                    {
                        i = Mathf.RoundToInt(UnityEngine.Random.value * (_takenPositions.Count - 1));
                        attempts++;
                    } while (GetNumberOfNeighbors(_takenPositions[i], _takenPositions) > 1 && attempts < 100);
                }
                else i = Mathf.RoundToInt(UnityEngine.Random.value * (_takenPositions.Count - 1));
               
                x = (int)_takenPositions[i].x;
                y = (int)_takenPositions[i].y;

                checkingPos = (UnityEngine.Random.value < 0.5f) ?

                     (UnityEngine.Random.value < 0.5f) ? new Vector2(x, y + 1) : new Vector2(x, y - 1) :

                     (UnityEngine.Random.value < 0.5f) ? new Vector2(x + 1, y) : new Vector2(x - 1, y);

            } while (_takenPositions.Contains(checkingPos) || checkingPos.x >= _gridX || checkingPos.x < -_gridX || checkingPos.y >= _gridY || checkingPos.y < -_gridY);
           
            return checkingPos;
        }
 
        private int GetNumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
        {
            int ret = 0;

            if (usedPositions.Contains(checkingPos + Vector2.right)) ret++;
        
            if (usedPositions.Contains(checkingPos + Vector2.left)) ret++;
         
            if (usedPositions.Contains(checkingPos + Vector2.up))  ret++;
          
            if (usedPositions.Contains(checkingPos + Vector2.down))  ret++;
        
            return ret;
        }

        private void SetRoomDoors()
        {
            for (int x = 0; x < ((_gridX * 2)); x++)
            {
                for (int y = 0; y < ((_gridY * 2)); y++)
                {
                    if(_rooms[x, y] == null) continue;

                    Vector2 gridPosition = new Vector2(x, y);

                    _rooms[x, y].DoorBot = (y - 1 < 0) ? false : _rooms[x, y - 1] != null;

                    _rooms[x, y].DoorTop = (y + 1 >= _gridY * 2) ? false : _rooms[x, y + 1] != null;

                    _rooms[x, y].DoorLeft = (x - 1 < 0) ? false : _rooms[x - 1, y] != null;

                    _rooms[x, y].DoorRight = (x + 1 >= _gridX * 2) ? false : _rooms[x + 1, y] != null;
                }   
            }
        }

        private void InstantiateMaze()
        {
            foreach (Room room in _rooms)
            {
                if (room is null)
                {
                    continue; 
                }
                Vector2 spawnPosition = room.GridPosition;
                spawnPosition.x *= 32;
                spawnPosition.y *= 33;
        
                MazeRoomSelector roomSelector = Instantiate(_roomSpawner, new Vector3(spawnPosition.x, 0, spawnPosition.y), Quaternion.identity).GetComponent<MazeRoomSelector>();
                //mapper.RoomType = room.RoomType;
                roomSelector.Up = room.DoorTop;
                roomSelector.Down = room.DoorBot;
                roomSelector.Right = room.DoorRight;
                roomSelector.Left = room.DoorLeft;
            }
        }
    }
}

