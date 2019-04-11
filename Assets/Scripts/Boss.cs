using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private int health = 1000;
    public int maxHealth;

    public List<GameObject> sockets = new List<GameObject>();
    //public int num_sockets;
    public float shotDelay;
    private float shotTime;
    public GameObject projectile;
    private int sock_index;
    public bool onOff; // temporary variable so we can test shooting without going crazy;

    public float orbitRate;
    public int direction;

    public Color initialColor;
    public Color finalColor;
    public Material color;

    public List<Vector3> positions;
    private Vector3 prevPosition;
    public Vector3 initialPosition;
    private int pos_index;

    private Transform t;

    private float time;
    public float pathTime;

    // Start is called before the first frame update
    void Start()
    {

        time = 0;
        pos_index = 0;
        sock_index = 0;
        health = maxHealth;
        color.color = initialColor;
        t = this.GetComponent<Transform>();
        t.SetPositionAndRotation(initialPosition, t.rotation);
        prevPosition = initialPosition;
        onOff = false;

        SetPhase();
        SetOrbitRate();
        SetDirection();
        //SetRadius(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // if health falls below a certain level do: change colors, activate new attacks, change speed, etc.
        Debug.Log("Health: " + health);
        if (health <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
        color.color = Color.Lerp(initialColor, finalColor, (float) (maxHealth - health) / (float) maxHealth);

        // Movement from one location to the next. We can add the positions in Unity and this code will make the Boss travel
        // between them in sequence.

        if (time >= pathTime * Vector3.Distance(prevPosition, positions[pos_index]))
        {
            // if we want the boss to move at the same rate, we should find the distance between prevPosition and the next position,
            // and multiply the pathTime by that DONE
            prevPosition = positions[pos_index];
            pos_index = (pos_index + 1) % positions.Count;
            time = 0;
        }
        
        t.position = Vector3.Lerp(prevPosition, positions[pos_index], time / (pathTime * Vector3.Distance(prevPosition, positions[pos_index])));
        time += Time.deltaTime;

        // firing

        if (onOff && shotTime >= shotDelay)
        {
            shotTime = 0;
            Instantiate(projectile, sockets[sock_index].transform.position, sockets[sock_index].transform.rotation);
            sock_index = (sock_index + 1) % sockets.Count;
        }
        shotTime += Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("Boss Hit");
        if (other.tag == "Friendly Projectile")
        {
            health -= other.GetComponent<Projectile>().damage;
        }
        else { }
    }

    public int GetHealth()
    {
        return health;
    }

    private void SetPhase()
    {
        for (int i = 0; i < sockets.Count; i++)
        {
            sockets[i].GetComponent<Socket>().offset = 360f * i / sockets.Count;
        }
    }

    private void SetOrbitRate()
    {
        for (int i = 0; i < sockets.Count; i++)
        {
            sockets[i].GetComponent<Socket>().orbitRate = orbitRate;
        }
    }

    private void SetDirection()
    {
        for (int i = 0; i < sockets.Count; i++)
        {
            sockets[i].GetComponent<Socket>().direction = direction;
        }
    }

    private void SetRadius(float r)
    {
        for (int i = 0; i < sockets.Count; i++)
        {
            sockets[i].GetComponent<Socket>().radius = r;
        }
    }
}
