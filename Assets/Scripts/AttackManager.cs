using System;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileEmitter;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Explosion();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Splash();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Explosion();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Suicide();
        }
    }

    private void Explosion()
    {
        if (!cam)
        {
            return;
        }

        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, float.PositiveInfinity, 1 << 6, QueryTriggerInteraction.Ignore))
        {
            return;
        }

        var explosion = Instantiate(explosionPrefab);
        explosion.transform.position = hit.point;
    }


    private void Suicide()
    {
        Splash();
    }

    private void Splash()
    {
        Instantiate(explosionPrefab, PlayerControl.Instance.transform);
    }

    private void Shoot()
    {

        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, float.PositiveInfinity, 1 << 9, QueryTriggerInteraction.Ignore))
        {
            return;
        }

        var projectileDir = hit.point - PlayerControl.Instance.transform.position;
        projectileDir.y = 0;
        projectileDir.Normalize();
        
        var projectile = Instantiate(projectilePrefab, PlayerControl.Instance.transform.position + projectileDir, Quaternion.identity);
        projectile.GetComponent<Projectile>().direction = projectileDir.normalized;
    }
}