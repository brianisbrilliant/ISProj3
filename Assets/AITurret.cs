using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurret : MonoBehaviour
{
    // [SerializeField]
    // private Transform target;

    [SerializeField]
    private float shotInterval = 1f, bulletSpeed = 50f;

    [SerializeField]
    private Rigidbody bulletprefab;
    [SerializeField]
    private Transform bulletSpawn;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire() {
        while(true) {
            yield return new WaitForSeconds(shotInterval);
            // all the shooting code goes here.
            Rigidbody copy = Instantiate(bulletprefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            copy.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
            // play a sound
            // optionally add reload mechanics
        }
    }
}
