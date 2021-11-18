using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    GameControl ayuda;
    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void Again()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
