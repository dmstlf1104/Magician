using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private GameObject GameClear;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private TMP_Text EarnGold;
    public int earnGold;

    private void Start()
    {
        if(gameObject == true)
        {
            GameClear.SetActive(true);
            GameOver.SetActive(false);
            earnGold = 1000;
            EarnGold.text = earnGold.ToString();
        }
        else if(gameObject == false)
        {
            GameClear.SetActive(false);
            GameOver.SetActive(true);
            earnGold = 100;
            EarnGold.text = earnGold.ToString();
        }
    }
    public void MainSceneLoad()
    {
        SceneManager.LoadScene("MainScene");
    }
}
