using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject firstPage;
    public GameObject secondPage;
    public GameObject thirdPage;
    public GameObject game;
    public GameObject menu;
    public GameState gameState;
    public Sprite[] images;
    private int teamOneCounter = 0;
    private int teamTwoCounter = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveToSecondPage()
    {
        firstPage.SetActive(false);
        secondPage.SetActive(true);
        thirdPage.SetActive(false);
    }

    public void moveToFirstPage()
    {
        secondPage.SetActive(false);
        firstPage.SetActive(true);
        thirdPage.SetActive(false);
    }

    public void moveToThirdPage()
    {
        secondPage.SetActive(false);
        firstPage.SetActive(false);
        thirdPage.SetActive(true);
    }

    public void endGame()
    {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void startGame()
    {
        menu.SetActive(false);
        game.SetActive(true);
        Image image = GameObject.Find("FlagTeam1").GetComponent<Image>();
        image.sprite = images[teamOneCounter];
        print("NEW FLAG IS: " + gameState.teamOneFlag);
        gameState.teamOneFlag = images[teamOneCounter];


        Image image2 = GameObject.Find("FlagTeam2").GetComponent<Image>();
        print("NEW FLAG IS: " + teamTwoCounter);
        image2.sprite = images[teamTwoCounter];
        gameState.teamTwoFlag = images[teamTwoCounter];

    }

    public void changeUpTeamOne()
    {
        Image image = GameObject.Find("TeamOneFlag").GetComponent<Image>();
        if(teamOneCounter+1 < images.Length) 
            teamOneCounter++;
        else
        {
            teamOneCounter = 0;
        }
        image.sprite = images[teamOneCounter];
    }

    public void changeDownTeamOne()
    {
        Image image = GameObject.Find("TeamOneFlag").GetComponent<Image>();
        if (teamOneCounter - 1 >= 0)
            teamOneCounter--;
        else
        {
            teamOneCounter = images.Length - 1;
        }
        image.sprite = images[teamOneCounter];
    }

    public void changeUpTeamTwo()
    {
        Image image = GameObject.Find("TeamTwoFlag").GetComponent<Image>();
        if (teamTwoCounter + 1 < images.Length)
            teamTwoCounter++;
        else
        {
            teamTwoCounter = 0;
        }
        image.sprite = images[teamTwoCounter];
    }

    public void changeDownTeamTwo()
    {
        Image image = GameObject.Find("TeamTwoFlag").GetComponent<Image>();
        if (teamTwoCounter - 1 >= 0)
            teamTwoCounter--;
        else
        {
            teamTwoCounter = images.Length - 1;
        }
        image.sprite = images[teamTwoCounter];
    }
}
