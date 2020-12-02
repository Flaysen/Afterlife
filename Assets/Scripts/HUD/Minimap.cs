using System.Collections;
using System.Collections.Generic;
using Afterlife.Assets.WorldGeneration.Scripts;
using LevelGeneration;
using Maze;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    Dictionary<RoomController, GameObject> _roomsDictionary = new Dictionary<RoomController, GameObject>();
    [SerializeField] private GameObject _roomImageSlot;
    [SerializeField] private Color _roomColor;
    [SerializeField] private float _mapScaling;

    private MazeGenerator _mazeGenerator;
    private Vector3 _lastPosition;
    private GameObject _lastRoomSlot;

    private void Awake()
    {
        _mazeGenerator = FindObjectOfType<MazeGenerator>();
        _lastPosition = transform.localPosition;
    }

    public void RegisterRoom(RoomController roomController)
    {     
        Vector2 mapPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 roomPosition = new Vector2(roomController.transform.position.x, roomController.transform.position.z) * _mapScaling;

        GameObject image = Instantiate(_roomImageSlot, Vector3.zero, Quaternion.identity, transform);
        image.transform.localPosition = roomPosition;
        _roomsDictionary.Add(roomController, image);
        roomController.OnRoomEntered += SetRoomColors; 
    }

    public void ResetMinimap()
    {
        foreach(Transform slot in transform)
        {
            Destroy(slot.gameObject);
        }
    }

    private void SetRoomColors(RoomController roomController)
    {
        Vector2 newPosition = Vector2.zero;
        GameObject newRoomSlot = null;

        foreach(KeyValuePair<RoomController, GameObject> kvp in _roomsDictionary)
        {        
            if(kvp.Key == roomController)
            {
                newPosition = new Vector2(kvp.Key.transform.position.x, kvp.Key.transform.position.z) * _mapScaling;
                newRoomSlot = kvp.Value; 

                kvp.Value.transform.GetChild(0).GetComponent<Image>().color = _roomColor;
            }

            // if(kvp.Key.RoomType == RoomType.EXIT)
            // {
            //     kvp.Value.transform.GetChild(0).GetComponent<Image>().color = Color.blue;
            // }
        }
        if(_lastRoomSlot)
        {
            _lastRoomSlot.transform.GetChild(0).GetComponent<Image>().color = _roomColor * 2;
        }
 
        MoveMap(roomController, newPosition);

        _lastPosition = newPosition;
        _lastRoomSlot = newRoomSlot;
    }

    private void MoveMap(RoomController roomController, Vector3 newPosition)
    {
        foreach(KeyValuePair<RoomController, GameObject> kvp in _roomsDictionary)
        {          
            kvp.Value.transform.localPosition += _lastPosition - newPosition;                      
        }
    }
}
