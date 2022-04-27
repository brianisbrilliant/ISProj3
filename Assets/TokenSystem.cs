using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenSystem : MonoBehaviour
{
    public int totalTokens = 3;
    public int tokens = 1;      // set this based on difficulty
    public float tokenRechargeInterval = 2;

    public Queue<MoveTo> waitingLine = new Queue<MoveTo>();

    void Start() {
        tokens = totalTokens;
    }

    public void GetInLine(MoveTo ai) {

        Debug.Log("Calling GetInLine() from " + ai.gameObject.name);
        Debug.Log("waitingLine.Count = " + waitingLine.Count);
        // check to see if this AI is already in the list.
        // if(waitingLine.Count == 0) return;

        foreach(MoveTo enemy in waitingLine) {
            if(enemy == ai) {
                return; // this buddy is already in line.
            }
        }
        // we didn't find this guy in line? Let's add them.
        waitingLine.Enqueue(ai);
    }

    void Update() {
        if(waitingLine.Count == 0) return;

        if(tokens > 0) {
            Debug.Log("Giving a token.");
            waitingLine.Dequeue().hasToken = true;
            StartCoroutine(RechargeToken());
        }
    }

    public bool GrabToken() {
        if(tokens > 0) {
            tokens -= 1;
            StartCoroutine(RechargeToken());
            return true;
        } else {
            return false;
        }
    }

    IEnumerator RechargeToken() {
        tokens -= 1;
        yield return new WaitForSeconds(tokenRechargeInterval);
        tokens += 1;

        if(tokens > totalTokens) {
            tokens = totalTokens;
        }
    }
}
