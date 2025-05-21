using UnityEngine;

public class StuffWraping : MonoBehaviour
{
    public GameObject wrapperObject; // 포장지
    public GameObject stuffObject;    // 실제 물건

    private bool isUnwrapped = false;

    public void Unwrap()
    {
        if (isUnwrapped) return;

        isUnwrapped = true;

        // 포장지를 비활성화하고 물건을 활성화
        wrapperObject.SetActive(false);
        stuffObject.SetActive(true);

        // 파티클 연출
        Debug.Log("포장이 뜯어졌습니다!");
        
    }
}
