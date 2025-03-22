using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public Vector3 direction = Vector3.forward;
    [SerializeField] public float speed = 25;
    [SerializeField] private GameObject explosionPrefab;

    private void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}