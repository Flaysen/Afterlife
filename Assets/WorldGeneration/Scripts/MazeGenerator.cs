using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2 _mazeSize = new Vector2(4,4);
        [SerializeField] private int _roomsCount = 20;
        [SerializeField] private GameObject _entryRoom;

        private List<Vector2> _takenPositions = new List<Vector2>();
        private Room[,] _rooms; 
        private int _gridX, _gridY;

        private void Start()
        {
            if (_roomsCount >= (_mazeSize.x * 2) * (_mazeSize.y * 2))
            { 
                _roomsCount = (int)(_mazeSize.x * 2 * _mazeSize.y * 2);
            }  

            _gridX = (int)(_mazeSize.x);
            _gridY = (int)(_mazeSize.y);

            GenerateRooms();
            SetRoomDoors();
            InstantiateMaze();
        }

        private void GenerateRooms() 
        {
            _rooms = new Room[_gridX * 2, _gridY * 2];

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

                if (GetNumberOfNeighbors(checkPos, _takenPositions) > 1 && Random.value > randomCompare)
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
            Vector2 checkingPos = Vector2.zero;

            do
            {
                int index = 0;
                int attempts = 0;

                int i = Mathf.RoundToInt(Random.value * (_takenPositions.Count - 1));
                do
                {
                    i = Mathf.RoundToInt(Random.value * (_takenPositions.Count - 1));
                    attempts++;
                } while (GetNumberOfNeighbors(_takenPositions[index], _takenPositions) > 1 && attempts < 100 && lookForOneNeighbor);

                x = (int)_takenPositions[i].x;
                y = (int)_takenPositions[i].y;

                checkingPos = (Random.value < 0.5f) ?

                    (Random.value < 0.5) ? new Vector2(x, y + 1) : new Vector2(x, y - 1) :

                    (Random.value < 0.5) ? new Vector2(x + 1, y) : new Vector2(x - 1, y);

            }
            while (_takenPositions.Contains(checkingPos) || x >= _gridX || x < -_gridX || y >= -_gridY || y < -_gridY);
            
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

                    _rooms[x, y].DoorBot = (y + 1 >= _gridY * 2) ? false : _rooms[x, y + 1] != null;

                    _rooms[x, y].DoorBot = (x - 1 < 0) ? false : _rooms[x - 1, y] != null;

                    _rooms[x, y].DoorBot = (x + 1 >= _gridX * 2) ? false : _rooms[x + 1, y] != null;
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
                Vector2 drawPos = room.GridPosition;
                drawPos.x *= 16;
                drawPos.y *= 16;
        
                MazeRoomSelector mapper = Object.Instantiate(_entryRoom, new Vector3(drawPos.x, 0, drawPos.y), Quaternion.identity).GetComponent<MazeRoomSelector>();
                mapper.RoomType = room.RoomType;
                mapper.Up = room.DoorTop;
                mapper.Down = room.DoorBot;
                mapper.Right = room.DoorRight;
                mapper.Left = room.DoorLeft;
                //mapper.gameObject.transform.parent = mapRoot;
            }
        }
    }
}

