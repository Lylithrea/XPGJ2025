using System.Collections;
using UnityEngine;

public class Room_Instrument : MonoBehaviour
{

    public float offsetTimeMax = 1f;

    [Tooltip("Spawn chance between 0 and 1, 0.1 is 10%")]
    public float initialSpawnChance = 0.5f;

    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(offsetTimeMax);
        SpawnInstrument();
    }

    public void GenerateInstrument()
    {
        float newValue = initialSpawnChance * GameDungeonManager.Instance.GetInstrumentSpawnChance();
        float random = Random.Range(0, 1f);
        if (random <= newValue)
        {
            StartCoroutine(StartTimer());
        }
    }
    
    public void SpawnInstrument()
    {
        Debug.Log("Spawned instrument");
        GameObject instrument = Instantiate(GameDungeonManager.Instance.GetInstrument());
        instrument.transform.position = transform.position;
    }



}
