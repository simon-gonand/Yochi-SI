using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChunk : MonoBehaviour
{
    public Transform self;
    public Transform entryPoint;
    public Transform exitPoint;

    [SerializeField]
    private Collider2D stairsCollider;
    [SerializeField]
    private Collider2D noReturnCollider;
    public Collider2D nextNoReturnCollider;

    public void SetChunkPosition(RoomChunk lastRoom)
    {
        Vector3 offset = self.position - entryPoint.position;
        self.position = lastRoom.exitPoint.position + offset;
        stairsCollider.enabled = true;
        noReturnCollider.enabled = false;
        lastRoom.nextNoReturnCollider = noReturnCollider;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(YochiManager.instance.gameObject))
        {
            GameManager.instance.GetNextRoom();
            stairsCollider.enabled = false;
            nextNoReturnCollider.enabled = true;
        }
    }
}