using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitMode : MonoBehaviour
{
    public GameObject menu;
    public GameObject firstPage;
    public GameObject secondPage;
    public GameObject thirdPage;
    public GameObject scoreText;
    public GameObject roundText;
    public GameObject goalText;
    public GameObject game;
    public GameState gameState;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.visible = true;
            scoreText.GetComponent<Text>().text = "0 - 0";
            roundText.GetComponent<Text>().text = "(Runda 1)";
            goalText.GetComponent<Text>().text = "";
            clearGameState();
            game.SetActive(false);
            menu.SetActive(true);
            firstPage.SetActive(true);
            secondPage.SetActive(false);
            thirdPage.SetActive(false);

        }
    }

    private void clearGameState()
    {
        gameState.roundNumber = 1;
        gameState.isTeamOneTurn = true;
        gameState.teamOneScore = 0;
        gameState.teamTwoScore = 0;
        gameState.teamOneFlag = null;
        gameState.teamTwoFlag = null;
        gameState.shootCounter = false;
        gameState.endGame = false;
        ball.transform.position = new Vector3(3.2f, -1.4f, -7.5f);
        ball.GetComponent<Rigidbody>().Sleep();
    }
}
