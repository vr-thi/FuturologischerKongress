using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cave;

/// <summary>
/// Destroyable makes a GameObject alter between two child GameObjects after a collision happened or the GameObject is reset
/// It can be useful for example to change between an undestroyed and a destroyed version of a mesh.
/// </summary>
public class Destroyable : CollisionSynchronization {
	// The speed at which the GameObject is flying towards Z
	public float speed = 1f;
	// The Z value at which the GameObject will be reset to its initial position
	public float destroyAtZ = -2f;
	// The initial prefab
	public GameObject normalPrefab;
	// The prefab used after Colliding with destroyableBy
	public GameObject destroyedPrefab;
	// The GameObject causing this GameObject to change states
	public GameObject destroyableBy;
	private MeshCollider destroyableByCollider;

	// Constructor needed for OnSynchronizedCollision in Cave
	public Destroyable() : base(new[] {Cave.EventType.OnCollisionEnter})
	{

	}

	/// <summary>
	/// Initialize values
	/// </summary>
	void Start () {
		// Find the MeshCollider of the active child
		destroyableByCollider = destroyableBy.GetComponentInChildren<MeshCollider>();
		// Set the initial prefabs
		normalPrefab.SetActive (true);
		destroyedPrefab.SetActive (false);
		// Store Transforms for all children
		this.GetComponent<RestorableTransform> ().Store ();
		foreach (RestorableTransform rest in this.GetComponentsInChildren<RestorableTransform>()) {
			rest.Store ();
		}
		foreach (RestorableTransform rest in normalPrefab.GetComponentsInChildren<RestorableTransform>()) {
			rest.Store ();
		}
		foreach (RestorableTransform rest in destroyedPrefab.GetComponentsInChildren<RestorableTransform>()) {
			rest.Store ();
		}
	}

	/// <summary>
	/// Update function used for moving towards Z
	/// </summary>
	void Update() {
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - TimeSynchronizer.deltaTime * speed);
		if (this.transform.position.z < destroyAtZ) {
			Reset ();
		}
	}

	/// <summary>
	/// Function used to reset the Object and every child object. Restores Transforms.
	/// </summary>
	private void Reset(){
		if (destroyedPrefab.activeSelf || !normalPrefab.activeSelf) {
			destroyedPrefab.SetActive (false);
			normalPrefab.SetActive (true);
		}
		this.GetComponent<RestorableTransform> ().Restore ();
		foreach (RestorableTransform rest in this.GetComponentsInChildren<RestorableTransform>()) {
			rest.Restore ();
		}
		foreach (RestorableTransform rest in normalPrefab.GetComponentsInChildren<RestorableTransform>()) {
			rest.Restore ();
		}
		foreach (RestorableTransform rest in destroyedPrefab.GetComponentsInChildren<RestorableTransform>()) {
			rest.Restore ();
		}
	}

	/// <summary>
	/// Raises the synchronized collision enter event.
	/// </summary>
	/// <param name="other">The GameObject this object collided with</param>
	public override void OnSynchronizedCollisionEnter(GameObject other){
		// Only collide if it collided with the specified destroyableByCollider and the normalPrefab is active and a destroyedPrefab exists
		if (other.name == destroyableByCollider.name && destroyedPrefab != null && normalPrefab.activeSelf) {
			normalPrefab.SetActive (false);
			destroyedPrefab.transform.position = normalPrefab.transform.position;
			destroyedPrefab.transform.rotation = normalPrefab.transform.rotation;
			destroyedPrefab.SetActive (true);
		}
	}
}
