using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RoomChunk : MonoBehaviour
{
    public Transform self;
    public Transform entryPoint;
    public Transform exitPoint;
    public Transform pathfindingCentre;
    public SpawnManager spawnManager;
    public OpenDoor door;
    public OpenDoor nextDoor;

    [SerializeField]
    private Collider2D stairsCollider;
    [SerializeField]
    private Collider2D noReturnCollider;
    public Collider2D nextNoReturnCollider;

    public List<MurYokai> destroyedGameObject;
    public List<PropsDestructible> destroyedProps;
    public List<Manekineko> manekinekos;

    public void SetChunkPosition(RoomChunk lastRoom)
    {
        Vector3 entryPointOffset = self.position - entryPoint.position;
        self.position = lastRoom.exitPoint.position + entryPointOffset;
        stairsCollider.enabled = true;
        noReturnCollider.enabled = true;
        lastRoom.nextNoReturnCollider = noReturnCollider;
        lastRoom.nextDoor = door;
        foreach (MurYokai wall in destroyedGameObject)
        {
            wall.gameObject.SetActive(true);
            wall.ResetHealth();
        }
        foreach(PropsDestructible props in destroyedProps)
        {
            props.Reset();
            props.gameObject.SetActive(true);
        }
        destroyedGameObject.Clear();
        destroyedProps.Clear();

        foreach(Manekineko manekineko in manekinekos)
        {
            manekineko.StartBehaviour();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(YochiManager.instance.gameObject))
        {
            GameManager.instance.GetNextRoom();
            stairsCollider.enabled = false;
            nextNoReturnCollider.enabled = true;
            nextDoor.Close();
            GameManager.instance.currentRoom = nextNoReturnCollider.GetComponent<RoomChunk>();
            GameManager.instance.currentRoom.enabled = true;
            ((GridGraph)AstarPath.active.graphs[0]).center = GameManager.instance.currentRoom.pathfindingCentre.position;
            AstarPath.active.Scan();
            GameManager.instance.currentRoom.spawnManager.SpawnAllEnemies(GameManager.instance.basePowerLevelValue + (GameManager.instance.level * GameManager.instance.enemyMultiplier));
        }
    }

    public void OnEnemyDied(EnemyParent enemyParent)
    {
        spawnManager.allEnemies.Remove(enemyParent);
        if (spawnManager.allEnemies.Count == 0)
        {
            nextNoReturnCollider.enabled = false;
            nextDoor.Open();
            foreach(Manekineko manekineko in manekinekos)
            {
                manekineko.StopBehaviour();
            }
        }
    }
}
