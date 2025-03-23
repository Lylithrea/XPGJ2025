using FMODUnity;
using UnityEngine;

public class FmodEventTrigger : MonoBehaviour { 
    [SerializeField] private EventReference fmodEvent;

    private void Start() {
        if (fmodEvent.IsNull) {
            Debug.LogWarning("No Event in EventReference");
            return;
        }
    }

    public void PlayEvent() {
        if (fmodEvent.IsNull) {
            Debug.LogWarning("No Event in EventReference");
            return;
        }
        RuntimeManager.PlayOneShot(fmodEvent);
    }
}
