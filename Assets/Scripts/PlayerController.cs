using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 10;
    [SerializeField] private float dashSpeed = 50;
    [SerializeField] private float dashCooldownTime = 1;
    [SerializeField] private float dashDurationTime = 0.25f;

    private float dashTimer;
    private bool canDash = true;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
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

        if (Input.GetKey(KeyCode.Space))
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
        var move = gameObject.transform.forward.normalized;
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
            gameObject.transform.rotation = Quaternion.LookRotation(move);
        }
    }
}