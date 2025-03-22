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

    public Vector3 faceDirection;

    private float dashTimer;
    private bool canDash = true;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        UIManager.Instance.SetHealth(health);
    }

    private void Update()
    {
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
        controller.Move(move * (Time.deltaTime * walkSpeed));

        if (move != Vector3.zero)
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,
                Quaternion.LookRotation(move), turnSmoothing * Time.deltaTime);
            faceDirection = move;
        }
    }
    
    public void Damage(int amount)
    {
        health -= amount;
        UIManager.Instance.SetHealth(health);
    }
}