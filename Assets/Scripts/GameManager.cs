using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("연출 관련")]
    public ClawGrabing clawGrabing;
    public StuffReveralCameraMove cameraMove;
    public RewardUIManager rewardUI;
    public StuffRevealUIController StuffRevealUIController;
    public GameObject countPanel;
    public GameObject gachaButtons;

    private void Awake()
    {
        Instance = this;
    }

    public void StartDrawSequence()
    {
        // 클로우 이동 시작
        Debug.Log("드로우 시퀀스 스타트");
        // 1. 카메라 연출부터 시작
        cameraMove.StartReveal(() =>
        {
            countPanel.SetActive(false);
            gachaButtons.SetActive(false);
            // 2. 카메라 연출 끝나고 클로우 연출 시작
            clawGrabing.StartGrabSequence(() =>
            {
                // 3. 클로우가 집고 올라온 후 stuff를 놓는 위치에 도달하면 알림 받음
                Debug.Log("클로우가 상자 놓기 완료 대기 중...");
                // ClawGrabing 내부에서 NotifyStuffPlacedOnSpot() 호출됨
            });
        });
    }

    public void ShowReward(string name, int price)
    {
        rewardUI.ShowReward(name, price);
    }

    public void NotifyStuffPlacedOnSpot(GameObject stuff)
    {
        Debug.Log("Stuff가 자리에 도착.");

        StuffController sc = stuff.GetComponent<StuffController>();

        if (sc != null)
        {
            sc.StartOpenSequence(() =>
            {
                // 4. 연출 끝나면 보상 UI 보여주기
                //ShowReward(sc.itemName, sc.price);
            });
        }
    }
}
