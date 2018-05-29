using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script's purpose is to save the objects position and rotation when calling the Store() function and restoring both to the saved state with the Restore() function
/// </summary>
public class RestorableTransform : MonoBehaviour {

	private Vector3 initialPosition;
	private Quaternion initialRotation;

	public void Store(){
		initialPosition = this.transform.position;
		initialRotation = this.transform.rotation;
	}

	public void Restore(){
		this.transform.position = initialPosition;
		this.transform.rotation = initialRotation;
	}
}
