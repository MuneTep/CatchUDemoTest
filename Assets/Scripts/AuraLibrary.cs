using System.Collections.Generic;
using UnityEngine;

public static class AuraLibrary
{
    private static Dictionary<Rank, GameObject> auraEffects;

    // 초기화는 한 번만 실행되게 만듦
    private static bool isInitialized = false;

    public static void Initialize()
    {
        if (isInitialized) return;

        auraEffects = new Dictionary<Rank, GameObject>();

        // Resources 폴더에 아우라 프리팹이 있음
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
