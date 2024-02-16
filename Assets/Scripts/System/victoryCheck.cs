using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class victoryCheck : MonoBehaviour
{
    [SerializeField] public WaveSystem waveSystem;
    [SerializeField] public PlayerHP playerHP;

    private void Awake()
    {
        waveSystem = GetComponent<WaveSystem>();
        playerHP = GetComponent<PlayerHP>();
    }

    private void Update()
    {
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

    }

    private void Defeat()
    {
        SceneManager.LoadScene("StageScene");
    }
}
