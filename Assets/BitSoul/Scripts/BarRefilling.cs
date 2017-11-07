using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarRefilling : MonoBehaviour {

    public float barDisplay; //current progress
    public float increaseRate = 0.05f;
    private float idle_full = 0;

    public Vector2 pos = new Vector2(20, 40);
    public Vector2 size = new Vector2(100, 20);
    public Texture2D emptyTex;
    public Texture2D fullTex;

    public float DOUBLE_SPEED = 0.1f;
    public float NORMAL_SPEED = 0.05f;
    public float DEPLETE_SPEED = -0.05f;

    void OnGUI()
    {
        if (idle_full < 5)
        {
            //draw the background:
            GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
            GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

            //draw the filled-in part:
            GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
            GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
            GUI.EndGroup();
            GUI.EndGroup();

        }
    }

    void Update()
    {
        //for this example, the bar display is linked to the current time,
        //however you would set this value based on your desired display
        //eg, the loading progress, the player's health, or whatever.

        barDisplay += Time.deltaTime * increaseRate;
        if (barDisplay > 1)
        {
            barDisplay = 1;
            idle_full += Time.deltaTime;
        }
        else
        {
            idle_full = 0f;
            if (barDisplay < 0)
            {
                barDisplay = 0;
            }
        }
    }
}