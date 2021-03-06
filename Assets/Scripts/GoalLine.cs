using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalLine : MonoBehaviour
{
    public GameObject goalInfo;
    public GameObject roundNumberText;
    public GameState gameState;
    public GameObject ball;
    public GameObject goalkeeperShirt;
    public GameObject goalkeeperShorts;
    public GameObject footballerShirt;
    public GameObject footballerShorts;
    public Material[] colors;
    private bool isGoal = false;
    private float counterTime = -1;

    private void Update()
    {
        if (gameState.shootCounter && !gameState.endGame)
        {
            if(counterTime == -1)
            {
                counterTime = 8.0f;
            }
            counterTime -= Time.deltaTime;
            if(counterTime <= 0)
            {

                goalInfo.GetComponent<Text>().text = "";

                if (!isGoal)
                {
                    if (!gameState.isTeamOneTurn)
                    {
                        setFirstKit();
                    }
                    else
                    {
                        setSecondKit();
                    }
                    checkWinner();
                }
                else
                {
                    if (gameState.isTeamOneTurn)
                        setFirstKit();
                    else
                        setSecondKit();
                }



                gameState.shootCounter = false;
                counterTime = -1;
                isGoal = false;
                print("Time is up");
                ball.transform.position = new Vector3(3.2f, -1.4f, -7.5f);
                ball.GetComponent<Rigidbody>().Sleep();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            Text textInfo = GameObject.Find("ScoreText").GetComponent<Text>();
            isGoal = true;
            if (gameState.isTeamOneTurn)
            {
                gameState.teamOneScore++;
            }
            else
            {
                gameState.teamTwoScore++;
            }
            textInfo.text = gameState.teamOneScore + " - " + gameState.teamTwoScore;
            goalInfo.GetComponent<Text>().text = "Goooool!";
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            checkWinner();
        }

    }


    public void checkWinner()
    {
        int matchResult = calculateResult();
        print("Result: " + matchResult.ToString());
        if (matchResult == 0)
        {
            if (!gameState.isTeamOneTurn)
            {
                gameState.isTeamOneTurn = true;
                gameState.roundNumber++;
                print("ZMIANA " + gameState.roundNumber);
                Text textRound = GameObject.Find("RoundNumberText").GetComponent<Text>();
                textRound.text = "(Runda " + gameState.roundNumber + ")";
            }
            else
            {
                gameState.isTeamOneTurn = false;
            }
        }
        else
        {
            gameState.endGame = true;
            isGoal = false;
            counterTime = -1;
            setFirstKit();
            goalInfo.GetComponent<Text>().text = "Koniec gry!";
            switch (matchResult)
            {
                case 1:
                    {
                        print("Pokaż zwycięstwo");
                        goalInfo.GetComponent<Text>().text = "Wygrałeś! [ESC]";
                        break;
                    }
                case 2:
                    {
                        print("Pokaż zwycięstwo");
                        goalInfo.GetComponent<Text>().text = "Przegrałeś! [ESC]";
                        break;
                    }
            }
        }
    }

    public int calculateResult()
    {
        //0 - no one
        //1 - team one
        //2 - team two
        if (gameState.roundNumber == 3)
        {
            if (gameState.isTeamOneTurn && (gameState.teamTwoScore - gameState.teamOneScore >= 3))
                return 2;
            if (!gameState.isTeamOneTurn && (gameState.teamOneScore - gameState.teamTwoScore >= 3))
                return 1;
            if (!gameState.isTeamOneTurn && (gameState.teamTwoScore - gameState.teamOneScore >= 3))
                return 2;
        }
        else if (gameState.roundNumber == 4)
        {
            if (gameState.isTeamOneTurn && (gameState.teamOneScore - gameState.teamTwoScore >= 3))
                return 1;
            if (gameState.isTeamOneTurn && (gameState.teamTwoScore - gameState.teamOneScore >= 2))
                return 2;
            if (!gameState.isTeamOneTurn && (gameState.teamOneScore - gameState.teamTwoScore >= 2))
                return 1;
            if (!gameState.isTeamOneTurn && (gameState.teamTwoScore - gameState.teamOneScore >= 2))
                return 2;
        }
        else if (gameState.roundNumber >= 5)
        {
            if (gameState.isTeamOneTurn && (gameState.teamOneScore - gameState.teamTwoScore >= 2))
                return 1;
            if (gameState.isTeamOneTurn && (gameState.teamTwoScore - gameState.teamOneScore >= 1))
                return 2;
            if (!gameState.isTeamOneTurn && (gameState.teamOneScore - gameState.teamTwoScore >= 1))
                return 1;
            if (!gameState.isTeamOneTurn && (gameState.teamTwoScore - gameState.teamOneScore >= 1))
                return 2;
        }
        return 0;

    }

    private void setFirstKit()
    {
        goalkeeperShirt.GetComponent<SkinnedMeshRenderer>().material = colors[1];
        goalkeeperShorts.GetComponent<SkinnedMeshRenderer>().material = colors[1];
        footballerShirt.GetComponent<SkinnedMeshRenderer>().material = colors[0];
        footballerShorts.GetComponent<SkinnedMeshRenderer>().material = colors[0];

    }
    private void setSecondKit()
    {
        print("Change 2");
        goalkeeperShirt.GetComponent<SkinnedMeshRenderer>().material = colors[0];
        goalkeeperShorts.GetComponent<SkinnedMeshRenderer>().material = colors[0];
        footballerShirt.GetComponent<SkinnedMeshRenderer>().material = colors[1];
        footballerShorts.GetComponent<SkinnedMeshRenderer>().material = colors[1];

    }
}


//    if (gameState.roundNumber>5 && gameState.isTeamOneTurn)
//        {
//            if (gameState.teamOneScore > gameState.teamTwoScore)
//            {
//                isTeamOneWinner = true;
//                return true;
//            }
//            else if (gameState.teamOneScore < gameState.teamTwoScore)
//{
//    isTeamOneWinner = false;
//    return true;
//}           
//        }
//        if (gameState.roundNumber == 4)
//{
//    if (gameState.teamOneScore == 3 && gameState.teamTwoScore == 0 && gameState.isTeamOneTurn)
//    {
//        isTeamOneWinner = true;
//        return true;
//    }
//    else if (gameState.teamOneScore == 0 && gameState.teamTwoScore == 3)
//    {
//        isTeamOneWinner = false;
//        return true;
//    }
//}
//else if (gameState.roundNumber == 5)
//{

//    if (gameState.teamOneScore - gameState.teamTwoScore >= 2 && gameState.isTeamOneTurn)
//    {
//        isTeamOneWinner = true;
//        return true;
//    }
//    else if (gameState.teamTwoScore - gameState.teamOneScore >= 2)
//    {
//        isTeamOneWinner = false;
//        return true;
//    }
//}
//else if (gameState.roundNumber == 6)
//{
//    if (gameState.teamOneScore - gameState.teamTwoScore >= 1 && gameState.isTeamOneTurn)
//    {
//        isTeamOneWinner = true;
//        return true;
//    }
//    else if (gameState.teamTwoScore - gameState.teamOneScore >= 1)
//    {
//        isTeamOneWinner = false;
//        return true;
//    }
//}
//return false;
//    }