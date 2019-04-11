using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage = 1;
    public float speed = 1;

    private Rigidbody rb;
    private Transform t;

    public string target;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        float radians = (t.rotation.eulerAngles.y) * Mathf.PI / 180.0f;
        rb.velocity = new Vector3(-Mathf.Sin(radians), 0, Mathf.Cos(radians)).normalized * speed;
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.tag == target)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log(this.gameObject.name + "became Invisible");
        GameObject.Destroy(this.gameObject);
    }
}
