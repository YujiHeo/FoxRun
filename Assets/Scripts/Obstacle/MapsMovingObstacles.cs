using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class MapsMovingObstacles : MonoBehaviour
{

    [HideInInspector] public List<GameObject> movingObjects;
    public int movingObjetctsCount = 10;
    public MapsObjectResource objectResource;
    public List<float> spawnX;
    public float spawnZ = 120f;
    public float resetZ = -10f;
    public float ItemspawnY = 1f;
    public float objectsSpawnY = 0;
    public float minSpawnInterval = 0.3f; // �ּ� ���� ���� (�ʹ� ������ �ʵ��� ����)
    public float speedFactor = 0.05f;

    public ResourceName resourceName;

    private float spawnInterval; // �⺻ ���� ����
    private GameObject newObject; // ��ֹ�������� ���� ���� ���ӿ�����Ʈ ����
    private List<GameObject> inactiveObjects = new List<GameObject>(); // Ȱ��ȭ�� ��ֹ� Ȯ�� �뵵
    private GameObject temptObject; // �� ���ӿ�����Ʈ


    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i < movingObjetctsCount; i++)
        {
             temptObject = objectResource.GetRandomObjectFromChildren(resourceName.ToString());
             temptObject.transform.SetParent(this.transform);
            //movingObjects[i].transform.SetParent(this.transform);         
        }

        StartCoroutine(SpawnObstacleRoutine());
    }


    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            SpawnOb();

            for (int i = 0; i < movingObjects.Count; i++)
            {
                if (movingObjects[i].transform.position.z <= resetZ) // ���� ��ġ Ȯ��
                {
                    if (resourceName == ResourceName.Item)
                    {
                        movingObjects[i].GetComponent<MeshRenderer>().enabled = false;
                        movingObjects.RemoveAt(i);

                    }
                    else
                    {
                        movingObjects[i].SetActive(false);
                        movingObjects.RemoveAt(i);
                    }
                }
            }

            float adjustedInterval = Mathf.Max(minSpawnInterval, spawnInterval - speedFactor); // �ּҰ� ����
            yield return new WaitForSeconds(adjustedInterval); // �������� ���� �ֱ� ����
        }
    }


    private void SpawnOb()
    {
        if (spawnX == null || spawnX.Count == 0)
        {
            Debug.LogError("spawnX �迭�� ��� �ֽ��ϴ�! �ν����Ϳ��� �� ������ �ʿ��մϴ�.");
            return;
        }

         RandomObstacle(objectsSpawnY, resourceName.ToString());
    }


    private void RandomObstacle(float _sapwY, string _typeName)
    {
        inactiveObjects.Clear();

        foreach (Transform ob in this.transform)
        {
            if (!ob.gameObject.activeSelf)
            {
                inactiveObjects.Add(ob.gameObject);
            }
        }


        if (inactiveObjects.Count == 0)
        {
            for (int i = 0; i < movingObjetctsCount; i++)
            {
                GameObject _newObjects = objectResource.GetRandomObjectFromChildren(_typeName);
                _newObjects.transform.SetParent(this.transform); // �θ� �����Ͽ� obstacle ������ �̵�

                inactiveObjects.Add(_newObjects);
            }
        }

        int indexPosition = Random.Range(0, spawnX.Count); // 3���� �� �� ���� �ε��� ����
        float _spawnX = spawnX[indexPosition]; // ���õ� ���� ��ġ ����

        Vector3 spawnPosition = new Vector3(_spawnX, _sapwY, spawnZ); // ���� ��ġ ����


        if (_typeName == ResourceName.Item.ToSafeString())
        {
            int number = Random.Range(0, 100);

            if(number > 95)
            {
                SelectItmeMethod(inactiveObjects, spawnPosition, "InvincibleItem");            

            }
            else if(number > 90)
            {
                SelectItmeMethod(inactiveObjects, spawnPosition, "FeverItem");
            }
            else
            {
                SelectItmeMethod(inactiveObjects, spawnPosition, "CommonItem");
            }


        }
        else
        {
            int index = Random.Range(0, inactiveObjects.Count); // ��ֹ� ����Ʈ���� �������� �ε��� ����
            GameObject newObstacle = inactiveObjects[index];
            newObstacle.transform.position = spawnPosition;
            newObstacle.gameObject.SetActive(true);
            movingObjects.Add(newObstacle);
        }

    }

    public void ReleaseObject(GameObject _object)
    {
        if (movingObjects.Contains(_object)) // ����Ʈ�� �ִ��� Ȯ��
        {
            _object.transform.position = new Vector3(0f,0f, spawnZ);
            movingObjects.Remove(_object); // ����Ʈ���� ����
            _object.SetActive(false); // ��Ȱ��ȭ ó��
        }
        else
        {
            Debug.LogWarning($"'{_object.name}'��(��) ����Ʈ�� �������� �ʴ� ������Ʈ�Դϴ�!");
        }
    }


    private void SelectItmeMethod(List<GameObject> _selectIteList,Vector3 _spawnPosition, string _itemName)
    {
        GameObject selectItem = null;
        //MeshRenderer meshRenderer = null;

        for (int i = 0; i < inactiveObjects.Count; i++)
        {
            if (inactiveObjects[i].name.Contains(_itemName))
            {
                selectItem = inactiveObjects[i];
                //meshRenderer = selectItem.GetComponent<MeshRenderer>();
                break;
            }
        }

        if(selectItem == null)
        {
            for (int i = 0; i < movingObjetctsCount; i++)
            {
                GameObject _newObjects = objectResource.GetRandomObjectFromChildren(resourceName.ToString());
                _newObjects.transform.SetParent(this.transform); // �θ� �����Ͽ� obstacle ������ �̵�

                inactiveObjects.Add(_newObjects);
            }

            for (int i = 0; i < inactiveObjects.Count; i++)
            {
                if (inactiveObjects[i].name.Contains(_itemName))
                {
                    selectItem = inactiveObjects[i];
                    //meshRenderer = selectItem.GetComponent<MeshRenderer>();
                    break;
                }
            }
        }

        selectItem.transform.position = _spawnPosition;
        selectItem.gameObject.SetActive(true);
        //meshRenderer.enabled = true;
        movingObjects.Add(selectItem);

    }

}
