using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ChatController : MonoBehaviour
{
    public Image imageold;
    public Image imagetop;
    public RawImage rawImage; 
    public Texture imageToShow;
    public Image imagema;
    public Image imagemm;
    public Image imagegr;
    public Image imagebb;


    public Text ChatText;
    public Text ChatTitle;

    public List<KeyCode> skipButton; // 대화를 빠르게 넘길 수 있는 키
    public string writerText = "";

    bool isButtonClicked = false;

    void Start()
    {
        StartCoroutine(TextPractice());
        imageold.enabled = false;
        imagetop.enabled = false;
        rawImage.enabled = false;
        imagema.enabled = false;
        imagemm.enabled = false;
        imagegr.enabled = false;
        imagebb.enabled = true;
    }

    void Update()
    {
        foreach (var element in skipButton) // 버튼 검사
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            imagebb.enabled = false;
        }
    }


    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        ChatTitle.text = narrator;
        writerText = "";

        //텍스트 타이핑 효과
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return null;
        }

        //키를 다시 누를 떄 까지 무한정 대기
        while (true)
        {
            if (isButtonClicked)
            {
                isButtonClicked = false;
                break;
            }
            yield return null;
        }
    }

    IEnumerator TextPractice()
    {
        yield return StartCoroutine(NormalChat("Wizard’s Island", "마법사와 해적의 이야기"));
        imageold.enabled = true;
        yield return StartCoroutine(NormalChat("스토리", "오랜 세월 해적들 사이에 전해 내려오던 <color=#FFFF00>신의 유적</color>이라 불리는 <color=#FFFF00>지도</color>가 드디어 발견되었다."));
        imageold.enabled = false;
        imagetop.enabled = true;
        yield return StartCoroutine(NormalChat("스토리", "그 지도가 이끄는 곳은 바로 거대한 바다 위에 떠 있는 신비로운 탑이었다. "));
        imagetop.enabled = false;
        rawImage.enabled = true;
        rawImage.texture = imageToShow;
        yield return StartCoroutine(NormalChat("스토리", "탐욕에 눈이 먼 해적들은 망설임 없이 탑으로 향했지만,  "));
        rawImage.enabled = false;
        imagema.enabled = true;
        yield return StartCoroutine(NormalChat("스토리", "그들은 예상치 못한 존재와 마주하게 된다.  "));
        imagema.enabled = false;
        imagemm.enabled = true;
        yield return StartCoroutine(NormalChat("스토리", "바로 그 탑은 전설적인 <color=#FFFF00>대마법사</color>가 은둔하며 살고 있는 곳이었던 것이다.  "));
        imagemm.enabled = false;
        imagegr.enabled = true;
        yield return StartCoroutine(NormalChat("스토리", "해적들이 상륙하던 순간,"));
        imagegr.enabled = true;
        yield return StartCoroutine(NormalChat("스토리", "자신의 영역에 침입한 이들을 발견한 그는 그들을 소탕하기로 한다  "));
        SceneManager.LoadScene("MainScene");
    }
}