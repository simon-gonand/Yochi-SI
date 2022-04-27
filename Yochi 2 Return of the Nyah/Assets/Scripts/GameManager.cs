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

    private int _level = 1;
    public int level { get { return _level; } set { _level = value; } }

    private void Awake()
    {
        if (_instance is not null)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    public void GetNextRoom()
    {
        int index = Random.Range(0, freeRooms.Count - 1);

        freeRooms[index].gameObject.SetActive(true);
        freeRooms[index].SetChunkPosition(usedRooms[usedRooms.Count - 1]);

        usedRooms.Add(freeRooms[index]);
        freeRooms.RemoveAt(0);

        if (usedRooms.Count > 3)
        {
            usedRooms[0].gameObject.SetActive(false);
            freeRooms.Add(usedRooms[0]);
            usedRooms.RemoveAt(0);
        }

        ++level;
    }
}
