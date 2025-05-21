using UnityEngine;

public class StuffSpawner : MonoBehaviour
{
    public GameObject[] stuffPrefabs;
    public Transform spawnPoint;

    public GameObject SpawnRandomWrappedStuff()
    {
        int index = Random.Range(0, stuffPrefabs.Length);
        GameObject selectedStuff = Instantiate(stuffPrefabs[index]);

        GameObject wrapper = Instantiate(Resources.Load<GameObject>("WrappedShell"));
        selectedStuff.transform.SetParent(wrapper.transform.Find("StuffSlot"));
        selectedStuff.transform.localPosition = Vector3.zero;
        selectedStuff.SetActive(false); // �ʱ⿡�� �� ���̰�

        wrapper.transform.position = spawnPoint.position;
        return wrapper;
    }
}
