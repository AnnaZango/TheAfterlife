using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{    
    [SerializeField] Transform target = null;
    [SerializeField] float UpperLimit = 9.0f;
    [SerializeField] float BottomLimit = 0.0f;
    [SerializeField] float LeftLimit = -3.0f;
    [SerializeField] float RightLimit = 110.0f;

   
    void Update()
    {   
        this.gameObject.transform.position = new Vector3(
            Mathf.Clamp(target.position.x, LeftLimit, RightLimit),
            Mathf.Clamp(target.position.y, BottomLimit, UpperLimit),
            transform.position.z);
    }
}
