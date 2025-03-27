using UnityEngine;

public class Flintlockexplosion : MonoBehaviour
{
    private int timer = 0;
    [SerializeField] private int timeDelete; // in seconds
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timer >= (timeDelete * 50)){
            Destroy(gameObject);
        }
        timer++;

    }
}
