using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kick : MonoBehaviour
{

    public float bounceFactor = 0.9f; // Determines how the ball will be bouncing after landing. The value is [0..1]
    public float forceFactor = 10f;
    public float tMax = 5f; // Pressing time upper limit

    private float kickStart; // Keeps time, when you press button
    private float kickForce; // Keeps time interval between button press and release 
    private Vector3 prevVelocity; // Keeps rigidbody velocity, calculated in FixedUpdate()
    private Vector3 worldPosition;
    public Animator footballer;
    public Animator goalkeeper;
    public AudioClip shootWhistle;
    public GameState gameState;
    public GameObject upperLeftCorner;
    public GameObject bottomRightCorner;
    private bool startLoadingStrike = false;

    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            print("POSITION: " + worldPosition.x + " " + worldPosition.y + " " + worldPosition.z);
            kickStart = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
               kickForce = Time.time - kickStart;
            }
        }
    }*/


    private float force = 80f; // change the force when needed
    private float strikePower = 0.0f;
    private void Start()
    {
        
    }
    void Update()
    {
        var rand = new System.Random();
        if (gameState.isTeamOneTurn && !gameState.endGame)
        {

            Cursor.visible = true;
            Vector2 mousePosition = CursorControl.GetGlobalCursorPos();
            int randomY = rand.Next(20)-10;
            int randomX = rand.Next(20)-10;
            CursorControl.SetGlobalCursorPos(new Vector2(mousePosition.x + randomX, mousePosition.y + randomY));
            //print("Mouse position:" + mousePosition.x + ", " + mousePosition.y + "(" + Screen.width + "," + Screen.height + ")");

            if (Input.GetMouseButtonDown(0) && !gameState.shootCounter)
            {
                kickStart = Time.time;
                startLoadingStrike = true;
                //Vector3 positions = Input.mousePosition; 
                // While you are locking the cursor
            }
            if (startLoadingStrike)
            {
                Image strikeBar = GameObject.Find("SpriteBar").GetComponent<Image>();
                strikePower = (Time.time - kickStart);
                strikeBar.fillAmount = strikePower;
            }

            if (Input.GetMouseButtonUp(0) && !gameState.shootCounter)
            {
                gameState.shootCounter = true;
                startLoadingStrike = false;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Create a ray from the current mouse coordinates 
                float time = (Time.time - kickStart);
                shoot(ray, time);
            }
        }
        else if (!gameState.isTeamOneTurn && !gameState.endGame)
        {
            if (!gameState.shootCounter)
            {
                Cursor.visible = false;
                gameState.shootCounter = true;
                Vector3 vectorUpperLeft = Camera.main.WorldToScreenPoint(upperLeftCorner.transform.position);
                Vector3 vectorBottomRight = Camera.main.WorldToScreenPoint(bottomRightCorner.transform.position);
                //530, 270   830, 340

                //270, 320   540 220

                //x 570- -830
                //y 250 340
                float randomX = rand.Next((int) (vectorBottomRight.x - vectorUpperLeft.x)) + vectorUpperLeft.x;
                float randomY = rand.Next((int) (vectorUpperLeft.y - vectorBottomRight.y)) + vectorBottomRight.y;
                //print("Shoot at: " + randomX + " "  + randomY);
                float randTime = (float) (rand.NextDouble()) * 0.6f + 0.4f;
                CursorControl.SetGlobalCursorPos(new Vector2(randomX, randomY));
                Ray ray = Camera.main.ScreenPointToRay(CursorControl.GetGlobalCursorPos());
                // Create a ray from the current mouse coordinates 
                StartCoroutine(EnemyCoroutine(ray, randTime));
            }
        }
    }
    private void shoot(Ray ray, float time)
    {
        Rigidbody ball = GameObject.Find("Ball").GetComponent<Rigidbody>();
        RaycastHit hit; // if something tagged "Ground" is hit... 
        if (Physics.Raycast(ray, out hit))
        {
            print("Time: " + time);
            if (time > 1) time = 1;
            kickForce = time * force;
            Vector3 dir = hit.point - ball.position;
            if (time > 0.5)
                dir.y += dir.y * 8.0f * (time - 0.5f);
            print(dir);
            footballer.SetBool("Kick", true);
            AudioSource gameAudio = GetComponent<AudioSource>();
            gameAudio.clip = shootWhistle;
            gameAudio.Play();
            // calculated the direction... // and kick!
            StartCoroutine(ExampleCoroutine(ball, dir.normalized * kickForce, time));
        }
    }
    IEnumerator ExampleCoroutine(Rigidbody ball, Vector3 direction, float animSpeed)
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.7f);
        ball.AddForce(direction, ForceMode.Impulse);
        //AudioSource gameAudio = GameObject.Find("Game").GetComponent<AudioSource>();
        //gameAudio.Play();
        if (gameState.isTeamOneTurn)
        {
            chooseSave(animSpeed);
        }
    }

    private void chooseSave(float animSpeed)
    {
        var rand = new System.Random();
        int saveType = rand.Next(7)+1;
        goalkeeper.SetInteger("saveType", saveType);
        goalkeeper.speed = animSpeed/2 + 0.5f;
    }

    IEnumerator EnemyCoroutine(Ray ray, float time)
    {
        yield return new WaitForSeconds(2f);
        shoot(ray, time);
    }


}
/*
    void FixedUpdate()
    {

        if (kickForce != 0)
        {
            //3,28 0,5
            //3,42 - 3,14
            //0,5 - 0,575
            //worldPosition.x - 3.28f,
            //worldPosition.y - 0.5f,
            Rigidbody ball = GameObject.Find("Ball").GetComponent<Rigidbody>();
            ball.AddForce(new Vector3( 24.0f,
                                        28.0f,
                                           -100.0f),
                               ForceMode.Impulse);
            kickForce = 0;
        }

    }*/


/*   IMPROVED VERSION: The script above kicks the ball in the clicked point direction, but the force must be controlled somehow by the player to make the ball reach the desired point, and you have no control over the elevation angle. If you want the ball to fall precisely in the clicked point, you can use the version below, where the function KickForce calculates the desired force and direction:

           var ball: Transform; // drag the ball here

           function KickForce(dest: Vector3, angle: float): Vector3 {
               var dir = dest - ball.position; // get target direction var h = dir.y; // get height difference dir.y = 0; // retain only the horizontal direction var dist = dir.magnitude ; // get horizontal distance var a = angle Mathf.Deg2Rad; // convert angle to radians dir.y = dist Mathf.Tan(a); // set dir to the elevation angle dist += h / Mathf.Tan(a); // correct for small height differences // calculate the velocity magnitude var vel = Mathf.Sqrt(dist Physics.gravity.magnitude / Mathf.Sin(2 a)); return vel dir.normalized ball.rigidbody.mass; }

               // control the kick angle when needed: the greater the angle, the slower the kick var kickAngle: float = 15;

               function Update()
               {
                   if (Input.GetButtonDown("Fire1"))
                   { // Create a ray from the current mouse coordinates var ray: Ray = Camera.main.ScreenPointToRay(Input.mousePosition); var hit: RaycastHit; // if something tagged "Ground" is hit... if (Physics.Raycast(ray, hit) && hit.transform.tag == "Ground"){ var kForce = KickForce(hit.point, kickAngle); // calculate the force and kick! ball.rigidbody.AddForce(kForce, ForceMode.Impulse); } } } NOTE: The current ball velocity will be added to the kick velocity, thus the destination point may not be reached; if this is the case, replace the last Update() line with ball.rigidbody.velocity = kForce; and remove the ball.rigidbody.mass from the last KickForce() line.
                   }*/