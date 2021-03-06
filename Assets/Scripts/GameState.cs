using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="GameState", menuName="States/GameState")]
public class GameState : ScriptableObject
{
    public int teamOneScore;
    public int teamTwoScore;
    public Sprite teamOneFlag;
    public Sprite teamTwoFlag;
    public int roundNumber = 1;
    public bool isTeamOneTurn = true;
    public bool shootCounter = false;
    public bool endGame = false;

}
