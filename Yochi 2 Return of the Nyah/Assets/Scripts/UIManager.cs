using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public bool isYokai = true;
    public GameObject deathScreen;
    public GameObject realHealthBar;
    public GameObject yokaiHealthBar;
    public GameObject inGameUI;
    private YochiManager yochi;
    private ScoringManager scoringManager;

    // Start is called before the first frame update
    void Awake()
    {

    }

    private void Start()
    {
        yochi = YochiManager.instance;
        scoringManager = ScoringManager.instance;
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (yochi.isInYokaiWorld == true && isYokai == false)
        {
            realHealthBar.SetActive(false);
            yokaiHealthBar.SetActive(true);
            isYokai = true;
        }
        else if (yochi.isInYokaiWorld == false && isYokai == true)
        {
            realHealthBar.SetActive(true);
            yokaiHealthBar.SetActive(false);
            isYokai = false;
        }

        if (yochi.currentHealthPoint <= 0)
        {
            inGameUI.SetActive(false);
            deathScreen.SetActive(true);
            scoringManager.PrintScore();
        }
    }

    public void Restart()
    {
        yochi.currentHealthPoint = yochi.maxHealthPoint;
        inGameUI.SetActive(true);
        deathScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        yochi.currentHealthPoint = yochi.maxHealthPoint;
        inGameUI.SetActive(true);
        deathScreen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
