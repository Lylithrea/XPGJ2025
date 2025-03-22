using FMOD.Studio;
using FMODUnity;
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

    private void Update() {
        if (flute) {
            fmodMusicPlayer.ChangeParameter("Flute", 1);
        } else fmodMusicPlayer.ChangeParameter("Flute", 0);
        if (Organ_Base) {
            fmodMusicPlayer.ChangeParameter("Organ Base", 1);
        } else fmodMusicPlayer.ChangeParameter("Organ Base", 0);
    }
}