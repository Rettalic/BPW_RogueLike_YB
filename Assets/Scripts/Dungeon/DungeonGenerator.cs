using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonGenerator : MonoBehaviour
{
    public enum Tile { Floor }

    [Header("Prefabs")]
    public GameObject FloorPrefab;
    public GameObject WallPrefab;
    public GameObject ShopStandPrefab;
    public GameObject VasePrefab;
    public Transform Player;
    public Transform Shop;
    public GameObject enemySpawner;
    private List<VaseGenerator> vaseGeneratorList = new List<VaseGenerator>();

    public NavMeshSurface surface;

    [Header("Dungeon Settings")]
    public int amountRooms;
    public int width = 100;
    public int height = 100;
    public int minRoomSize = 3;
    public int maxRoomSize = 7;

    private Dictionary<Vector2Int, Tile> dungeonDictionary = new Dictionary<Vector2Int, Tile>();
    public List<Room> roomList = new List<Room>();
    private List<GameObject> allSpawnedObjects = new List<GameObject>();

    void Awake()
    {
        //GenerateDungeon();
    }

    private void AllocateRooms()
    {
        for (int i = 0; i < amountRooms; i++)
        {
            Room room = new Room()
            {
                position = new Vector2Int(Random.Range(0, width), Random.Range(0, height)),
                size = new Vector2Int(Random.Range(minRoomSize, maxRoomSize), Random.Range(minRoomSize, maxRoomSize))
            };

            if (CheckIfRoomFitsInDungeon(room))
            {
                AddRoomToDungeon(room);
            }
            else
            {
                i--;
            }
        }
    }

    private void AddRoomToDungeon(Room room)
    {
        for (int xx = room.position.x; xx < room.position.x + room.size.x; xx++)
        {
            for (int yy = room.position.y; yy < room.position.y + room.size.y; yy++)
            {
                Vector2Int pos = new Vector2Int(xx, yy);
                dungeonDictionary.Add(pos, Tile.Floor);
            }
        }

        roomList.Add(room);

        //deze code is van Tom. (met zijn toestemming mag ik dit gebruiken)
        float roomFinalHeight = 0;
        float roomFinalWidth = 0;

        if (room.size.x % 2 == 0)
        {
            roomFinalWidth = -0.5f;
        }

        if (room.size.y % 2 == 0)
        {
            roomFinalHeight = -0.5f;
        }

        GameObject enemySpawnerPrefab = Instantiate(enemySpawner, new Vector3((room.position.x + room.size.x / 2) + roomFinalWidth, 0.3f, (room.position.y + room.size.y / 2) + roomFinalHeight), Quaternion.identity);

        enemySpawnerPrefab.GetComponent<BoxCollider>().size = new Vector3(room.size.x, 1, room.size.y);
        vaseGeneratorList.Add(enemySpawnerPrefab.GetComponent<VaseGenerator>());
    }

    private bool CheckIfRoomFitsInDungeon(Room room)
    {
        for (int xx = room.position.x; xx < room.position.x + room.size.x; xx++)
        {
            for (int yy = room.position.y; yy < room.position.y + room.size.y; yy++)
            {
                Vector2Int pos = new Vector2Int(xx, yy);
                if (dungeonDictionary.ContainsKey(pos))
                {
                    return false;
                }
            }
        }
        return true;
    }


    private void AllocateCorridors()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            //modulo
            //10 % 3 = 1
            //20 % 3 = 2
            // 0 1 2 3 4 5 [6 7]
            Room startRoom = roomList[i];
            Room otherRoom = roomList[(i + Random.Range(1, roomList.Count - 1)) % roomList.Count];

            // -1, 0, 1
            int dirX = Mathf.RoundToInt(Mathf.Sign(otherRoom.position.x - startRoom.position.x));
            for (int x = startRoom.position.x; x != otherRoom.position.x; x += dirX)
            {
                Vector2Int pos = new Vector2Int(x, startRoom.position.y);
                if (!dungeonDictionary.ContainsKey(pos))
                {
                    dungeonDictionary.Add(pos, Tile.Floor);
                }
            }

            int dirY = Mathf.RoundToInt(Mathf.Sign(otherRoom.position.y - startRoom.position.y));
            for (int y = startRoom.position.y; y != otherRoom.position.y; y += dirY)
            {
                Vector2Int pos = new Vector2Int(otherRoom.position.x, y);
                if (!dungeonDictionary.ContainsKey(pos))
                {
                    dungeonDictionary.Add(pos, Tile.Floor);
                }
            }
        }
    }

    private void BuildDungeon()
    {
        foreach (KeyValuePair<Vector2Int, Tile> kv in dungeonDictionary)
        {
            GameObject floor = Instantiate(FloorPrefab, new Vector3Int(kv.Key.x, 0, kv.Key.y), Quaternion.identity);
            allSpawnedObjects.Add(floor);

            SpawnWallsForTile(kv.Key);
        }
    }

    private void SpawnWallsForTile(Vector2Int position)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (Mathf.Abs(x) == Mathf.Abs(z)) { continue; }
                Vector2Int gridPos = position + new Vector2Int(x, z);
                if (!dungeonDictionary.ContainsKey(gridPos))
                {
                    //Spawn Wall
                    Vector3 direction = new Vector3(gridPos.x, 0, gridPos.y) - new Vector3(position.x, 0, position.y);
                    GameObject wall = Instantiate(WallPrefab, new Vector3(position.x, 0, position.y), Quaternion.LookRotation(direction));
                    allSpawnedObjects.Add(wall);
                }
            }
        }
    }

    private void DefineRooms()
    {
        Room spawnRoom = roomList[0];
        Player.transform.position = new Vector3(spawnRoom.position.x + spawnRoom.size.x / 2, 0.2f, spawnRoom.position.y + spawnRoom.size.y / 2);
        Shop.transform.position = new Vector3(spawnRoom.position.x + spawnRoom.size.x / 2 + 1, 0.2f, spawnRoom.position.y + spawnRoom.size.y / 2 + 1);
        Instantiate(ShopStandPrefab, new Vector3(spawnRoom.position.x + (spawnRoom.size.x / 2) + 1, 0.2f, spawnRoom.position.y + (spawnRoom.size.y / 2) + 1), Quaternion.Euler(0, 90, 0));
        Instantiate(VasePrefab, new Vector3(spawnRoom.position.x + (spawnRoom.size.x / 2) - 1, 0.2f, spawnRoom.position.y + (spawnRoom.size.y / 2) - 1), Quaternion.Euler(90, 0, 0));
    }


    public void GenerateDungeon(System.Action onDone = null)
    {
        AllocateRooms();
        AllocateCorridors();
        BuildDungeon();
        DefineRooms();
        surface.BuildNavMesh();

        foreach (VaseGenerator VaseGenerator in vaseGeneratorList)
        {
            VaseGenerator.SetFloor();
            VaseGenerator.SetVase();
        }
        onDone?.Invoke();
    }
}

[System.Serializable]
public class Room
{
    public Vector2Int position;
    public Vector2Int size;
}

