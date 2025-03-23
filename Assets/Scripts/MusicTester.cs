using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;

public class MusicTester : MonoBehaviour {
    public static MusicTester instance;

    [SerializeField] private FmodMusicPlayer fmodMusicPlayer;
    [SerializeField] private FmodEventTrigger endSequenceSound;
    [SerializeField] private float gameContinuesAfter = 13f;
    

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
            default:
                Debug.LogWarning("Unknown instrument type");
                break;
        }
    }



    [ContextMenu("Play End Sequence")]
    private void EndSequence() {
        StartCoroutine(PlayEndSequence());
    }    
    IEnumerator PlayEndSequence() {
        fmodMusicPlayer.SetSoundVolume(0);
        endSequenceSound.PlayEvent();
        yield return new WaitForSeconds(gameContinuesAfter);
        fmodMusicPlayer.SetSoundVolume(1);
    }
}