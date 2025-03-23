using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackManager : Singleton<AttackManager>
{
    public List<Instrument> instruments;
    
    private Camera cam;
    
    private readonly Func<bool>[] triggers =
    {
        () => Input.GetKeyDown(KeyCode.Q),
        () => Input.GetKeyDown(KeyCode.E),
        () => Input.GetKeyDown(KeyCode.R),
        () => Input.GetMouseButtonDown(1),
        () => Input.GetKeyDown(KeyCode.G)
    };

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggers[4]())
        {
            MusicTester.instance.EndSequence();
            return;
        }
        var ui = UIManager.Instance;
        for (var i = 0; i < instruments.Count; i++)
        {
            if (IsSlotAvailable(i) && triggers[i]())
            {
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

    public void RemoveAllInstruments()
    {
        foreach (var instrument in instruments)
        {
            UIManager.Instance.UnregisterInstrument();
        }
        instruments.Clear();
    }
    
}