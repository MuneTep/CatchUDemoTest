using System.Collections.Generic;
using UnityEngine;

public static class AuraLibrary
{
    private static Dictionary<Rank, GameObject> auraEffects;

    // �ʱ�ȭ�� �� ���� ����ǰ� ����
    private static bool isInitialized = false;

    public static void Initialize()
    {
        if (isInitialized) return;

        auraEffects = new Dictionary<Rank, GameObject>();

        // Resources ������ �ƿ�� �������� ����
        auraEffects[Rank.White] = Resources.Load<GameObject>("Aura/Aura_White");
        auraEffects[Rank.Green] = Resources.Load<GameObject>("Aura/Aura_Green");
        auraEffects[Rank.Blue] = Resources.Load<GameObject>("Aura/Aura_Blue");
        auraEffects[Rank.Purple] = Resources.Load<GameObject>("Aura/Aura_Purple");
        auraEffects[Rank.Magenta] = Resources.Load<GameObject>("Aura/Aura_Magenta");

        isInitialized = true;
    }

    public static GameObject GetEffect(Rank rank)
    {
        Initialize();

        if (auraEffects.TryGetValue(rank, out var prefab))
        {
            return prefab;
        }

        Debug.LogWarning($"Aura effect for rank {rank} not found.");
        return null;
    }
}
