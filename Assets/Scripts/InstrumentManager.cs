using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class InstrumentManager : MonoBehaviour {
    public static InstrumentManager instance;

    [SerializeField]
    private FmodMusicPlayer fmodMusicPlayer;

    public bool flute = false;
    public bool Organ_Base = false;

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
    
    private void Update() {
        if (flute) {
            fmodMusicPlayer.ChangeParameter("Flute", 1);
        } else fmodMusicPlayer.ChangeParameter("Flute", 0);
        if (Organ_Base) {
            fmodMusicPlayer.ChangeParameter("Organ Base", 1);
        } else fmodMusicPlayer.ChangeParameter("Organ Base", 0);
    }
}