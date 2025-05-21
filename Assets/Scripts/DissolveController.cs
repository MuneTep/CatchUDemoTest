using UnityEngine;
using System.Collections;
public class DissolveController : MonoBehaviour
{
    public Material dissolveMaterial; // Instance된 머터리얼
    public float dissolveDuration = 1f;

    private float dissolveAmount = 1f;
    private bool isDissolving = false;

    private void Start()
    {
        if (dissolveMaterial != null)
            dissolveMaterial.SetFloat("_DissolveAmount", 1f); // 시작은 안 보이는 상태
    }

    public void StartDissolve()
    {
        if (!isDissolving && dissolveMaterial != null)
        {
            StartCoroutine(DissolveIn());
        }
    }

    private IEnumerator DissolveIn()
    {
        isDissolving = true;
        float elapsed = 0f;

        while (elapsed < dissolveDuration)
        {
            dissolveAmount = Mathf.Lerp(1f, 0f, elapsed / dissolveDuration);
            dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);
            elapsed += Time.deltaTime;
            yield return null;
        }

        dissolveMaterial.SetFloat("_DissolveAmount", 0f);
        isDissolving = false;
    }
}
