using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GameObject dieeffect;
    private int healt;
    public Transform target;
    public int MoveSpeed = 4;
    public int MaxDist = 10;
    public int MinDist = 5;
    public Animator anim2;
    // Start is called before the first frame update
    void Start()
    {
        healt = 20;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        
        if (Vector3.Distance(transform.position, target.transform.position) >= MinDist)
        {
            




            if (Vector3.Distance(transform.position, target.transform.position) <= MaxDist)
            {

                
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                //Here Call any function U want Like Shoot at here or something
            }

        }
        anim2.SetBool("Moving", (Vector3.Distance(transform.position, target.transform.position) <= MaxDist));
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hit"))
        {
           
            this.healt = this.healt - 5;
            Instantiate(dieeffect, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);

            if (healt <= 0)
            {
                
                
                StartCoroutine(Die());
                this.gameObject.SetActive(false);
                Instantiate(dieeffect, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);

            }
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);

        dieeffect.SetActive(false);
    }
    }
