using NUnit.Framework;
using UnityEngine;

public class grab_object : MonoBehaviour
{

    [SerializeField] GameObject attach_point;
    bool is_grabbed = false;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(is_grabbed == true)
        {
            rb.isKinematic = true;
            transform.position = attach_point.transform.position;
        }
        else
        {
            rb.isKinematic = false;
        }
        
    }
    public void grabbed()
    {
        is_grabbed = !is_grabbed;
    }
}
