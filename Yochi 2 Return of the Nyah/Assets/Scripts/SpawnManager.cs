using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum EnemyType { DollHuman, DollYokai, Kameosa, Chochin, Daruma};

    public int dollHumanNumber;
    public int dollYokaiNumber;
    public int kameosaNumber;
    public int chochinNumber;
    public int darumaNumber;

    public GameObject dollHumanPrefab;
    public GameObject dollYokaiPrefab;
    public GameObject kameosaPrefab;
    public GameObject chochinPrefab;
    public GameObject darumaPrefab;

    public List<SpawnPoint> allRoomSpawnPoints;

    private List<SpawnPoint> dollHumanSP;
    private List<SpawnPoint> dollYokaiSP;
    private List<SpawnPoint> kameosaSP;
    private List<SpawnPoint> chochinSP;
    private List<SpawnPoint> darumaSP;

    private List<GameObject> allEnemies;

    private void Start()
    {
        dollHumanSP = new List<SpawnPoint>();
        dollYokaiSP = new List<SpawnPoint>();
        kameosaSP = new List<SpawnPoint>();
        chochinSP = new List<SpawnPoint>();
        darumaSP = new List<SpawnPoint>();
        allEnemies = new List<GameObject>();

        for (int i = 0; i < allRoomSpawnPoints.Count; i++)
        {
            if(allRoomSpawnPoints[i].enemy == EnemyType.DollHuman)
            {
                dollHumanSP.Add(allRoomSpawnPoints[i]);
            }

            if (allRoomSpawnPoints[i].enemy == EnemyType.DollYokai)
            {
                dollYokaiSP.Add(allRoomSpawnPoints[i]);
            }

            if (allRoomSpawnPoints[i].enemy == EnemyType.Kameosa)
            {
                kameosaSP.Add(allRoomSpawnPoints[i]);
            }

            if (allRoomSpawnPoints[i].enemy == EnemyType.Chochin)
            {
                chochinSP.Add(allRoomSpawnPoints[i]);
            }

            if (allRoomSpawnPoints[i].enemy == EnemyType.Daruma)
            {
                darumaSP.Add(allRoomSpawnPoints[i]);
            }
        }

        SpawnAllEnemies(1f);
    }

    public void SpawnAllEnemies(float powerLevelMultiplier)
    {
        int currDollHumanNumber = Mathf.RoundToInt(dollHumanNumber * powerLevelMultiplier);
        int currDollYokaiNumber = Mathf.RoundToInt(dollYokaiNumber * powerLevelMultiplier);
        int currKameosaNumber = Mathf.RoundToInt(kameosaNumber * powerLevelMultiplier);
        int currChochinNumber = Mathf.RoundToInt(chochinNumber * powerLevelMultiplier);
        int currDarumaNumber = Mathf.RoundToInt(darumaNumber * powerLevelMultiplier);

        int randomSpawnPointIndex = 0;
        Vector2 randomPosInSpawner = Vector2.zero;
        float spawnerRange = 0;
        for (int i = 0; i < currDollHumanNumber; i++)
        {
            spawnerRange = dollHumanSP[randomSpawnPointIndex].spawnerRange;
            randomSpawnPointIndex = Random.Range(0, dollHumanSP.Count);
            allEnemies.Add(Instantiate(dollHumanPrefab, dollHumanSP[randomSpawnPointIndex].transform.position
                + new Vector3(Random.Range(-spawnerRange, spawnerRange), Random.Range(-spawnerRange, spawnerRange)), Quaternion.identity));
        }

        for (int i = 0; i < currDollYokaiNumber; i++)
        {
            spawnerRange = dollYokaiSP[randomSpawnPointIndex].spawnerRange;
            randomSpawnPointIndex = Random.Range(0, dollYokaiSP.Count);
            allEnemies.Add(Instantiate(dollYokaiPrefab, dollYokaiSP[randomSpawnPointIndex].transform.position
                + new Vector3(Random.Range(-spawnerRange, spawnerRange), Random.Range(-spawnerRange, spawnerRange)), Quaternion.identity));
        }

        for (int i = 0; i < currKameosaNumber; i++)
        {
            spawnerRange = kameosaSP[randomSpawnPointIndex].spawnerRange;
            randomSpawnPointIndex = Random.Range(0, kameosaSP.Count);
            allEnemies.Add(Instantiate(kameosaPrefab, kameosaSP[randomSpawnPointIndex].transform.position
                + new Vector3(Random.Range(-spawnerRange, spawnerRange), Random.Range(-spawnerRange, spawnerRange)), Quaternion.identity));
        }

        for (int i = 0; i < currChochinNumber; i++)
        {
            spawnerRange = chochinSP[randomSpawnPointIndex].spawnerRange;
            randomSpawnPointIndex = Random.Range(0, chochinSP.Count);
            allEnemies.Add(Instantiate(chochinPrefab, chochinSP[randomSpawnPointIndex].transform.position
                + new Vector3(Random.Range(-spawnerRange, spawnerRange), Random.Range(-spawnerRange, spawnerRange)), Quaternion.identity));
        }

        for (int i = 0; i < currDarumaNumber; i++)
        {
            spawnerRange = darumaSP[randomSpawnPointIndex].spawnerRange;
            randomSpawnPointIndex = Random.Range(0, darumaSP.Count);
            allEnemies.Add(Instantiate(darumaPrefab, darumaSP[randomSpawnPointIndex].transform.position
                + new Vector3(Random.Range(-spawnerRange, spawnerRange), Random.Range(-spawnerRange, spawnerRange)), Quaternion.identity));
        }
    }
}
