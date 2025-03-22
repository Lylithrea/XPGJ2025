using UnityEngine;

public class SelfdestructAfter : MonoBehaviour
{
    [SerializeField] private float seconds;

    private float counter;
    
    void Update()
    {
        if (counter > seconds)
        {
            Destroy(gameObject);
        }
        
        counter += Time.deltaTime;
    }
}
