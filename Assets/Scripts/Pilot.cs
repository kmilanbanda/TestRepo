using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot : MonoBehaviour {
    public float speed = 1;
    public Rigidbody rb;
    public Transform t;

    public List<Transform> sockets;
    public float shotDelay;
    private float waitTime;
    private int shotsFired;

    public GameObject projectile;
    //private List<GameObject> projectiles = new List<GameObject>();
    //public int maxProjectiles;

    private int index;
    //private int index2 = 0;

	// Use this for initialization
	void Start () {
        waitTime = 0;
        shotsFired = 0;
	}
	
	// Update is called once per frame
	void Update () {

        // Movement

        if (Input.GetKey(KeyCode.LeftArrow)) {
            rb.velocity = new Vector3(-speed, 0, rb.velocity.z);
            if (t.rotation.eulerAngles.x <= 15) { rb.angularVelocity = new Vector3(0, 0, 1.5f); }
            else { rb.angularVelocity = new Vector3(0, 0, 0); t.SetPositionAndRotation(t.position, Quaternion.Euler(15, 270, 0)); }

        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            rb.velocity = new Vector3(speed, 0, rb.velocity.z);
            if (t.rotation.eulerAngles.x <= -15) { rb.angularVelocity = new Vector3(0, 0, -1.5f); }
            else { rb.angularVelocity = new Vector3(0, 0, 0); t.SetPositionAndRotation(t.position, Quaternion.Euler(-15, 270, 0)); }

        }
        else {
            rb.velocity = new Vector3(0, 0, rb.velocity.z);
            if (t.rotation.eulerAngles.x != 0)
            {
                t.SetPositionAndRotation(t.position, Quaternion.Euler(0, 270, 0));
            }
        }

        if (Input.GetKey(KeyCode.UpArrow)) { rb.velocity = new Vector3(rb.velocity.x, 0, speed); }
        else if (Input.GetKey(KeyCode.DownArrow)) { rb.velocity = new Vector3(rb.velocity.x, 0, -speed); }
        else { rb.velocity = new Vector3(rb.velocity.x, 0, 0); }

        // Boundaries

        if (t.position.x < -5.5) { t.SetPositionAndRotation(new Vector3(-5.5f, t.position.y, t.position.z), t.rotation); }
        if (t.position.x > 5.5) { t.SetPositionAndRotation(new Vector3(5.5f, t.position.y, t.position.z), t.rotation); }
        if (t.position.z > 4.3) { t.SetPositionAndRotation(new Vector3(t.position.x, t.position.y, 4.3f), t.rotation); }
        if (t.position.z < -4.3) { t.SetPositionAndRotation(new Vector3(t.position.x, t.position.y, -4.3f), t.rotation); }

        // Firing

        if (waitTime >= shotDelay)
        {
            
            index = shotsFired % sockets.Count;
            /*
            if (projectiles.Count < maxProjectiles)
            {
                GameObject obj;
                obj = (GameObject) Instantiate(projectile, sockets[index].position, sockets[index].rotation);
                projectiles.Add(obj);
            }
            else
            {
                
                projectiles[index2].GetComponent<Transform>().SetPositionAndRotation(sockets[index].position, sockets[index].rotation);
            }
            */
            Instantiate(projectile, sockets[index].position, sockets[index].rotation);
            waitTime = 0;
            shotsFired++;
            //index2 = (index2 + 1) % maxProjectiles;
        }
        waitTime += Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Friendly Projectile")
        {
            // player destroyed
            // play destruction animation/create particle effect
            GameObject.Destroy(this.gameObject);
        }
    }
}
