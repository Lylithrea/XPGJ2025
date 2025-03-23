using FMODUnity;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackManager : Singleton<AttackManager>
{
    public List<Instrument> instruments;

    [SerializeField] private FmodMusicPlayer gameMusic;
    [SerializeField] private float beatSensativity = 0.5f;
    [SerializeField] private EventReference failedBeatCheck;


    private Camera cam;
    
    private readonly Func<bool>[] triggers =
    {
        () => Input.GetKeyDown(KeyCode.Q),
        () => Input.GetKeyDown(KeyCode.E),
        () => Input.GetKeyDown(KeyCode.R),
        () => Input.GetMouseButtonDown(1)
    };

    void Start()
    {
        if (gameMusic == null) {
            Debug.LogWarning("No Game Music found");
        }
        if (failedBeatCheck.IsNull) {
            Debug.LogWarning("No Failed Beat Check found");
        }
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var ui = UIManager.Instance;
        for (var i = 0; i < instruments.Count; i++)
        {
            if (IsSlotAvailable(i)
                && triggers[i]())
            {
                if (gameMusic.OnBeat(beatSensativity)) {
                    RuntimeManager.PlayOneShot(failedBeatCheck);
                    return;
                }
                instruments[i].Use();
            }

            if (instruments.Count > i)
            {
                ui.SetCooldown(i, instruments[i].GetCooldownT());
            }
        }
    }
    
    private bool IsSlotAvailable(int index)
    {
        return instruments.Count > index
            && instruments[index].IsReady();
    }

    public bool IsFull()
    {
        return instruments.Count >= 4;
    }

    public void PickupInstrument(Instrument instrument)
    {
        instruments.Add(instrument);
        UIManager.Instance.RegisterInstrument();
    }
}