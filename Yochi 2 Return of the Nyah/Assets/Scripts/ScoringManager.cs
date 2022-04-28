using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringManager : MonoBehaviour
{
    public static ScoringManager instance;
    private YochiManager yochi;
    private float timer = 0.0f;
    private bool isTimer = true;

    private int roomsCleared = 0;
    private int dollsKilled = 0;
    private int kameosaKilled = 0;
    private int chochinObakeKilled = 0;
    private int darumaKilled = 0;
    [HideInInspector] public int score = 0;

    public TextMeshProUGUI roomsText;
    public TextMeshProUGUI dollsText;
    public TextMeshProUGUI kameosaText;
    public TextMeshProUGUI chochinText;
    public TextMeshProUGUI darumaText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        yochi = YochiManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer == true)
        {
            timer += Time.deltaTime;
        }
    }

    public void EnemyKilledScoring(EnemyParent.EnemyTypes type)
    {
        if (type == EnemyParent.EnemyTypes.RealDoll)
        {
            score += 1;
            dollsKilled++;
        }
        else if (type == EnemyParent.EnemyTypes.YokaiDoll)
        {
            score += 1;
            dollsKilled++;
        }
        else if (type == EnemyParent.EnemyTypes.Kameosa)
        {
            score += 2;
            kameosaKilled++;
        }
        else if (type == EnemyParent.EnemyTypes.Chochin)
        {
            score += 3;
            chochinObakeKilled++;
        }
        else if (type == EnemyParent.EnemyTypes.Daruma)
        {
            score += 5;
            darumaKilled++;
        }
    }

    public void RoomScoring(int level)
    {
        float levelValue = ((level / 10) + 1) * 10;

        score += ((int)levelValue);
        roomsCleared++;
    }

    public void PrintScore()
    {
        isTimer = false;
        int printedTime = Mathf.RoundToInt(timer);
        int hours = Mathf.FloorToInt(printedTime / 3600);
        int minutes = Mathf.FloorToInt(printedTime / 60);
        int seconds = printedTime % 60;
        roomsText.text = "Rooms Cleared: " + roomsCleared.ToString("D2");
        dollsText.text = "Dolls Killed: " + dollsKilled.ToString("D2");
        kameosaText.text = "Kameosas Killed: " + kameosaKilled.ToString("D2");
        chochinText.text = "Chochin Obakes Killed: " + chochinObakeKilled.ToString("D2");
        darumaText.text = "Darumas Killed: " + darumaKilled.ToString("D2");
        scoreText.text = "FINAL SCORE: " + score.ToString("D3");
        timerText.text = "Time: " + hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
}
