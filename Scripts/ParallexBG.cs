using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBG : MonoBehaviour
{
    private float spriteLength;
    private float startPosSprite;
    public GameObject mainCamera;
    [SerializeField] float parallexMagnitude = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPosSprite = this.transform.position.x;
        spriteLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = (mainCamera.transform.position.x * parallexMagnitude);
        transform.position = new Vector3((startPosSprite + distanceMoved), transform.position.y, transform.position.z);        
    }
}
