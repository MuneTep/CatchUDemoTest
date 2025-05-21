using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StuffRevealUIController : MonoBehaviour
{
    [Header("UI Objects")]
    public GameObject Canvas;
    public GameObject darkBackground;
    public RectTransform stuffBoxRoot;
    public RectTransform boxLid;
    public GameObject auraEffect;
    public Image flashLight;
    public GameObject rewardUI;
    

    [Header("Animation Settings")]
    public float dropDistance = 1000f;
    public float dropDuration = 0.6f;
    public float shakeDuration = 0.4f;
    public float lidOpenAngle = -120f;
    public float lidOpenDuration = 0.5f;
    public float revealDelay = 0.8f;

    public void StartReveal()
    {
        Debug.Log("���� ����");
        Canvas.SetActive(true);
        darkBackground.SetActive(true);
        auraEffect.SetActive(false);
        rewardUI.SetActive(false);

        // �ʱ� ��ġ ����
        Vector2 originalPos = stuffBoxRoot.anchoredPosition;
        stuffBoxRoot.anchoredPosition += new Vector2(0, dropDistance);

        // ������ ����
        Sequence seq = DOTween.Sequence();

        // 1. ���� ����߸���
        seq.Append(stuffBoxRoot.DOAnchorPos(originalPos, dropDuration).SetEase(Ease.OutQuad));

        // 2. ����
        seq.Append(stuffBoxRoot.DOShakeAnchorPos(shakeDuration, strength: new Vector2(50, 50), vibrato: 30));

        // 3. �Ѳ� ����
        seq.Append(boxLid.DOLocalRotate(new Vector3(lidOpenAngle, 0f, 0f), lidOpenDuration).SetEase(Ease.OutBack));

        // 4. ��/��ƼŬ ȿ��
        seq.AppendCallback(() => auraEffect.SetActive(true));
        seq.AppendCallback(() => flashLight.transform.DORotate(new Vector3(0, 0, -360f), 2.5f, 
            RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1));


        // 5. ������ UI Ȱ��ȭ
        seq.AppendInterval(revealDelay);
        seq.AppendCallback(() => rewardUI.SetActive(true));
    }
}
