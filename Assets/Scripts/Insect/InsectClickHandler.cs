using UnityEngine;
using UnityEngine.InputSystem;

public class InsectClickHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
            LayerMask mask = LayerMask.GetMask("insect");
            if(Physics.Raycast(ray,out RaycastHit hit,100f,mask))
            {
                Debug.Log("Hit: " + hit.collider.name);
                Insect insect = hit.collider.GetComponent<Insect>();
                if(insect!=null)
                {
                    insect.HandleClick();
                }
                else
                {
                    Debug.Log("Raycast hit nothing");
                }
            }
        }
    }
}
