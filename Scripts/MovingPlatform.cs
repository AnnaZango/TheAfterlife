using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float speedMovement = 1f;
    [SerializeField] bool isHorizontal = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHorizontal)
        {
            transform.Translate(speedMovement * Time.deltaTime, 0.0f, 0.0f);
        }
        else
        {
            transform.Translate(0.0f, speedMovement * Time.deltaTime, 0.0f);
        }        
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="PlatformLimit")
        {           
            speedMovement = speedMovement * -1;
        }        
    }

}
