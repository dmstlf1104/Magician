using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Audioplay : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject optionPanel;

    public void Option()
    {
        startPanel.SetActive(false);
        optionPanel.SetActive(true);
    }
    public void ExitOption()
    {
        startPanel.SetActive(true);
        optionPanel.SetActive(false);
    }
}
