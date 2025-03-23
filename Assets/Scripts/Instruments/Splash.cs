using UnityEngine;

public class Splash : Instrument
{
    [SerializeField] private GameObject explosionPrefab;

    protected override bool OnUse()
    {
        Instantiate(explosionPrefab, PlayerManager.Instance.transform);
        return true;
    }
}