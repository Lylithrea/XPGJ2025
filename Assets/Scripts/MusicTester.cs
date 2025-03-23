using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEngine;

public class MusicTester : MonoBehaviour {
    public static MusicTester instance;

    [SerializeField] private FmodMusicPlayer fmodMusicPlayer;
    [SerializeField] private FmodEventTrigger endSequenceSound;
    [SerializeField] private float gameContinuesAfter = 13f;

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