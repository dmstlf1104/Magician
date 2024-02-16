using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private TowerSpawner towerSpawner;
    [SerializeField] private TowerDataViewer towerDataViewr;
    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    private Transform hitTransform = null; //마우스 클릭 임시저장

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        //마우스가 ui에 있을때
        if(EventSystem.current.IsPointerOverGameObject() == true)
        {
            return;
        }
        //마우스 왼쪽버튼
        if (Input.GetMouseButtonDown(0))
        {
            //카메라 위치에서 마우스 위치에 광선
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            //광선에 부딫히는 오브젝트를 찾아서 hit에 저장
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform = hit.transform;
                // 부딫힌 오브젝트 태그가 Tile 이면
                if (hit.transform.CompareTag("Tile"))
                {
                    //타워 생성
                    towerSpawner.SpawnTower(hit.transform);
                }
                else if (hit.transform.CompareTag("Tower"))
                {
                    towerDataViewr.OnPanel(hit.transform);
                }
            }
            
        }
        else if(Input.GetMouseButtonUp(0))
        {
            //마우스클릭시 오브젝트가 없거나 타워가 아니면
            if(hitTransform == null || hitTransform.CompareTag("Tower") == false)
            {
                //ui비활성화
                towerDataViewr.OffPanel();
            }

            hitTransform = null;
        }
    }
}
