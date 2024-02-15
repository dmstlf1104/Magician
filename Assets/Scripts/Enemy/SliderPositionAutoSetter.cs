using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.down * 20.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void SetUp(Transform target)
    {
        targetTransform = target;

        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        //���� �������� ���� ����
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        //������Ʈ�� ��ġ�� ���ŵ� ���Ŀ� Slider UI�� ���� ��ġ�� ���� �ϱ����� LateUpdate()���� ȣ��

        //������Ʈ�� ���� ��ǥ �������� ȭ�鿡�� ��ǥ���� ���ϱ�
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        //ȭ�鳻���� ��ǥ+distance ��ŭ ������ ��ġ�� slider�� ��ġ�� ����
        rectTransform.position = screenPosition + distance;
    }
}
