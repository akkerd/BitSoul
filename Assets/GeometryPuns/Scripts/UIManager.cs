using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    private Canvas canvas;
    const int white = 1;
    const int cyan = 3;
    const int yellow = 7;
    const int magenta = 5;
    private GameObject[] canvasElements;
    private Vector3 big = new Vector4(15f, 15f, 15f);
    private Vector3 small = new Vector4(10f, 10f, 10f);
 
    public GameObject whiteSprite;
    public  GameObject cyanSprite;
    private  GameObject cyanSpriteBlank;
    public  GameObject yellowSprite;
    private GameObject yellowSpriteBlank;
    public GameObject magentaSprite;
    private GameObject magentaSpriteBlank;
	// Use this for initialization
	void Start () {
        //canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //canvasElements = canvas.GetComponentsInChildren<GameObject>();
        //canvas.gameObject.
        /*whiteSprite = GameObject.Find("Canvas/UIWhite").GetComponent<GameObject>();
        cyanSprite = GameObject.Find("Canvas/UICyan").GetComponent<GameObject>();
        cyanSpriteBlank = GameObject.Find("Canvas/UICyanBlank").GetComponent<GameObject>();
        magentaSprite = GameObject.Find("Canvas/UIMagenta").GetComponent<GameObject>();
        magentaSpriteBlank = GameObject.Find("Canvas/UIMagentaBlank").GetComponent<GameObject>();
        yellowSprite = GameObject.Find("Canvas/UIYellow").GetComponent<GameObject>();
        yellowSpriteBlank = GameObject.Find("Canvas/UIYellowBlank").GetComponent<GameObject>();*/
        yellowSprite.GetComponent<SpriteRenderer>().color = Color.grey;
        magentaSprite.GetComponent<SpriteRenderer>().color = Color.grey;
        cyanSprite.GetComponent<SpriteRenderer>().color = Color.grey;

    }

    private void scaleUp(GameObject activeSprite)
    {
        activeSprite.transform.localScale = big;
    }

    private void scaleDown(GameObject activeSprite)
    {
        activeSprite.transform.localScale = small;
    }

    public void uiSetInactive(int colorCode)
    {
        switch (colorCode)
        {
            case cyan:
                scaleDown(cyanSprite);
                break;
            case yellow:
                scaleDown(yellowSprite);
                break;
            case magenta:
                scaleDown(magentaSprite);
                break;
            case white:
                scaleDown(whiteSprite);
                break;
            default:
                break;
        }
    }

    public void uiSetActive(int colorCode)
    {
        switch (colorCode)
        {
            case cyan:
                scaleUp(cyanSprite);
                break;
            case yellow:
                scaleUp(yellowSprite);
                break;
            case magenta:
                scaleUp(magentaSprite);
                break;
            case white:
                scaleUp(whiteSprite);
                break;
            default:
                break;
        }
    }
	
	public void uiCollectColor(int colorCode)
    {
        switch(colorCode)
        {
            case cyan:
                cyanSprite.GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            case yellow:
                yellowSprite.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case magenta:
                magentaSprite.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
            default:
                break;
        }
    }

    public void uiLoseColor(int colorCode)
    {
        switch (colorCode)
        {
            case cyan:
                cyanSprite.GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case yellow:
                yellowSprite.GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case magenta:
                magentaSprite.GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            default:
                break;
        }
    }
}
