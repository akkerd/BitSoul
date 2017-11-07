using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class ChangeBody : MonoBehaviour {
    private BodyManager bm;
    private BodyHashes bh;
    bool cloneExists = false;
    int numClones = 0;
    // Use this for initialization
    void Start()
    {
        cloneExists = false;
        bm = GameObject.FindGameObjectWithTag("BodyManager").GetComponent<BodyManager>();
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(5);
    }

    public void HideClone()
    {
        numClones--;
        if(numClones == 0)
        {
            cloneExists = false;
        }

        GameObject currentPlayer = bm.GetCurrentBody();
        currentPlayer.SetActive(false);

        bm.SwitchToNext();
       

    }

    public void ViewClone()
    {

        cloneExists = true;
        numClones++;
        GameObject currentPlayer = bm.GetCurrentBody();
        

        bm.SwitchToNext();
        currentPlayer = bm.GetCurrentBody();
        currentPlayer.SetActive(true);
        
        
    }

    public void SwitchPlayer()
    {
       
        bm.SwitchToNext();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            if (!cloneExists)
            {
                ViewClone();
            }
            else
            {
                HideClone();
            }
           

            StartCoroutine(CoolDown());
        }

        if (Input.GetKeyDown(KeyCode.E) && cloneExists)
        {
            SwitchPlayer();
            StartCoroutine(CoolDown());
        }
    }
}
