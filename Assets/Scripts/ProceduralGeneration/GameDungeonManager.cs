using NaughtyAttributes;
using UnityEngine;

public class GameDungeonManager : MonoBehaviour
{
    public PG_AdvGenerator dungeonGenerator;

    public int minRooms = 4;
    public int maxRooms = 16;


    public bool generatedDungeon = false;
    private int maxGenerationCount = 50;
    private int currentGenerationCount = 0;
    
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
    
}
