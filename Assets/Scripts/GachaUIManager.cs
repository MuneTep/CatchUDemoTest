using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class GachaUIManager : MonoBehaviour
{
    public static GachaUIManager Instance;

    [Header("UI Elements")]
    public Button selectButton;
    public Button gachaButton;
    public Button giftButton;
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
    }


}
