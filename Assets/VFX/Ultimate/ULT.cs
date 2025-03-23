using System.Collections;
using System.Linq;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(UltRoutine());
        }
    }

    IEnumerator UltRoutine()
    {
        vfx.enabled = true;
        ultsound.Play();
        vfx.Play();
        yield return new WaitForSeconds(7f);

        for(int i = 0; i < 100; i++)
        {
            volume.weight = Mathf.Lerp(0, 1, i / 100f);
            yield return new WaitForSeconds(0.005f);
        }

        var crackInstance = Instantiate(cracks, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(volumeDuration);

        volume.weight = 0;


        yield return new WaitForSeconds(4);
        while(crackInstance.transform.localScale.x > 0.01f)
        {
            crackInstance.transform.localScale *= 0.99f;
        }
        Destroy(crackInstance); 
    }
}
