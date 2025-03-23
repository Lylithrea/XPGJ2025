using System;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;

public class MusicTester : MonoBehaviour {
    public static MusicTester instance;

    [SerializeField] private FmodMusicPlayer fmodMusicPlayer;
    [SerializeField] private FmodEventTrigger endSequenceSound;
    [SerializeField] private float explosionTime = 8f;
    [SerializeField] private float delayTime = 4f;
    

    void Start() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    
    public void ChangeInstrument(InstrumentType type, bool active)
    {
        Debug.Log("Changing Instrument "  + type.ToString() + " to: " + active);
        switch (type)
        {
            case InstrumentType.flute:
                fmodMusicPlayer.ChangeParameter("Flute", active ? 1 : 0);
                break;
            case InstrumentType.cowbell:
                fmodMusicPlayer.ChangeParameter("CowBell", active ? 1 : 0);
                break;
            case InstrumentType.chordstab:
                fmodMusicPlayer.ChangeParameter("Stab", active ? 1 : 0);
                break;
            case InstrumentType.organbass:
                fmodMusicPlayer.ChangeParameter("OranBass", active ? 1 : 0);
                break;
            case InstrumentType.chordpad:
                fmodMusicPlayer.ChangeParameter("Pad", active ? 1 : 0);
                break;
            case InstrumentType.fx:
                fmodMusicPlayer.ChangeParameter("FX", active ? 1 : 0);
                break;
            case InstrumentType.percussion:
                fmodMusicPlayer.ChangeParameter("Percussion", active ? 1 : 0);
                break;
            case InstrumentType.vocalsample:
                fmodMusicPlayer.ChangeParameter("Vocal", active ? 1 : 0);
                break;
            default:
                Debug.LogWarning("Unknown instrument type");
                break;
        }
    }



    [ContextMenu("Play End Sequence")]
    public void EndSequence() {
        StartCoroutine(PlayEndSequence());
    }    
    IEnumerator PlayEndSequence() {
        fmodMusicPlayer.SetSoundVolume(0);
        endSequenceSound.PlayEvent();
        yield return new WaitForSeconds(explosionTime);
        GameDungeonManager.Instance.ResetInstruments();
        AttackManager.Instance.RemoveAllInstruments();
        ResetAllInstruments();
        yield return new WaitForSeconds(delayTime);
        fmodMusicPlayer.SetSoundVolume(1);
    }


    public void ResetAllInstruments()
    {
        ChangeInstrument(InstrumentType.flute, false);
        ChangeInstrument(InstrumentType.cowbell, false);
        ChangeInstrument(InstrumentType.chordstab, false);
        ChangeInstrument(InstrumentType.organbass, false);
        ChangeInstrument(InstrumentType.chordpad, false);
        ChangeInstrument(InstrumentType.fx, false);
        ChangeInstrument(InstrumentType.percussion, false);
        ChangeInstrument(InstrumentType.vocalsample, false);
    }
    
}