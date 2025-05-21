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
        Debug.Log("연출 시작");
        Canvas.SetActive(true);
        darkBackground.SetActive(true);
        auraEffect.SetActive(false);
        rewardUI.SetActive(false);

        // 초기 위치 설정
        Vector2 originalPos = stuffBoxRoot.anchoredPosition;
        stuffBoxRoot.anchoredPosition += new Vector2(0, dropDistance);

        // 시퀀스 실행
        Sequence seq = DOTween.Sequence();

        // 1. 상자 떨어뜨리기
        seq.Append(stuffBoxRoot.DOAnchorPos(originalPos, dropDuration).SetEase(Ease.OutQuad));

        // 2. 떨림
        seq.Append(stuffBoxRoot.DOShakeAnchorPos(shakeDuration, strength: new Vector2(50, 50), vibrato: 30));

        // 3. 뚜껑 열기
        seq.Append(boxLid.DOLocalRotate(new Vector3(lidOpenAngle, 0f, 0f), lidOpenDuration).SetEase(Ease.OutBack));

        // 4. 빛/파티클 효과
        seq.AppendCallback(() => auraEffect.SetActive(true));
        seq.AppendCallback(() => flashLight.transform.DORotate(new Vector3(0, 0, -360f), 2.5f, 
            RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1));


        // 5. 리워드 UI 활성화
        seq.AppendInterval(revealDelay);
        seq.AppendCallback(() => rewardUI.SetActive(true));
    }
}
