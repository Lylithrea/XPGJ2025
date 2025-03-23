using UnityEngine;

public class Instrument : MonoBehaviour
{
    [SerializeField] protected float cooldown = 1; 
    
    protected float timer;
    
    public virtual bool IsReady()
    {
        return timer <= 0;
    }
    
    public void Use()
    {
        if (!IsReady())
        {
            return;
        }

        if (OnUse())
        {
           timer = cooldown;
        }
    }
    
    protected virtual bool OnUse()
    {
        return false;
    }
    
    protected virtual void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    
    public virtual float GetCooldownT()
    {
        return (cooldown - timer) / cooldown;
    }
}