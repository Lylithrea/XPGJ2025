using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public FMODUnity.EventReference fmodEvent;
    private EventInstance eventInstance;

    public bool flute = false;
    public bool Organ_Base = false;

    private float bpm = 146f; // Default BPM, you will sync it later from FMOD
    private float beatInterval; // Interval between beats in seconds

    void Start() {
        // Create FMOD instance
        eventInstance = RuntimeManager.CreateInstance(fmodEvent);
        eventInstance.start();

        // Calculate beat interval based on BPM (60 seconds / BPM gives seconds per beat)
        beatInterval = 60f / bpm;
    }

    private void Update() {
        if (flute) {
            eventInstance.setParameterByName("Flute", 1);
        } else eventInstance.setParameterByName("Flute", 0);
        if (Organ_Base) {
            eventInstance.setParameterByName("Organ Base", 1);
        } else eventInstance.setParameterByName("Organ Base", 0);

        // Get current song position (in seconds)
        int currentTime;
        eventInstance.getTimelinePosition(out currentTime);

        //Debug.Log(currentTime);
        // Get current beat number
        int currentBeat = Mathf.FloorToInt(currentTime / beatInterval);

        //Debug.Log(currentBeat);
        // Check for button press synced with the beat
        if (Input.GetKeyDown(KeyCode.Space)) // Replace with your input
        {
            if (Mathf.Abs(currentTime - currentBeat * beatInterval) < beatInterval / 2f) // If within the current beat window
            {
                Debug.Log("Perfect timing on beat!");
                // Trigger some action in Unity
            }
            else {
                Debug.Log("Missed the beat!");
            }
        }


    }


    //void Start() {
    //    //loopingEvent = RuntimeManager.CreateInstance(soundEvent);
    //    //loopingEvent.start();
    //    //FMODUnity.RuntimeManager.PlayOneShot(soundEvent, transform.position);
    //}


    //[ContextMenu("Play Flute")]
    //public void StopSound() {

    //}

    //[ContextMenu("Stop Sound")]
    //public void StopSound() {
    //    loopingEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    //    loopingEvent.release();
    //}
}