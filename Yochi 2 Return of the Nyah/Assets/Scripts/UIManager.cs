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
    private YochiManager yochi;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
        yochi = YochiManager.instance;
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
            deathScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
