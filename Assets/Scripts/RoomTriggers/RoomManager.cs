using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public List<Room_Door> roomDoors = new List<Room_Door>();
    public List<Room_Spawner> enemySpawners = new List<Room_Spawner>();
    public List<Room_Instrument> instrumentPositions = new List<Room_Instrument>();

    public bool activeRoom = false;
    public bool completed = false;

    [Button]
    public void StartRoom()
    {
        if (activeRoom || completed) return;
        activeRoom = true;
        
        //doors should not trigger anymore after initialize trigger
        foreach (Room_Door door in roomDoors)
        {
            door.ToggleDoor(true);
        }

        foreach (Room_Spawner spawner in enemySpawners)
        {
            StartCoroutine(spawner.SpawnEnemies());
        }
        
        foreach (Room_Instrument instrument in instrumentPositions)
        {
            instrument.GenerateInstrument();
        }
        
    }

    public void CompleteRoom()
    {
        foreach (Room_Door door in roomDoors)
        {
            door.ToggleDoor(false);
        }
    }
    
    

}
