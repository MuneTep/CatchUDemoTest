using UnityEngine;

[System.Serializable]
public class StuffData
{
    public string name;
    public int price;
    public Rank rank;
    public GameObject modelPrefab;
}

public enum Rank
{
    White, Green, Blue, Purple, Magenta
}
