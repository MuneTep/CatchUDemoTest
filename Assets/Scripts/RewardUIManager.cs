using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

// 뽑기 버튼 UI 제어

public class RewardUIManager:MonoBehaviour
{
    public static RewardUIManager Instance;

    [Header("UI Elements")]
    public GameObject panel;
    public TextMeshProUGUI stuffNameText;
    public TextMeshProUGUI priceText;
    public Button sellButton;
    public Button retryButton;
    public Button acceptButton;
    public Button backButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }


        Hide();
    }

    public void ShowReward(string stuffName, int price)
    {
        stuffNameText.text = stuffName;
        priceText.text = $"{price} 원";
        panel.SetActive(true);
    }

  


    public void Hide()
    {
        panel.SetActive(false);
    }

    public void SetupButtonCallbacks(System.Action onSell, System.Action onRetry, System.Action onAccept, System.Action onBack)
    {
        sellButton.onClick.RemoveAllListeners();
        retryButton.onClick.RemoveAllListeners();
        acceptButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();

        sellButton.onClick.AddListener(() => onSell?.Invoke());
        retryButton.onClick.AddListener(() => onRetry?.Invoke());
        acceptButton.onClick.AddListener(() => onAccept?.Invoke());
        backButton.onClick.AddListener(() => onBack?.Invoke());
    }
}
