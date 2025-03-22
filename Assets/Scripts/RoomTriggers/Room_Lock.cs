using System;
using System.Collections.Generic;
using UnityEngine;

public class Room_Lock : MonoBehaviour
{
    
    public RoomManager roomManager;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entering new Room!");
        roomManager.StartRoom();
    }
}
