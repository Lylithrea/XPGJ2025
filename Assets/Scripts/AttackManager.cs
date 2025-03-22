using System;
using UnityEngine;

public class AttackManager : Singleton<AttackManager>
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileEmitter;

    [SerializeField] private float explodeCooldown = 1;
    [SerializeField] private float shootCooldown = 1;
    [SerializeField] private float splashCooldown = 1;
    [SerializeField] private float clickCooldown = 1;
    [SerializeField] private float suicideCooldown = 1;

    private float explodeTimer;
    private float shootTimer;
    private float splashTimer;
    private float clickTimer;
    private float suicideTimer;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (explodeTimer <= 0
            && Input.GetKeyDown(KeyCode.Q))
        {
            Explosion();
            explodeTimer = explodeCooldown;
        }
        else
        {
            explodeTimer -= Time.deltaTime;
        }

        if (shootTimer <= 0
            && Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
            shootTimer = shootCooldown;
        }
        else
        {
            shootTimer -= Time.deltaTime;
        }

        if (splashTimer <= 0
            && Input.GetKeyDown(KeyCode.R))
        {
            Splash();
            splashTimer = splashCooldown;
        }
        else
        {
            splashTimer -= Time.deltaTime;
        }

        if (clickTimer <= 0
            && Input.GetMouseButtonDown(1))
        {
            Explosion();
            clickTimer = clickCooldown;
        }
        else
        {
            clickTimer -= Time.deltaTime;
        }

        if (suicideTimer <= 0
            && Input.GetKeyDown(KeyCode.G))
        {
            Suicide();
            suicideTimer = suicideCooldown;
        }
        else
        {
            suicideTimer -= Time.deltaTime;
        }

        var ui = UIManager.Instance;
        ui.SetExplodeCooldown((explodeCooldown - explodeTimer) / explodeCooldown);
        ui.SetShootCooldown((shootCooldown - shootTimer) / shootCooldown);
        ui.SetSplashCooldown((splashCooldown - splashTimer) / splashCooldown);
        ui.SetClickCooldown((clickCooldown - clickTimer) / clickCooldown);
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
        Instantiate(explosionPrefab, PlayerManager.Instance.transform);
    }

    private void Shoot()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, float.PositiveInfinity, 1 << 9, QueryTriggerInteraction.Ignore))
        {
            return;
        }

        var projectileDir = hit.point - PlayerManager.Instance.transform.position;
        projectileDir.y = 0;
        projectileDir.Normalize();

        var projectile = Instantiate(projectilePrefab, PlayerManager.Instance.transform.position + projectileDir,
            Quaternion.identity);
        projectile.GetComponent<Projectile>().direction = projectileDir.normalized;
    }
}