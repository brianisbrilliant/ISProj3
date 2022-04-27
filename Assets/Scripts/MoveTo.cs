using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       // for access to the navmeshagent

public class MoveTo : MonoBehaviour
{
    [SerializeField]
    public Transform target, bulSpawn;

    [SerializeField]
    float stoppingDistance = 10f;

    [SerializeField]
    Rigidbody bulletPrefab;

    public bool hasToken = false;

    private NavMeshAgent agent;
    TokenSystem tok;

    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(target != null) {
            tok = target.gameObject.GetComponent<TokenSystem>();
        }
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
        if(canFire){

            if(hasToken) {
                // shoot
                Debug.Log("Pow!");
                Rigidbody bullet = Instantiate(bulletPrefab, bulSpawn.position, bulSpawn.rotation);
                bullet.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse);
                Bullet bulletStuff = bullet.GetComponent<Bullet>();
                // bulletStuff.elType = this.elType;
                // bulletStuff.damage = this.damage;
                hasToken = false;

                // at the end of each fire, use a coroutine to wait.
                StartCoroutine(WaitToFire());
            }
            else {
                tok.GetInLine(this);
            }
        }
    }

    bool canFire = true;
    
    IEnumerator WaitToFire() {
        canFire = false;
        yield return new WaitForSeconds(1);
        canFire = true;
    }
}
