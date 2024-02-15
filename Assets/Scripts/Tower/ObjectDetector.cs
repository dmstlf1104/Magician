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
        //���콺 ���ʹ�ư
        if (Input.GetMouseButtonDown(0))
        {
            //ī�޶� ��ġ���� ���콺 ��ġ�� ����
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            //������ �΋H���� ������Ʈ�� ã�Ƽ� hit�� ����
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // �΋H�� ������Ʈ �±װ� Tile �̸�
                if (hit.transform.CompareTag("Tile"))
                {
                    //Ÿ�� ����
                    towerSpawner.SpawnTower(hit.transform);
                }
            }
            
        }
    }
}
