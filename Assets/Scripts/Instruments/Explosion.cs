using UnityEngine;

public class Explosion : Instrument
{
    [SerializeField] private GameObject explosionPrefab;

    private Camera cam;
    
    private void Start()
    {
        cam = Camera.main;
    }

    protected override bool OnUse()
    {
        if (!cam)
        {
            return false;
        }

        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, float.PositiveInfinity, 1 << 6, QueryTriggerInteraction.Ignore))
        {
            return false;
        }

        var explosion = Instantiate(explosionPrefab);
        explosion.transform.position = hit.point;

        return true;
    }
}