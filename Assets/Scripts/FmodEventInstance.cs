using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using UnityEngine;

public class FmodEventInstance : MonoBehaviour {
    [SerializeField] private EventReference fmodEvent;
    private EventInstance musicInstance;

    private void Start() {
        if (fmodEvent.IsNull) {
            Debug.LogWarning("No Event in EventReference");
            return;
        }

        musicInstance = RuntimeManager.CreateInstance(fmodEvent);
    }

    public void StartInstance() {
        if (fmodEvent.IsNull) {
            Debug.LogWarning("No Event in EventReference");
            return;
        }
        EventInstance eventInstance = RuntimeManager.CreateInstance(fmodEvent);
        eventInstance.start();
    }
    public void StopInstance() {
        if (fmodEvent.IsNull) {
            Debug.LogWarning("No Event in EventReference");
            return;
        }

        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
    }

    private void OnDestroy() {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
    }
}
