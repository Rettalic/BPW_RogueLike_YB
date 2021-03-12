using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public DungeonGenerator DungeonGenerator { get; private set; }
    public GenerateEnemies GenerateEnemies { get; private set;  }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DungeonGenerator = FindObjectOfType<DungeonGenerator>();
        GenerateEnemies = FindObjectOfType<GenerateEnemies>();

        DungeonGenerator.GenerateDungeon(()=>GenerateEnemies.SpawnAllEnemies(DungeonGenerator.roomList));
    }
}
