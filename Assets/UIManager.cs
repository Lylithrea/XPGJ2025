using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image explodeCooldownImage;    
    [SerializeField] private Image shootCooldownImage;
    [SerializeField] private Image splashCooldownImage;
    [SerializeField] private Image clickCooldownImage;

    public void SetExplodeCooldown(float value)
    {
        explodeCooldownImage.fillAmount = value;
    }
    
    public void SetShootCooldown(float value)
    {
        shootCooldownImage.fillAmount = value;
    }
    
    public void SetSplashCooldown(float value)
    {
        splashCooldownImage.fillAmount = value;
    }
    
    public void SetClickCooldown(float value)
    {
        clickCooldownImage.fillAmount = value;
    }
}
