using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIcon : MonoBehaviour
{
    public Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(texture, new Vector3(32.0f,32.0f,0.0f), CursorMode.ForceSoftware);
    }

}
