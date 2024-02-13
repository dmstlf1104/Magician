using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnGather : MonoBehaviour
{
    public GameObject[] CharImage;
    public GameObject[] DetailExPanel;
    
    bool detailEx;

    public void DetailExBtn(int DetailExNum)
    {
        for(int i = 0; i < CharImage.Length; i++)
        {
            if(i == DetailExNum)
            {
                if (!detailEx)
                {
                    CharImage[i].SetActive(false);
                    DetailExPanel[i].SetActive(true);
                    detailEx = true;
                }
                else if (detailEx)
                {
                    CharImage[i].SetActive(true);
                    DetailExPanel[i].SetActive(false);
                    detailEx = false;
                }
            }
        }
        
    }
}
