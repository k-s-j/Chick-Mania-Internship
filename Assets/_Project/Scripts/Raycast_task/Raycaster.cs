using UnityEngine;
using UnityEngine.Events;

public class Raycaster : MonoBehaviour
{
    public UnityEvent light_1_Button_event;
    public UnityEvent light_5_Button_event;
    public UnityEvent Door_event;
    public UnityEvent grab_event;
    RaycastHit hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 60f))
        {
            Debug.DrawRay(transform.position, fwd * 10, Color.green);
            
            if (hit.collider.gameObject.CompareTag("Button"))
            {   
                Debug.Log("button");
                   
                if (Input.GetKeyDown(KeyCode.E))
                {
                    light_1_Button_event.Invoke();
                }
            }
            else if(hit.collider.gameObject.CompareTag("Door"))
            {
                Debug.Log("door");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Door_event.Invoke();
                }
            }
            else if(hit.collider.gameObject.CompareTag("Grab"))
            {
                Debug.Log("grab");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    grab_event.Invoke();
                }
            }
            else if(hit.collider.gameObject.CompareTag("Button_5"))
            {
                Debug.Log("button5");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    light_5_Button_event.Invoke();
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, fwd * 10, Color.red);
        }
    }
}
