using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatController : MonoBehaviour
{
    public Text ChatText;
    public Text ChatTitle;

    public List<KeyCode> skipButton; // ��ȭ�� ������ �ѱ� �� �ִ� Ű

    public string writerText = "";

    bool isButtonClicked = false;

    void Start()
    {
        StartCoroutine(TextPractice());
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
        yield return StartCoroutine(NormalChat("���丮", "���� ���� ������ ���̿� ���� �������� <color=#FFFF00>���� ����</color>�̶� �Ҹ��� <color=#FFFF00>����</color>�� ���� �߰ߵǾ���."));
        yield return StartCoroutine(NormalChat("���丮", "�� ������ �̲��� ���� �ٷ� �Ŵ��� �ٴ� ���� �� �ִ� �ź�ο� ž�̾���. "));
        yield return StartCoroutine(NormalChat("���丮", "Ž�忡 ���� �� �������� ������ ���� ž���� ��������,  "));
        yield return StartCoroutine(NormalChat("���丮", "�׵��� ����ġ ���� ����� �����ϰ� �ȴ�.  "));
        yield return StartCoroutine(NormalChat("���丮", "�ٷ� �� ž�� �������� <color=#FFFF00>�븶����</color>�� �����ϸ� ��� �ִ� ���̾��� ���̴�.  "));
        yield return StartCoroutine(NormalChat("���丮", "�������� ����ϴ� ����,"));
        yield return StartCoroutine(NormalChat("���丮", "�ڽ��� ������ ħ���� �̵��� �߰��� �״� �׵��� �����ϱ�� �Ѵ�  "));
    }
}


