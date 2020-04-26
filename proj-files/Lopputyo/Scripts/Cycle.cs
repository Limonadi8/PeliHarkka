using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cycle : MonoBehaviour
{
    private float multi = 1.2f;
    public Light valo;


    // Start is called before the first frame update
    void Start()
    {
 

    }
    void Update()
    {
        
        transform.RotateAround(Vector3.zero, Vector3.right, multi * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        if (valo.transform.position.y < -10)
        {
            multi = 5.5f;
            
            valo.intensity = 0.3f;
         //   valo.enabled = false;
        }
        if (valo.transform.position.y > 0)
        {
            multi = 1.2f;
            valo.intensity = 1f;
            
            // valo.enabled = true;
        }

        
        
    }

    // Update is called once per frame
    
}
