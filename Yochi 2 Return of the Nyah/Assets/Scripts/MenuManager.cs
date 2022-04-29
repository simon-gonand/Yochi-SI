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
    public Button creditsButton;
    public Button returnButton;

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
            returnButton.Select();
        }
        else if (isCredits == false)
        {
            mainMenuUI.SetActive(true);
            creditsScreen.SetActive(false);
            creditsButton.Select();
        }
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
