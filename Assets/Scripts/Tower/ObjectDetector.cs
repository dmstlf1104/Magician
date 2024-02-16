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
    private Transform hitTransform = null; //���콺 Ŭ�� �ӽ�����

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        //���콺�� ui�� ������
        if(EventSystem.current.IsPointerOverGameObject() == true)
        {
            return;
        }
        //���콺 ���ʹ�ư
        if (Input.GetMouseButtonDown(0))
        {
            //ī�޶� ��ġ���� ���콺 ��ġ�� ����
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            //������ �΋H���� ������Ʈ�� ã�Ƽ� hit�� ����
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform = hit.transform;
                // �΋H�� ������Ʈ �±װ� Tile �̸�
                if (hit.transform.CompareTag("Tile"))
                {
                    //Ÿ�� ����
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
            //���콺Ŭ���� ������Ʈ�� ���ų� Ÿ���� �ƴϸ�
            if(hitTransform == null || hitTransform.CompareTag("Tower") == false)
            {
                //ui��Ȱ��ȭ
                towerDataViewr.OffPanel();
            }

            hitTransform = null;
        }
    }
}
