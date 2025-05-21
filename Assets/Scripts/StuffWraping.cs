using UnityEngine;

public class StuffWraping : MonoBehaviour
{
    public GameObject wrapperObject; // ������
    public GameObject stuffObject;    // ���� ����

    private bool isUnwrapped = false;

    public void Unwrap()
    {
        if (isUnwrapped) return;

        isUnwrapped = true;

        // �������� ��Ȱ��ȭ�ϰ� ������ Ȱ��ȭ
        wrapperObject.SetActive(false);
        stuffObject.SetActive(true);

        // ��ƼŬ ����
        Debug.Log("������ ��������ϴ�!");
        
    }
}
