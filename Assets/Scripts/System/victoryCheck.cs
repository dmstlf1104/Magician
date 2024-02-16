using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class victoryCheck : MonoBehaviour
{
    public WaveSystem waveSystem;
    public PlayerHP playerHP;

    private bool vic;
    private bool def;

    private void Awake()
    {
        waveSystem = GetComponent<WaveSystem>();
        playerHP = GetComponent<PlayerHP>();
    }

    private void Update()
    {
        waveSystem = GameObject.Find("EnemySpawner").GetComponent<WaveSystem>();
        playerHP = GameObject.Find("PlayerStats").GetComponent<PlayerHP>();
        if (waveSystem.Victory == true)
        {
            Win();
        }
        else if (playerHP.defeat == true)
        {
            Defeat();
        }
    }
    private void Win()
    {
        SceneManager.LoadScene("RewardScene");
    }

    private void Defeat()
    {
        SceneManager.LoadScene("RewardScene");
    }
}
