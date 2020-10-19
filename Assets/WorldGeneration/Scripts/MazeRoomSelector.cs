using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration 
{
    public class MazeRoomSelector : MonoBehaviour
    {
        [SerializeField] private GameObject roomU, roomD, roomR, roomL,
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
                        roomL;
            
            Instantiate(roomToInstantiate, transform.position, Quaternion.identity);
        }

    //     if (_up)
    //     {
    //         if (_down)
    //         {
    //             if (_right)
    //             {
    //                 if (_left)
    //                 {
    //                     Instantiate(roomUDRL, transform.position, Quaternion.identity);
    //                 }
    //                 else
    //                 {
    //                     Instantiate(roomDRU, transform.position, Quaternion.identity);
    //                 }
    //             }
    //             else if (_left)
    //             {
    //                 Instantiate(roomULD, transform.position, Quaternion.identity);
    //             }
    //             else
    //             {
    //                 Instantiate(roomUD, transform.position, Quaternion.identity);
    //             }
    //         }
    //         else
    //         {
    //             if (_right)
    //             {
    //                 if (_left)
    //                 {
    //                     Instantiate(roomRUL, transform.position, Quaternion.identity);
    //                 }
    //                 else
    //                 {
    //                     Instantiate(roomUR, transform.position, Quaternion.identity);
    //                 }
    //             }
    //             else if (_left)
    //             {
    //                 Instantiate(roomUL, transform.position, Quaternion.identity);
    //             }
    //             else
    //             {
    //                 Instantiate(roomU, transform.position, Quaternion.identity);
    //             }
    //         }
    //         return;
    //     }
    //     if (_down)
    //     {
    //         if (_right)
    //         {
    //             if (_left)
    //             {
    //                 Instantiate(roomLDR, transform.position, Quaternion.identity);
    //             }
    //             else
    //             {
    //                 Instantiate(roomDR, transform.position, Quaternion.identity);
    //             }
    //         }
    //         else if (_left)
    //         {
    //             Instantiate(roomDL, transform.position, Quaternion.identity);
    //         }
    //         else
    //         {
    //             Instantiate(roomD, transform.position, Quaternion.identity);
    //         }
    //         return;
    //     }
    //     if (_right)
    //     {
    //         if (_left)
    //         {
    //             Instantiate(roomRL, transform.position, Quaternion.identity);
    //         }
    //         else
    //         {
    //             Instantiate(roomR, transform.position, Quaternion.identity);
    //         }
    //     }
    //     else
    //     {
    //         Instantiate(roomL, transform.position, Quaternion.identity);
    //     }
    // }
    }
}


