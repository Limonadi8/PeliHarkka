using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCycle : MonoBehaviour
{
    public Text timeText;

    public Light valo;


    // Start is called before the first frame update
    void Start()
    {



        timeText.text = "Päivä: 1";

    }
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, 5f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        if (valo.transform.position.y < -10)
        {

            valo.intensity = 0.3f;
            //   valo.enabled = false;
        }
        if (valo.transform.position.y > 30)
        {
            valo.intensity = 1f;

            // valo.enabled = true;
        }



    }

    // Update is called once per frame

}
