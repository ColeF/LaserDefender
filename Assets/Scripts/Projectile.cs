using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private int _damage = 10;

    public int GetDamage()
    {
        return _damage;
    }

	public void Hit()
    {
        Destroy(gameObject);
    }
}   
