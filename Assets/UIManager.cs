using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image[] cooldownImages;
    [SerializeField] private RawImage[] instrumentImages;
    [SerializeField] private Image[] healthImages;
    
    [SerializeField] private Texture2D[] instrumentIcons;

    private int instrumentCount;
    
    public void RegisterInstrument(InstrumentType type)
    {
        var index = instrumentCount++;
        cooldownImages[index].gameObject.SetActive(true);
        var raw = instrumentImages[index];
        raw.gameObject.SetActive(true);
        raw.texture = instrumentIcons[(int) type];
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
