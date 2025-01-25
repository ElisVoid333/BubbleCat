using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosionScript : MonoBehaviour
{
   public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
