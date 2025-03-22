using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public List<Room_Door> roomDoors = new List<Room_Door>();
    public List<Room_Spawner> enemySpawners = new List<Room_Spawner>();

    public bool activeRoom = false;
    public bool completed = false;

    public void StartRoom()
    {
        activeRoom = true;
        
        //doors should not trigger anymore after initialize trigger
        foreach (Room_Door door in roomDoors)
        {
            door.gameObject.SetActive(false);
        }

        foreach (Room_Spawner spawner in enemySpawners)
        {
            StartCoroutine(spawner.SpawnEnemies());
        }
        
    }
    
    

}
