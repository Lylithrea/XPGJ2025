using UnityEngine;

public class Room_Door : MonoBehaviour
{
    public GameObject door;
    public void ToggleDoor(bool state)
    {
        door.SetActive(state);
    }
}
