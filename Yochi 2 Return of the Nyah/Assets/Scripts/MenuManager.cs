using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject creditsScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Debug.Log("Leave");
    }
}
