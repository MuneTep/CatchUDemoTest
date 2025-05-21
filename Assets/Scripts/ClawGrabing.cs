using UnityEngine;
using System.Collections;
using DG.Tweening;

using System.Collections;
using UnityEngine;

public class ClawGrabing : MonoBehaviour
{
    [Header("클로우 이동 위치")]
    public Transform targetPosition;      // 상자 위치
    public Transform dropPosition;        // 상자를 내려둘 위치
    public Transform clawGrabPoint;       // 클로우가 Stuff를 붙잡는 위치

    [Header("속도 조정")]
    public float moveSpeed = 2f;
    public float verticalSpeed = 1f;

    [Header("Claw 구성요소")]
    public Transform claw;
    public GameObject line;

    private GameObject attachedStuff;

    public void StartGrabSequence(System.Action onComplete)
    {
        StartCoroutine(GrabSequenceRoutine(onComplete));
    }

    private IEnumerator GrabSequenceRoutine(System.Action onComplete)
    {
        // 1. 이동 → targetPosition으로 이동
        yield return MoveTo(targetPosition.position);

        // 2. 내려가기
        yield return LowerClaw();

        // 3. 상자 잡기
        TryAttachStuff();

        // 4. 다시 올라오기
        yield return RaiseClaw();

        // 5. dropPosition으로 이동
        yield return MoveTo(dropPosition.position);

        // 6. 상자 놓기
        DetachStuff();

        // 7. GameManager에게 알리기
        GameManager.Instance.NotifyStuffPlacedOnSpot(attachedStuff);

        onComplete?.Invoke();
    }

    private IEnumerator MoveTo(Vector3 destination)
    {
        while (Vector3.Distance(transform.position, destination) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator LowerClaw()
    {
        float targetY = transform.position.y + 4.0f;  //  위치 보정을 위해 4만큼 추가
        while (transform.position.y > targetY)
        {
            transform.position -= new Vector3(0, verticalSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }

    private IEnumerator RaiseClaw()
    {
        float targetY = transform.position.y + 1.0f;  // 다시 원래 위치로
        while (transform.position.y < targetY)
        {
            transform.position += new Vector3(0, verticalSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }

    private void TryAttachStuff()
    {
        Collider[] hits = Physics.OverlapSphere(clawGrabPoint.position, 0.5f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Stuff"))
            {
                Debug.Log("Stuff Attached!");
                attachedStuff = hit.gameObject;
                attachedStuff.transform.SetParent(this.gameObject.transform);
                attachedStuff.transform.localPosition = Vector3.zero;
                break;
            }
        }
    }

    private void DetachStuff()
    {
        if (attachedStuff != null)
        {
            attachedStuff.transform.SetParent(null);
        }
    }


    private bool canAttach = false;

    private void Update()
    {
        // clawGrabPoint에 도달했을 때만 true
        if (!canAttach && Vector3.Distance(claw.position, clawGrabPoint.position) < 0.05f)
        {
            canAttach = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canAttach) return; // 지정 포인트 도달 전엔 무시

        if (other.CompareTag("Stuff"))
        {
            attachedStuff = other.gameObject;
            attachedStuff.transform.SetParent(clawGrabPoint);
            attachedStuff.transform.localPosition = Vector3.zero;
            attachedStuff.GetComponent<Rigidbody>().isKinematic = true;

            canAttach = false; // 한 번만 붙잡도록 제한
        }
    }
}
