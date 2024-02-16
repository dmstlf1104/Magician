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
    int earnGold;

    private void Start()
    {
        if(gameObject == true)
        {
            GameClear.SetActive(true);
            GameOver.SetActive(false);
            earnGold = 1000;
            EarnGold.text = earnGold.ToString();
            ShopManager.Instance.User.gold += earnGold;
        }
        else if(gameObject == false)
        {
            GameClear.SetActive(false);
            GameOver.SetActive(true);
            earnGold = 100;
            EarnGold.text = earnGold.ToString();
            ShopManager.Instance.User.gold += earnGold;
        }
    }
    public void MainSceneLoad()
    {
        SceneManager.LoadScene("MainScene");
    }
}
