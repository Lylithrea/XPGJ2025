using UnityEngine;

public class Gun : Instrument
{
    [SerializeField] private GameObject projectilePrefab;

    private Camera cam;
    
    private void Start()
    {
        cam = Camera.main;
    }

    protected override bool OnUse()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, float.PositiveInfinity, 1 << 9, QueryTriggerInteraction.Ignore))
        {
            return false;
        }

        var projectileDir = hit.point - PlayerManager.Instance.transform.position;
        projectileDir.y = 0;
        projectileDir.Normalize();

        var projectile = Instantiate(projectilePrefab, PlayerManager.Instance.transform.position + projectileDir,
            Quaternion.identity);
        projectile.GetComponent<Projectile>().direction = projectileDir.normalized;

        return true;
    }
}
