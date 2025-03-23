using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [SerializeField] public GameObject instrumentPrefab;

    public InstrumentType type;


}

public enum InstrumentType
{
    kick,
    percussion,
    organbass,
    chordstab,
    vocalsample,
    chordpad,
    cowbell,
    flute
}
