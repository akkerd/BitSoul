using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{

    private GameObject player;
    public static int currentLevel = 1;
    public string[] levelNames = new string[] { "Level_1", "Level_2", "Level_3" };
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject;

        // check if collider is player 
        if (player.tag == "Player")
        {
            if (currentLevel < levelNames.Length)
            {
                
                //Scene sceneToLoad = SceneManager.GetSceneByName(levelNames[currentLevel]);
                SceneManager.LoadScene(levelNames[currentLevel], LoadSceneMode.Single);
                //Application.LoadLevel(levelNames[currentLevel]);
                currentLevel++;

            }
        }

    }
}
