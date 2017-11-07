using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerDamage : MonoBehaviour {


    public int lives = 5;
    public int maxLives = 5;
    public float invulnerability = 1.0f;
    public float flashDuration = 0.1f;
    public Text loseText;
    public List<Image> heartImgs;


    private bool invulnerable = false;

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            this.lives -= damage;
            StartCoroutine(CharacterFlash());
            if (lives <= 0)
            {
                loseText.enabled = true;
            }
            ShowLives();
        }
    }

    public void ShowLives()
    {
        for (int i = 0; i < maxLives; ++i)
        {
            if (i >= lives)
            {
                heartImgs[i].enabled = false;
            }
        }
    }

    private IEnumerator CharacterFlash()
    {
        invulnerable = true;

        for (float i = 0.0f; i < invulnerability; i += flashDuration)
        {
            yield return new WaitForSeconds(flashDuration);
            this.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(flashDuration);
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
        invulnerable = false;
    }

}
