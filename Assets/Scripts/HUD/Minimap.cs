using System.Collections;
using System.Collections.Generic;
using LevelGeneration;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    private MazeGenerator _mazeGenerator;

    [SerializeField] private Sprite _mapSprite;
    [SerializeField] private Image _image;

    private void Awake()
    {
        _mazeGenerator = FindObjectOfType<MazeGenerator>();

        _mazeGenerator.OnMazeGenerated += DrawMap;

        _image = GetComponent<Image>();

        //RoomController.OnRoomEntered += MoveMap;
    }

    private void DrawMap(List<Vector2> roomPositions)
    {
        Debug.Log("MAP DRAWED");

        _image.enabled = true;
        _image.sprite = _mapSprite;

        foreach(Vector2 position in roomPositions)
        {
            Vector2 mapPosition = new Vector2(transform.position.x, transform.position.y);
            Image imgage = Instantiate(_image, mapPosition + position * 28, Quaternion.identity);
            imgage.transform.parent = transform.parent; 
        }
        
    }

    private void MoveMap(RoomController roomController)
    {
        
    }
}
