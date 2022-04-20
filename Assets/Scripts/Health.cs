using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 10;

    public Gun.elements elType = Gun.elements.Fire;

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet")) {
            Destroy(other.gameObject);      // delete the bullet
            
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            
            Debug.Log("My Elemental Type is " + elType);
            Debug.Log("Bullet elemental type is " + bullet.elType);

            
            if(bullet.elType == this.elType) {
                // half damage
                health -= bullet.damage / 2;
            } else if(((int)bullet.elType + 2) % 4 == (int)this.elType) {
                // double damage
                health -= bullet.damage * 2;
            }
            else {
                // regular damage
                health -= bullet.damage;
            }
            

            if(health <= 0) {
                // do something more interesting later
                Destroy(this.gameObject);
            }
        }
    }
}
