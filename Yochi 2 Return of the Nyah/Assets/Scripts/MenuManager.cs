using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject creditsScreen;
    public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.Select();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GetRoomsScenes");
    }

    public void SetCredits(bool isCredits)
    {
        if (isCredits == true)
        {
            mainMenuUI.SetActive(false);
            creditsScreen.SetActive(true);
        }
        else if (isCredits == false)
        {
            mainMenuUI.SetActive(true);
            creditsScreen.SetActive(false);
        }
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
