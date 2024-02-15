using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTMPviewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPlayerHP; //HP
    [SerializeField] private PlayerHP playerHP;
    [SerializeField] private TextMeshProUGUI textPlayerMP; //MP
    [SerializeField] private PlayerMP playerMP;
    [SerializeField] private TextMeshProUGUI textWave;//���̺�
    [SerializeField] private WaveSystem waveSystem;
    [SerializeField] private TextMeshProUGUI textEnemyCount; //�� ����
    [SerializeField] private EnemySpawner enemySapwner;
    void Update()
    {
        textPlayerHP.text = playerHP.CurrentHP + "/" + playerHP.MaxHP;
        textPlayerMP.text = playerMP.CurrentMana.ToString();
        textWave.text = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
        textEnemyCount.text = enemySapwner.CurrentEnemyCount + "/" + enemySapwner.MaxEnemyCount;
    }
}
