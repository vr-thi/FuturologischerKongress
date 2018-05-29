using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cave;

public class Lasersword_Flystick : CollisionSynchronization {

	public GameObject flyStick;

	public Lasersword_Flystick()
		: base(new[] { Cave.EventType.OnCollisionEnter })
	{

	}

	// Use this for initialization
	void Start () {
		flyStick = GameObject.Find ("Flystick").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (flyStick == null) {
			flyStick = GameObject.Find ("Flystick").gameObject;
			this.gameObject.SetActive (false);
		} else {
			Debug.Log ("Rotation " + flyStick.transform.rotation);
			Debug.Log ("Lightsaber " + this.gameObject.transform.rotation);
			this.gameObject.transform.position = flyStick.transform.position;
			this.gameObject.transform.rotation = flyStick.transform.rotation;
//			Quaternion dummyQuaternion = new Quaternion ();
//			dummyQuaternion.Set(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z, this.gameObject.transform.rotation.w);
//			this.gameObject.transform.rotation = dummyQuaternion;
			this.gameObject.transform.Rotate(180, 0, 90);
			this.gameObject.SetActive (true);
		}

	}
}
