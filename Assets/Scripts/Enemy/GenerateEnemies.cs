using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    private DungeonGenerator dungeon;
    public GameObject EnemyObject;
    public int maxEnemy;
    
    void Start()
    {
        dungeon = GameManager.Instance.DungeonGenerator;
    }

    public void DrawEnemy(Room room)
    {
        StartCoroutine(EnemyDraw(room));
    }

    public void SpawnAllEnemies(List<Room> rooms)
    {
        for (int i = 1; i < rooms.Count; i++)
        {
            DrawEnemy(rooms[i]);
        }
    }

    IEnumerator EnemyDraw(Room room)
    {
        for (int i = 0; i < maxEnemy; i++)
        {
            int xPos = room.position.x + room.size.x;
            int zPos = room.position.y + room.size.y;
            int randomXPos = Random.Range(room.position.x, xPos);
            int randomZPos = Random.Range(room.position.y, zPos);

            Instantiate(EnemyObject, new Vector3(randomXPos, 0, randomZPos), Quaternion.identity);
        }
        yield return null;
    }
}
