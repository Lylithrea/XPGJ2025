using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class ULT : MonoBehaviour
{
    [SerializeField] private VisualEffect vfx;

    [SerializeField] private GameObject cracks;

    [SerializeField] private AudioSource ultsound;

    [SerializeField] private Volume volume;

    public float volumeDuration = 1.5f;

    public static ULT Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }



    public void StartUlt()
    {
        UltRoutine();
    }

    async Task UltRoutine()
    {
        vfx.enabled = true;
        //ultsound.Play();
        vfx.Play();
        await Task.Delay(7000);

        _ = BlendInVolume();

        await Task.Delay(1000);

        var crackInstance = Instantiate(cracks, transform.position, Quaternion.identity);
        await Task.Delay((int)(volumeDuration * 1000));

        volume.weight = 0;

        await Task.Delay(4000);
        while (crackInstance.transform.localScale.x > 0.01f)
        {
            crackInstance.transform.localScale *= 0.99f;
        }
        Destroy(crackInstance);
    }

    async Task BlendInVolume()
    {
        for (int i = 0; i < 100; i++)
        {
            volume.weight = Mathf.Lerp(0, 1, i / 100f);
            await Task.Delay(10);
        }
    }
}
