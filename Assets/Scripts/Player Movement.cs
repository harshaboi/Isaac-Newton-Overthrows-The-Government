using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int hello = 0;
    private int x = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hello++;
        if(hello == 100){
            hello = 0;
        }else if(hello == 50){
            hello = 51;
        }else if(x < 1){
            x++;
        }

    }
}
