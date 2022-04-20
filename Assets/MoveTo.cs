using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       // for access to the navmeshagent

public class MoveTo : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    [SerializeField]
    float stoppingDistance = 10f;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance > stoppingDistance) {
            agent.destination = target.position;
            agent.updateRotation = true;
        } else {
            // stop moving
            agent.destination = transform.position;
            // stop letting agent control rotation
            agent.updateRotation = false;

            // calculate the direction we want to look at.
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // rotate towards that direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 360);

            // shoot at the player.
            Fire();
        }
        
    }

    void Fire() {
        if(canFire) {
            // shoot
            Debug.Log("Pow!");
            // at the end of each fire, use a coroutine to wait.
            StartCoroutine(WaitToFire());
        }
    }

    bool canFire = true;
    
    IEnumerator WaitToFire() {
        canFire = false;
        yield return new WaitForSeconds(1);
        canFire = true;
    }
}
