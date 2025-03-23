using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private int health = 5;
    [SerializeField] private float walkSpeed = 10;
    [SerializeField] private float dashSpeed = 50;
    [SerializeField] private float dashCooldownTime = 1;
    [SerializeField] private float dashDurationTime = 0.25f;
    [SerializeField] private float turnSmoothing = 30;
    

    public Animator playerAnimator;

    public Vector3 faceDirection;

    private float dashTimer;
    private bool canDash = true;

    private CharacterController controller;

    public bool isDead = false;
    

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        UIManager.Instance.SetHealth(health);
    }

    private void Update()
    {
        if (isDead) return;
        if (!canDash)
        {
            if (dashTimer < dashDurationTime)
            {
                DashFrame();

                dashTimer += Time.deltaTime;
                return;
            }

            if (dashTimer >= dashDurationTime + dashCooldownTime)
            {
                canDash = true;
            }

            Walk();

            dashTimer += Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DashTrigger();
            return;
        }

        Walk();
    }

    private void DashTrigger()
    {
        DashFrame();

        canDash = false;
        dashTimer = 0;
    }

    private void DashFrame()
    {
        var move = faceDirection;
        controller.Move(move * (dashSpeed * Time.deltaTime));
    }

    private void Walk()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var move = new Vector3(horizontal, 0, vertical).normalized;
        playerAnimator.SetFloat("speed", move.magnitude);
        controller.Move(move * (Time.deltaTime * walkSpeed));

        if (move != Vector3.zero)
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,
                Quaternion.LookRotation(move), turnSmoothing * Time.deltaTime);
            faceDirection = move;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;
        if (!other.isTrigger)
        {
            return;
        }

        if (!other.gameObject.TryGetComponent<Pickupable>(out var pickupable))
        {
            return;
        }
        
        if (AttackManager.Instance.IsFull())
        {
            return;
        }
        
        var instrumentObj = Instantiate(pickupable.instrumentPrefab, transform);
        var instrument = instrumentObj.GetComponent<Instrument>();
        MusicTester.instance.ChangeInstrument(pickupable.type, true);
        Destroy(other.gameObject);
        if (instrument != null)
        {
            AttackManager.Instance.PickupInstrument(instrument);
        }

    }

    public void Damage(int amount)
    {
        if (isDead) return;
        health -= amount;
        
        UIManager.Instance.SetHealth(health);
        if (health <= 0)
        {
            Gameover();
        }
    }

    public void Gameover()
    {
        HighScoreManager.instance.SetScore();
        isDead = true;
    }

    public void ResetPosition()
    {
        controller.enabled = false;
        controller.transform.position = new Vector3(0, 1.25f, 0);
        controller.enabled = true;
    }
    
    
}