using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour {
    private Vector3 center;
    public Vector3 delta;
    public float angularSpeed;

    void Start()
    {
        center = transform.position;
    }

    void Update()
    {
        Vector3 position = center;
        position.x += delta.x * Mathf.Cos(Time.time * angularSpeed);
        position.y += delta.y * Mathf.Sin(Time.time * angularSpeed);
        transform.position = position;
    }
}