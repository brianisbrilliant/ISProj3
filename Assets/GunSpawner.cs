using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public List<Gun> gunPrefabs;        // should have 4 guns, one for each elemental type.

    private bool onCooldown = false;

    void SpawnItem() {
        if(!onCooldown) {
            // picking a random number to choose the elementType.
            int elementType = Random.Range(0,4);

            Gun newGun = Instantiate(gunPrefabs[Random.Range(0, gunPrefabs.Count)], 
                                            transform.position, transform.rotation);

            newGun.elType = (Gun.elements)elementType;
            newGun.Randomize();
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            SpawnItem();
        }
    }

    IEnumerator Cooldown() {
        onCooldown = true;
        yield return new WaitForSeconds(2);
        onCooldown = false;
    }



}
