using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum SystemType { Mana = 0, Build}

public class SystemTextViewer : MonoBehaviour
{
    private TextMeshProUGUI textSystem;
    private TMPAlpha tmpAlpha;

    private void Awake()
    {
        textSystem = GetComponent<TextMeshProUGUI>();
        tmpAlpha = GetComponent<TMPAlpha>();
    }

    public void PrintText(SystemType type)
    {
        switch (type)
        {
            case SystemType.Mana:
                textSystem.text = "System: No Money";
                break;
            case SystemType.Build:
                textSystem.text = "System: No more Build";
                break;
        }
        tmpAlpha.FadeOut();
    }
}
