using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration 
{
    public class MazeRoomSelector : MonoBehaviour
    {
        [SerializeField] private GameObject roomU, roomD, roomR, RoomL,
            roomUD, roomRL, roomUR, roomUL, roomDR, roomDL,
            roomULD, roomRUL, roomDRU, roomLDR,
            roomUDRL;

        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }

        private bool _up => Up;
        private bool _down => Down;
        private bool _left => Left;
        private bool _right => Right;

        public int RoomType { get; set; }
        
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

                        (_left) ? roomUDRL : roomDRU
                                :
                        (_left) ? roomULD : roomUD

                        :

                  (_right) ? 

                        (_left) ? roomRUL : roomUR
                                :
                        (_left) ? roomUL : roomU

                                        :
                 (_down) ?

                    (_right) ? 

                        (_left) ? roomLDR : roomDR
                                :
                        (_left) ? roomDL : roomD

                        :

                  (_right) ? 

                        (_left) ? roomRL : roomR
                                :
                        roomR;
            
            Instantiate(roomToInstantiate, transform.position, Quaternion.identity);
        }
    }
}


