using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearState : MonoBehaviour
{
    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState.roundNumber = 1;
        gameState.isTeamOneTurn = true;
        gameState.teamOneScore = 0;
        gameState.teamTwoScore = 0;
        gameState.teamOneFlag = null;
        gameState.teamTwoFlag = null;
        gameState.shootCounter = false;
        gameState.endGame = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
