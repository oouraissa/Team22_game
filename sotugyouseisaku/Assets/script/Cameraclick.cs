using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraclick : MonoBehaviour
{
    public Camera Camera_object;

    public string message;
    public ObjectClick objectclick;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera_object.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit.collider.gameObject.name == gameObject.name)
            {
                //objectclick.Novel();
            }

        }

    }
}
