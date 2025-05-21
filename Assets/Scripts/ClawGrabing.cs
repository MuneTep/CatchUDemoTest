using UnityEngine;
using System.Collections;
using DG.Tweening;

using System.Collections;
using UnityEngine;

public class ClawGrabing : MonoBehaviour
{
    [Header("Ŭ�ο� �̵� ��ġ")]
    public Transform targetPosition;      // ���� ��ġ
    public Transform dropPosition;        // ���ڸ� ������ ��ġ
    public Transform clawGrabPoint;       // Ŭ�ο찡 Stuff�� ����� ��ġ

    [Header("�ӵ� ����")]
    public float moveSpeed = 2f;
    public float verticalSpeed = 1f;

    [Header("Claw �������")]
    public Transform claw;
    public GameObject line;

    private GameObject attachedStuff;

    public void StartGrabSequence(System.Action onComplete)
    {
        StartCoroutine(GrabSequenceRoutine(onComplete));
    }

    private IEnumerator GrabSequenceRoutine(System.Action onComplete)
    {
        // 1. �̵� �� targetPosition���� �̵�
        yield return MoveTo(targetPosition.position);

        // 2. ��������
        yield return LowerClaw();

        // 3. ���� ���
        TryAttachStuff();

        // 4. �ٽ� �ö����
        yield return RaiseClaw();

        // 5. dropPosition���� �̵�
        yield return MoveTo(dropPosition.position);

        // 6. ���� ����
        DetachStuff();

        // 7. GameManager���� �˸���
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
        float targetY = transform.position.y + 4.0f;  //  ��ġ ������ ���� 4��ŭ �߰�
        while (transform.position.y > targetY)
        {
            transform.position -= new Vector3(0, verticalSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }

    private IEnumerator RaiseClaw()
    {
        float targetY = transform.position.y + 1.0f;  // �ٽ� ���� ��ġ��
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
        // clawGrabPoint�� �������� ���� true
        if (!canAttach && Vector3.Distance(claw.position, clawGrabPoint.position) < 0.05f)
        {
            canAttach = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canAttach) return; // ���� ����Ʈ ���� ���� ����

        if (other.CompareTag("Stuff"))
        {
            attachedStuff = other.gameObject;
            attachedStuff.transform.SetParent(clawGrabPoint);
            attachedStuff.transform.localPosition = Vector3.zero;
            attachedStuff.GetComponent<Rigidbody>().isKinematic = true;

            canAttach = false; // �� ���� ���⵵�� ����
        }
    }
}
