using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramAnimate : MonoBehaviour
{
    public AnimationCurve curve;

    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(0,curve.Evaluate(Mathf.Sin(Time.time)),0,Space.Self);
    }

}
