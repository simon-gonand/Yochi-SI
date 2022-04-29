using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get 
        { 
            return _instance;
        }
    }

    public List<RoomChunk> freeRooms;
    public List<RoomChunk> usedRooms;

    public RoomChunk currentRoom;
    private ScoringManager scoringManager;

    private int _level = 0;
    public int level { get { return _level; } set { _level = value; } }

    public float enemyMultiplier;

    private void Awake()
    {
        if (_instance is not null)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        scoringManager = ScoringManager.instance;
        currentRoom.spawnManager.SpawnAllEnemies(1.0f);
    }

    public void GetNextRoom()
    {
        int index = Random.Range(0, freeRooms.Count - 1);

        scoringManager.RoomScoring(level);
        freeRooms[index].gameObject.SetActive(true);
        freeRooms[index].SetChunkPosition(usedRooms[usedRooms.Count - 1]);

        usedRooms.Add(freeRooms[index]);
        freeRooms.RemoveAt(index);

        if (usedRooms.Count > 3)
        {
            usedRooms[0].gameObject.SetActive(false);
            freeRooms.Add(usedRooms[0]);
            usedRooms.RemoveAt(0);
        }

        ++level;
    }
}
