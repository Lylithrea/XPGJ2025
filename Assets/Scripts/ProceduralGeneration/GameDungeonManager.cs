using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameDungeonManager : MonoBehaviour
{
    public PG_AdvGenerator dungeonGenerator;

    public int minRooms = 4;
    public int maxRooms = 16;

    public int dungeonLevel = -1;
    public AnimationCurve InstrumentSpawnChance;
    public AnimationCurve EnemyAmount;
    public AnimationCurve EnemySpeed;

    public bool generatedDungeon = false;
    private int maxGenerationCount = 50;
    private int currentGenerationCount = 0;

    public GameObject LoadingScreen;

    public static GameDungeonManager Instance;
    public GameObject player;
    
    public List<GameObject> instruments = new List<GameObject>();
    private List<GameObject> availableInstruments = new List<GameObject>();

    public bool visualDemo = false;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        ResetInstruments();
        NextDungeon();
    }

    public void ResetInstruments()
    {
        availableInstruments.Clear();
        availableInstruments.AddRange(instruments);
    }

    public GameObject GetInstrument()
    {
        int instrumentIndex = Random.Range(0, availableInstruments.Count);
        GameObject instrument = availableInstruments[instrumentIndex];
        availableInstruments.RemoveAt(instrumentIndex);
        return instrument;
    }

    public float GetInstrumentSpawnChance()
    {
        return InstrumentSpawnChance.Evaluate(dungeonLevel);
    }

    public float GetEnemyAmount()
    {
        return EnemyAmount.Evaluate(dungeonLevel);
    }

    public float GetEnemySpeed()
    {
        return EnemySpeed.Evaluate(dungeonLevel);
    }
    

    [Button]
    public void GenerateDungeon()
    {
        //generate dungeon
        //dungeon returns a boolean if requirements are met yes or no
        //we regenerate dungeon until its true
        //then we start the game etc
        //we set global parameters every level you go deeper
        //parameters influence the enemies and difficulity
        generatedDungeon = false;
        currentGenerationCount = 0;
        
        while (generatedDungeon == false)
        {
            generatedDungeon = dungeonGenerator.GenerateFullDungeon(minRooms, maxRooms);
            currentGenerationCount++;
            if (currentGenerationCount >= maxGenerationCount)
            {
                Debug.LogError("It took more than " + maxGenerationCount + " generation times to generate a valid dungeon, something is wrong.");
                break;
            }
        }
        
        Debug.Log("<color=Green>[DungeonGenerator]</color> Took " + currentGenerationCount + " generation times to generate a valid dungeon.");

        
    }

    public void NextDungeon()
    {
        dungeonLevel++;
        //LoadingScreen.gameObject.SetActive(true);
        GenerateDungeon();
        //player.transform.position = new Vector3(0, 1.25f, 0);
        PlayerManager.Instance.ResetPosition();
        //LoadingScreen.gameObject.SetActive(false);
    }
    
}
