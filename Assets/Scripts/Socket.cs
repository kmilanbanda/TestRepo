using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public enum MotionType { Fixed, Orbit, BackNForth }; // fixed keeps socket attached to its parent, Orbit makes it orbit a position
    public MotionType motionType = MotionType.Orbit;

    public Vector3 center;
    public float radius = 1.0f;
    public float orbitRate = 1.0f; // if set to 1.0f, then the orbit will change one degree per second.
    public int direction; // set to -1 if you want to reverse the direction of the orbit;
    public float offset = 0; // this is in degrees

    private Transform t;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        t = this.GetComponent<Transform>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (motionType)
        {
            case MotionType.Fixed:
                break;
            case MotionType.Orbit:
                float radians = (orbitRate * time + offset) * Mathf.PI / 180;
                t.localPosition = new Vector3(((-Mathf.Sin(radians) - center.x) * radius), 0, ((Mathf.Cos(radians) - center.z) * radius));
                t.localRotation = Quaternion.Euler(0, orbitRate * time + offset, 0);
                break;
            case MotionType.BackNForth:
                break;
            default:
                break;
        }
        time += Time.deltaTime;
    }
}
