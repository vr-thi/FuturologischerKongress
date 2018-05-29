using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSabreControl : MonoBehaviour {

    public OSC osc;

    private float last;

    Vector3 previous;
    float velocity;


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
      
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;

        if (velocity > 100)
        {
            velocity = 100;
        }

        velocity = velocity / 70;

        previous = transform.position;

        if (velocity != last)
        {       
            OscMessage message = new OscMessage();
            message.address = "/velocity";
            message.values.Add(velocity);
            osc.Send(message);
            Debug.Log(velocity);
        }

        last = velocity;
    }
}
