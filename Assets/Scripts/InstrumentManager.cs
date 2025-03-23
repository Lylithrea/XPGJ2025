using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class InstrumentManager : MonoBehaviour {
    public static InstrumentManager instance;

    [SerializeField]
    private FmodMusicPlayer fmodMusicPlayer;


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
    

}