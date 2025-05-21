using UnityEngine;
using System.Collections;

public class StuffController : MonoBehaviour
{
    [Header("Parts")]
    public Transform boxTop;                   // �Ѳ� �κ�
    public ParticleSystem openEffect;          // ���� ���� �� ȿ��
    public GameObject auraPrefab;              // �ƿ�� ����Ʈ ������
    public Transform auraSpawnPoint;           // �ƿ�� ��ġ
    public DissolveController dissolve;        // Dissolve ȿ�� ��Ʈ�ѷ�
    public StuffRevealUIController uiController;

    private Transform originalParent;

    // ������ ����
    public string itemName = "Mystery Box";
    public int price = 100;
    public string rank = "�Ķ�";


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
        // 1. Dissolve ����
        //if (dissolveController != null)
        //    dissolveController.Play();
        uiController.StartReveal();

        // 2. �ణ�� ���� �� ��ƼŬ, ��, ī�޶� ��鸲 ��
        yield return new WaitForSeconds(5f);

        // 3. �ƿ�� �� ���� ������
        //Instantiate(auraPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        // 4. �Ϸ� �ݹ�
        onComplete?.Invoke();
    }
}