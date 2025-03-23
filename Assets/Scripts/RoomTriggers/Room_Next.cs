using System;
using UnityEngine;

public class Room_Next : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        GameDungeonManager.Instance.NextDungeon();
    }
}
