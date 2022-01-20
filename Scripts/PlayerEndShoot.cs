using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndShoot : MonoBehaviour
{
    public void EventFinishShootAnimation()
    {
        GetComponent<Animator>().SetBool("ShootFinished", true);
        //this.gameObject.transform.root.GetComponent<PlayerController>().FinishShootAnimation();        
    }

    public void EventDeactivateShootFinished()
    {
        GetComponent<Animator>().SetBool("ShootFinished", false);
    }

    public void EventInstantiateProjectile()
    {
        if (this.gameObject.transform.parent.GetComponent<PlayerController>())
        {
            this.gameObject.transform.parent.GetComponent<PlayerController>().InstantiateProjectile();
        }        
    }
}
