using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image[] cooldownImages;    
    [SerializeField] private Image[] healthImages;

    private int instrumentCount;
    
    public void RegisterInstrument()
    {
        cooldownImages[instrumentCount++].gameObject.SetActive(true);
    }
    
    public void UnregisterInstrument()
    {
        cooldownImages[--instrumentCount].gameObject.SetActive(false);
    }

    public void SetCooldown(int index, float value)
    {
        cooldownImages[index].fillAmount = value;
    }
    
    public void SetHealth(int value)
    {
        for (var i = 0; i < healthImages.Length; i++)
        {
            healthImages[i].enabled = i < value;
        }
    }
}
