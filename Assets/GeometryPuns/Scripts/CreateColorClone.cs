using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateColorClone : MonoBehaviour {
    private BodyManager bm;
    // Use this for initialization
    void Start () {
        bm = GameObject.FindGameObjectWithTag("BodyManager").GetComponent<BodyManager>();
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(5);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // deactivate current player 
            // activate main player
            StartCoroutine(CoolDown());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // if yellow not collected, do nothing
            // if yellow collected & DNE, insantiate yellow
            // deactivate current, activate yellow
            StartCoroutine(CoolDown());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // if blue not collected, do nothing
            // if blue collected & DNE, insantiate blue
            // deactivate current, activate blue
            StartCoroutine(CoolDown());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // if magenta not collected, do nothing
            // if magenta collected & DNE, insantiate magenta
            // deactivate current, activate magenta
            StartCoroutine(CoolDown());
        }
    }
}
