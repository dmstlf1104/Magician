using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private TowerSpawner towerSpawner;
    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        //마우스 왼쪽버튼
        if (Input.GetMouseButtonDown(0))
        {
            //카메라 위치에서 마우스 위치에 광선
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            //광선에 부딫히는 오브젝트를 찾아서 hit에 저장
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // 부딫힌 오브젝트 태그가 Tile 이면
                if (hit.transform.CompareTag("Tile"))
                {
                    //타워 생성
                    towerSpawner.SpawnTower(hit.transform);
                }
            }
            
        }
    }
}
