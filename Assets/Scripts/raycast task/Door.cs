using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject opened_door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
            opened_door.SetActive(true);
        }
    }
}
