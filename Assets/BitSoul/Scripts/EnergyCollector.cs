using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCollector : MonoBehaviour {

    public int energy_packs;
    public Vector2 pos = new Vector2(20, 10);
    public Vector2 size = new Vector2(16, 22);
    public Texture2D batteryTex;
    private int batt_width = 0;
    private int batt_height = 0;
    float base_x;
    float base_y;

    private BarRefilling bar;

    private bool crouch;
    private bool jump;

    private UnityStandardAssets._2D.PlatformerCharacter2D m_Character;

    // Use this for initialization
    void Start () {
        energy_packs = 0;

        bar = GameObject.Find("EnergyBar").GetComponent<BarRefilling>();
    }


    void OnGUI()
    {
        GUILayout.BeginHorizontal();

        base_x = pos.x;
        base_y = pos.y;

        for (int i = 0; i < energy_packs; i++)
        {
            GUI.DrawTexture(new Rect(base_x, base_y, size.x, size.y), batteryTex);

            base_x += 2 * size.x;
        }
 
        GUILayout.EndHorizontal();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnergyPack")
        {
            energy_packs += 1;
            Destroy(other.gameObject); // Or whatever way you want to remove the coin.
            
            //other.GetComponent<Renderer>().enabled = false;
            //other.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void Update()
    {
        // Get energy meter values
        

        // Read the inputs.
        crouch = Input.GetKey(KeyCode.LeftControl);
        if (crouch) // crouching not allowed without energy
        {
            bar.increaseRate = bar.DOUBLE_SPEED;
        } else
        {
            bar.increaseRate = bar.NORMAL_SPEED;
        }

        jump = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetButtonDown("Jump");
        if (jump)
        {
            bar.barDisplay = Mathf.Max(bar.barDisplay-0.3f, 0f);
        }
    }
}
