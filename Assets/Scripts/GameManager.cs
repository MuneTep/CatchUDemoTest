using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("���� ����")]
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
        // Ŭ�ο� �̵� ����
        Debug.Log("��ο� ������ ��ŸƮ");
        // 1. ī�޶� ������� ����
        cameraMove.StartReveal(() =>
        {
            countPanel.SetActive(false);
            gachaButtons.SetActive(false);
            // 2. ī�޶� ���� ������ Ŭ�ο� ���� ����
            clawGrabing.StartGrabSequence(() =>
            {
                // 3. Ŭ�ο찡 ���� �ö�� �� stuff�� ���� ��ġ�� �����ϸ� �˸� ����
                Debug.Log("Ŭ�ο찡 ���� ���� �Ϸ� ��� ��...");
                // ClawGrabing ���ο��� NotifyStuffPlacedOnSpot() ȣ���
            });
        });
    }

    public void ShowReward(string name, int price)
    {
        rewardUI.ShowReward(name, price);
    }

    public void NotifyStuffPlacedOnSpot(GameObject stuff)
    {
        Debug.Log("Stuff�� �ڸ��� ����.");

        StuffController sc = stuff.GetComponent<StuffController>();

        if (sc != null)
        {
            sc.StartOpenSequence(() =>
            {
                // 4. ���� ������ ���� UI �����ֱ�
                //ShowReward(sc.itemName, sc.price);
            });
        }
    }
}
