using UnityEngine;
using System;
using System.Collections;

public class BeatScaler : MonoBehaviour
{
    public Transform target;  // The object to scale
    public float minScale = 1f;
    public float maxScale = 1.5f;

    [SerializeField]
    private FmodMusicPlayer music;

    public event Action<float> musicKicked; // Event provides time to next beat

    private Coroutine scalingCoroutine;

    void OnEnable() {
        if (music != null)
            music.musicKicked.AddListener(StartScaling);
    }

    private void Update() {
    }

    private void StartScaling(float timeOfNextBeat) {
        if (scalingCoroutine != null) StopCoroutine(scalingCoroutine);
        scalingCoroutine = StartCoroutine(ScaleOverTime(timeOfNextBeat));
    }

    private IEnumerator ScaleOverTime(float timeOfNextBeat) {
        float currentTime = Time.time;
        float scaleTime = (timeOfNextBeat - currentTime) / 2;

        float elapsed = 0f;
        while (elapsed < scaleTime) {
            float t = elapsed / scaleTime;
            float scale = Mathf.Lerp(1f, 1.5f, t);
            transform.localScale = Vector3.one * scale;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < scaleTime) {
            float t = elapsed / scaleTime;
            float scale = Mathf.Lerp(1.5f, 1f, t);
            transform.localScale = Vector3.one * scale;
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
