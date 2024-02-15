using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void next()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void nextshop()
    {
        SceneManager.LoadScene("StoreScene");
    }
}
