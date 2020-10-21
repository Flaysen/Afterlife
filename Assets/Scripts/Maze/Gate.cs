using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private GameObject _door;

    private Collider _collider;

    private RoomController _roomController;

    private void Awake()
    {
        _collider = GetComponent<Collider>();

        _collider.enabled = false;
    }

    private void Start()
    {
        _door = transform.GetChild(0).gameObject;

        _roomController.OnRoomEntered += CloseDoor;
        _roomController.OnRoomCleared += DisableDoors;
    }

    public void Initialize(RoomController roomController)
    {
        _roomController = roomController;
    }

    private void DisableDoors(RoomController roomController)
    {
        _collider.enabled = false;
        _door.gameObject.SetActive(false);
        _roomController.OnRoomEntered -= CloseDoor;
    }


    private void CloseDoor(RoomController roomController)
    {
        _collider.enabled = true;
        //_renderer.enabled = true;
    }
}
