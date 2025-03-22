using FMOD.Studio;
using FMODUnity;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class FmodMusicPlayer : MonoBehaviour {
    #region FMOD stuff
    [StructLayout(LayoutKind.Sequential)]
    class TimelineInfo {
        public int currentMusicBeat = 0;
        public int timesignaturelower;
        public int timesignatureupper;
        public int bar;
        public int position;
        public float tempo;
        public FMOD.StringWrapper lastMarker = new ();
    }

    private TimelineInfo timelineInfo = new();
    private GCHandle timelineHandle;

    private EVENT_CALLBACK beatCallback = new(BeatEventCallback);
    [SerializeField] private EventReference fmodEvent;
    private EventInstance musicInstance;
    #endregion

    public UnityEvent musicKicked = new();
    private int lastBeat = -1;
    private float nextBeatTime = -1;

    private void Update() {
        if (lastBeat != timelineInfo.currentMusicBeat) {
            lastBeat = timelineInfo.currentMusicBeat;
            musicKicked.Invoke();
            float secondsPerBeat = 60f / timelineInfo.tempo;
            nextBeatTime = Time.time + secondsPerBeat;
        }
    }

    private void Start() {
        if (fmodEvent.IsNull) {
            Debug.LogWarning("No Event in EventReference");
            return;
        }
        musicInstance = RuntimeManager.CreateInstance(fmodEvent);
        
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(beatCallback, EVENT_CALLBACK_TYPE.TIMELINE_BEAT | EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicInstance.start();
    }

    public void ChangeParameter(string parameter, int newValue) => 
        musicInstance.setParameterByName(parameter, newValue);

    public bool OnBeat(float sensitivity) {
        float currentTime = Time.time;
        return currentTime >= (nextBeatTime - sensitivity) && currentTime <= (nextBeatTime + sensitivity);
    }

    private void OnDestroy() {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free();
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    private static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr) {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK) {
            Debug.LogError("Timeline Callback error: " + result);
            return result;
        }

        if (timelineInfoPtr != IntPtr.Zero) {
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type) {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT: {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.tempo = parameter.tempo;
                        timelineInfo.currentMusicBeat = parameter.beat;
                        timelineInfo.timesignaturelower = parameter.timesignaturelower;
                        timelineInfo.timesignatureupper = parameter.timesignatureupper;
                        timelineInfo.bar = parameter.bar;
                        timelineInfo.position = parameter.position;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER: {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }
}