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


    public Text ChatText;
    public Text ChatTitle;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű
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
    }

    void Update()
    {
        foreach (var element in skipButton) // ��ư �˻�
        {
            if (Input.GetKeyDown(element))
            {
                isButtonClicked = true;
            }
        }
    }


    IEnumerator NormalChat(string narrator, string narration)
    {
        int a = 0;
        ChatTitle.text = narrator;
        writerText = "";

        //�ؽ�Ʈ Ÿ���� ȿ��
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return null;
        }

        //Ű�� �ٽ� ���� �� ���� ������ ���
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
        yield return StartCoroutine(NormalChat("�����ϰڽ��ϴ�.", "�����̽��ٸ� �����ּ���"));
        imageold.enabled = true;
        yield return StartCoroutine(NormalChat("���丮", "���� ���� ������ ���̿� ���� �������� <color=#FFFF00>���� ����</color>�̶� �Ҹ��� <color=#FFFF00>����</color>�� ���� �߰ߵǾ���."));
        imageold.enabled = false;
        imagetop.enabled = true;
        yield return StartCoroutine(NormalChat("���丮", "�� ������ �̲��� ���� �ٷ� �Ŵ��� �ٴ� ���� �� �ִ� �ź�ο� ž�̾���. "));
        imagetop.enabled = false;
        rawImage.enabled = true;
        rawImage.texture = imageToShow;
        yield return StartCoroutine(NormalChat("���丮", "Ž�忡 ���� �� �������� ������ ���� ž���� ��������,  "));
        rawImage.enabled = false;
        imagema.enabled = true;
        yield return StartCoroutine(NormalChat("���丮", "�׵��� ����ġ ���� ����� �����ϰ� �ȴ�.  "));
        imagema.enabled = false;
        imagemm.enabled = true;
        yield return StartCoroutine(NormalChat("���丮", "�ٷ� �� ž�� �������� <color=#FFFF00>�븶����</color>�� �����ϸ� ��� �ִ� ���̾��� ���̴�.  "));
        imagemm.enabled = false;
        imagegr.enabled = true;
        yield return StartCoroutine(NormalChat("���丮", "�������� ����ϴ� ����,"));
        imagegr.enabled = true;
        yield return StartCoroutine(NormalChat("���丮", "�ڽ��� ������ ħ���� �̵��� �߰��� �״� �׵��� �����ϱ�� �Ѵ�  "));
        SceneManager.LoadScene("MainScene");

        
    }
}