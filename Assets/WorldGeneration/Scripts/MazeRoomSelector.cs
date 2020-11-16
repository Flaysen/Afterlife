using System.Collections;
using System.Collections.Generic;
using Afterlife.Assets.WorldGeneration.Scripts;
using UnityEngine;

namespace LevelGeneration 
{
    public class MazeRoomSelector : MonoBehaviour
    {
        [SerializeField] private GameObject _entryRoom;

        [SerializeField] private GameObject _roomU, _roomD, _roomR, _roomL,
            _roomUD, _roomRL, _roomUR, _roomUL, _roomDR, _roomDL,
            _roomULD, _roomRUL, _roomDRU, _roomLDR,
            _roomUDRL;

        [SerializeField] private GameObject [] _obstacleRoomsUD, _obstacleRoomsLR;
        [SerializeField] private GameObject _exitRoom;

        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }

        private bool _up => Up;
        private bool _down => Down;
        private bool _left => Left;
        private bool _right => Right;

        public RoomType RoomType { get; set; }
   
        private void Start()
        {
            SelectRoom();
        }

        private void SelectRoom()
        {
            GameObject roomToInstantiate = null;

            roomToInstantiate = (_up) ?

                (_down) ?

                    (_right) ? 

                        (_left) ? _roomUDRL : _roomDRU
                                :
                        (_left) ? _roomULD : (RoomType == RoomType.OBSTACLE) ?
                        
                            _obstacleRoomsUD[Random.Range(0, _obstacleRoomsUD.Length)] : _roomUD

                        :

                  (_right) ? 

                        (_left) ? _roomRUL : _roomUR
                                :
                        (_left) ? _roomUL : _roomU

                                        :
                 (_down) ?

                    (_right) ? 

                        (_left) ? _roomLDR : _roomDR
                                :
                        (_left) ? _roomDL : _roomD

                        :

                  (_right) ? 

                        (_left) ? (RoomType == RoomType.OBSTACLE) ?

                            _obstacleRoomsLR[Random.Range(0, _obstacleRoomsLR.Length)] : _roomRL : _roomR
                        :                    
                        _roomL;
                
            if (RoomType == RoomType.EXIT) roomToInstantiate = _exitRoom;
         
            roomToInstantiate.GetComponent<RoomController>().RoomType = RoomType;
            
            Instantiate(roomToInstantiate, transform.position, transform.rotation, transform);
        }
    }
}


