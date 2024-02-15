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
        //적이 없어지면 같이 삭제
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        //오브젝트의 위치가 갱신된 이후에 Slider UI도 같이 위치를 설정 하기위해 LateUpdate()에서 호출

        //오브젝트의 월드 좌표 기준으로 화면에서 좌표값을 구하기
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        //화면내에서 좌표+distance 만큼 떨어진 위치를 slider의 위치로 설정
        rectTransform.position = screenPosition + distance;
    }
}
