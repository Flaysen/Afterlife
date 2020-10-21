using System.Collections;
using System.Collections.Generic;
using LevelGeneration;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    Dictionary<RoomController, Image> _roomsDictionary = new Dictionary<RoomController, Image>();
    private MazeGenerator _mazeGenerator;
    private Vector3 _actualPosition;

    [SerializeField] private Sprite _mapSprite;
    [SerializeField] private Image _image;

    private void Awake()
    {
        _mazeGenerator = FindObjectOfType<MazeGenerator>();

        _actualPosition = transform.localPosition;
    }

    public void RegisterRoom(RoomController roomController)
    {
        
        Vector2 mapPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 roomPosition = new Vector2(roomController.transform.position.x, roomController.transform.position.z) * 2;

        Image image = Instantiate(_image, Vector3.zero, Quaternion.identity, transform);

        image.transform.localPosition = roomPosition;

        _roomsDictionary.Add(roomController, image);

        roomController.OnRoomEntered += ChangeRoomColor; 

        //roomController.OnRoomCleared += ClearColor;  
    }

    private void ChangeRoomColor(RoomController roomController)
    {
        Vector2 newPosition = Vector2.zero;

        foreach(KeyValuePair<RoomController, Image> kvp in _roomsDictionary)
        {        
            if(kvp.Key == roomController)
            {
                newPosition = new Vector2(kvp.Key.transform.position.x, kvp.Key.transform.position.z) * 2f;
                kvp.Value.color = Color.red;
            }
            else kvp.Value.color = Color.white;
        }

        Debug.Log("New:" + newPosition + " Actual:" + _actualPosition);

        MoveMap(roomController, newPosition);

        _actualPosition = newPosition;
    }

    private void ClearColor(RoomController roomController)
    {

        foreach(KeyValuePair<RoomController, Image> kvp in _roomsDictionary)
        {        
            if(kvp.Key == roomController)
            {
                kvp.Value.color = Color.green;
            }
        }
    }

    private void MoveMap(RoomController roomController, Vector3 newPosition)
    {
        foreach(KeyValuePair<RoomController, Image> kvp in _roomsDictionary)
        {          
            kvp.Value.transform.localPosition += _actualPosition - newPosition;                      
        }
    }
}
