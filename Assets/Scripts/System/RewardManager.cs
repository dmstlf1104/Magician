using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private GameObject GameClear;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private TMP_Text EarnGold;

    WaveSystem waveSystem;
    PlayerHP playerHP;

    int earnGold;

    private void Awake()
    {
        waveSystem = FindObjectOfType<WaveSystem>();
        playerHP = FindObjectOfType<PlayerHP>();
    }
    private void Start()
    {
        if(waveSystem.Victory == true)
        {
            GameClear.SetActive(true);
            GameOver.SetActive(false);
            earnGold = 1000;
            EarnGold.text = earnGold.ToString();
            ShopManager.Instance.User.gold += earnGold;
        }
        else if(playerHP.defeat == true)
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
