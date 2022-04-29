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

    [HideInInspector]
    public List<EnemyParent> allEnemies;

    private void Awake()
    {
        dollHumanSP = new List<SpawnPoint>();
        dollYokaiSP = new List<SpawnPoint>();
        kameosaSP = new List<SpawnPoint>();
        chochinSP = new List<SpawnPoint>();
        darumaSP = new List<SpawnPoint>();
        allEnemies = new List<EnemyParent>();

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
    }

    public void SpawnAllEnemies(float powerLevelMultiplier)
    {
        Debug.Log("pl ! " + powerLevelMultiplier);
        int currDollHumanNumber = Mathf.Clamp(Mathf.RoundToInt(dollHumanNumber * powerLevelMultiplier), 0, 200);
        int currDollYokaiNumber = Mathf.Clamp(Mathf.RoundToInt(dollYokaiNumber * powerLevelMultiplier), 0, 200);
        int currKameosaNumber = Mathf.Clamp(Mathf.RoundToInt(kameosaNumber * powerLevelMultiplier), 0, 200);
        int currChochinNumber = Mathf.Clamp(Mathf.RoundToInt(chochinNumber * powerLevelMultiplier), 0, 200);
        int currDarumaNumber = Mathf.Clamp(Mathf.RoundToInt(darumaNumber * powerLevelMultiplier), 0, 200);

        int randomSpawnPointIndex = 0;
        Vector2 randomPosInSpawner = Vector2.zero;
        float spawnerRange = 0;

        if (currDollHumanNumber > 0)
            for (int i = 0; i < currDollHumanNumber; i++)
        {
            randomSpawnPointIndex = Random.Range(0, dollHumanSP.Count);
            spawnerRange = dollHumanSP[randomSpawnPointIndex].spawnerRange;
            allEnemies.Add(Instantiate(dollHumanPrefab, dollHumanSP[randomSpawnPointIndex].transform.position
                + new Vector3(Random.Range(-spawnerRange, spawnerRange), Random.Range(-spawnerRange, spawnerRange)), Quaternion.identity).transform.GetChild(0).GetComponent<EnemyParent>());
        }

        if (currDollYokaiNumber > 0)
            for (int i = 0; i < currDollYokaiNumber; i++)
        {
            randomSpawnPointIndex = Random.Range(0, dollYokaiSP.Count);
            spawnerRange = dollYokaiSP[randomSpawnPointIndex].spawnerRange;
            allEnemies.Add(Instantiate(dollYokaiPrefab, dollYokaiSP[randomSpawnPointIndex].transform.position
                + new Vector3(Random.Range(-spawnerRange, spawnerRange), Random.Range(-spawnerRange, spawnerRange)), Quaternion.identity).transform.GetChild(0).GetComponent<EnemyParent>());
        }

        if (currKameosaNumber > 0)
            for (int i = 0; i < currKameosaNumber; i++)
        {
            randomSpawnPointIndex = Random.Range(0, kameosaSP.Count);
            spawnerRange = kameosaSP[randomSpawnPointIndex].spawnerRange;
            allEnemies.Add(Instantiate(kameosaPrefab, kameosaSP[randomSpawnPointIndex].transform.position
                + new Vector3(Random.Range(-spawnerRange, spawnerRange), Random.Range(-spawnerRange, spawnerRange)), Quaternion.identity).transform.GetChild(0).GetComponent<EnemyParent>());
        }

        if(currChochinNumber > 0)
        for (int i = 0; i < currChochinNumber; i++)
        {
            randomSpawnPointIndex = Random.Range(0, chochinSP.Count);
            spawnerRange = chochinSP[randomSpawnPointIndex].spawnerRange;
            allEnemies.Add(Instantiate(chochinPrefab, chochinSP[randomSpawnPointIndex].transform.position
                + new Vector3(Random.Range(-spawnerRange, spawnerRange), Random.Range(-spawnerRange, spawnerRange)), Quaternion.identity).transform.GetChild(0).GetComponent<EnemyParent>());
        }

        if (currDarumaNumber > 0)
            for (int i = 0; i < currDarumaNumber; i++)
            {
                randomSpawnPointIndex = Random.Range(0, darumaSP.Count);
                spawnerRange = darumaSP[randomSpawnPointIndex].spawnerRange;
                allEnemies.Add(Instantiate(darumaPrefab, darumaSP[randomSpawnPointIndex].transform.position
                    + new Vector3(Random.Range(-spawnerRange, spawnerRange), Random.Range(-spawnerRange, spawnerRange)), Quaternion.identity).transform.GetChild(0).GetComponent<EnemyParent>());
            }
    }
}
