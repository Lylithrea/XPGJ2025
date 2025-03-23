using UnityEngine;
using System;
using System.Collections;

public class BeatRotator : MonoBehaviour {
    public Transform target;  // The object to scale
    public float minScale = 1f;
    public float maxScale = 1.5f;

    [SerializeField]
    private FmodMusicPlayer music;

    public event Action<float> musicKicked; // Event provides time to next beat

    private Coroutine scalingCoroutine;
    public float rotation = 30f;


    void OnEnable() {
        if (music != null)
            music.musicKicked.AddListener(StartScaling);
    }

    private void Update() {
    }

    private void StartScaling(float timeOfNextBeat) {
        if (scalingCoroutine != null) StopCoroutine(scalingCoroutine);
        scalingCoroutine = StartCoroutine(RotateOverTime(timeOfNextBeat));
    }

    private IEnumerator RotateOverTime(float timeOfNextBeat) {
        float currentTime = Time.time;
        float rotationTime = (timeOfNextBeat - currentTime) / 2;

        float elapsed = 0f;
        while (elapsed < rotationTime) {
            float t = elapsed / rotationTime;
            float angle = Mathf.Lerp(0f, rotation, t); // Rotate from 0 to 30 degrees
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < rotationTime) {
            float t = elapsed / rotationTime;
            float angle = Mathf.Lerp(rotation, 0f, t); // Rotate back from 30 to 0 degrees
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }


    void OnDisable() {
        // Unsubscribe to prevent memory leaks
        if (music != null)
            music.musicKicked.RemoveListener(StartScaling); // Unsubscribe 
    }
}
