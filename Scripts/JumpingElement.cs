using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingElement : MonoBehaviour
{
    [SerializeField] float jumpingForce = 2;
    [SerializeField] GameObject soundJumpPlayer = null;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, jumpingForce);
            soundJumpPlayer.GetComponent<AudioSource>().Play();
        }
    }
}
