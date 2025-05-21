using UnityEngine;
using System.Collections;

public class StuffController : MonoBehaviour
{
    [Header("Parts")]
    public Transform boxTop;                   // 뚜껑 부분
    public ParticleSystem openEffect;          // 상자 열릴 때 효과
    public GameObject auraPrefab;              // 아우라 이펙트 프리팹
    public Transform auraSpawnPoint;           // 아우라 위치
    public DissolveController dissolve;        // Dissolve 효과 컨트롤러
    public StuffRevealUIController uiController;

    private Transform originalParent;

    // 리워드 정보
    public string itemName = "Mystery Box";
    public int price = 100;
    public string rank = "파랑";


    public void AttachToClaw(Transform claw)
    {
        originalParent = transform.parent;
        transform.SetParent(claw);
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    public void DetachFromClaw()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
    }

    public void PlayOpenSequence()
    {
        StartCoroutine("StartOpenSequence");
    }

    public void StartOpenSequence()
    {

    }

    public void StartOpenSequence(System.Action onComplete = null)
    {
        StartCoroutine(OpenSequenceRoutine(onComplete));
    }

    private IEnumerator OpenSequenceRoutine(System.Action onComplete)
    {
        // 1. Dissolve 연출
        //if (dissolveController != null)
        //    dissolveController.Play();
        uiController.StartReveal();

        // 2. 약간의 지연 후 파티클, 빛, 카메라 흔들림 등
        yield return new WaitForSeconds(5f);

        // 3. 아우라 및 연출 마무리
        //Instantiate(auraPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        // 4. 완료 콜백
        onComplete?.Invoke();
    }
}