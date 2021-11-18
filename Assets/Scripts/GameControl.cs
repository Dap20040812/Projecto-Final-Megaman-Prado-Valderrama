using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public Megaman memuero;
    public Win winscreen;
    public Lose losescreen;
    public Text gg;
    int uthemisery;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uthemisery = GameObject.FindGameObjectsWithTag("enemies").Length + GameObject.FindGameObjectsWithTag("Tower1").Length;
        //Input.GetKeyDown(KeyCode.P)
        if (GameObject.FindGameObjectsWithTag("enemies").Length == 0 && GameObject.FindGameObjectsWithTag("Tower1").Length == 0)
        {
            winscreen.Setup();
        }
        if (!memuero.vivo)
        {
            losescreen.Setup();
        }
        gg.text = "Enemies: "+uthemisery.ToString();
    }
}
