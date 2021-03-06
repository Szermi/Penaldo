using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTypeController : MonoBehaviour
{
    public Animator anim;
    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameState.isTeamOneTurn)
        {
        if (Input.GetKeyDown("1"))
            anim.SetInteger("saveType", 1);
        else if (Input.GetKeyDown("2"))
            anim.SetInteger("saveType", 2);
        else if (Input.GetKeyDown("3"))
            anim.SetInteger("saveType", 3);
        else if (Input.GetKeyDown("4"))
            anim.SetInteger("saveType", 4);
        else if (Input.GetKeyDown("5"))
            anim.SetInteger("saveType", 5);
        else if (Input.GetKeyDown("6"))
            anim.SetInteger("saveType", 6);
        else if (Input.GetKeyDown("7"))
            anim.SetInteger("saveType", 7);
        else if (Input.GetKeyDown("0"))
            anim.SetInteger("saveType", 0);
        }
    }
}
