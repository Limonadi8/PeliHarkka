using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject hand;
    public Slider bossslider;
    public AudioSource audioSource;
    public GameObject skeleton;
    private int prog;
    private int o = 0;
    public GameObject dieeffect;
    private int healt;
    public Transform target;
    public float MoveSpeed;
    public int MaxDist = 10;
    public int MinDist = 5;
    public Animator anim2;
    public GameObject trap;
    // Start is called before the first frame update
    void Start()
    {
        prog = PlayerPrefs.GetInt("Prog");
        Debug.Log(prog);
        if(prog > 4)
        {
            skeleton.SetActive(false);
            bossslider.enabled = false;
        }
        if (prog == 4 || prog == 3)
        {
            InvokeRepeating("Lyonti", 0, 3f);

        }

        healt = 440;
        bossslider.value = healt;
        

    }
    void Lyonti()
    {
        
        hand.SetActive(true);
        StartCoroutine(Disable());
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(2f);
        hand.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if (o == 0)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= 28)
            {

                audioSource.Play();
                anim2.SetTrigger("Close");
                trap.SetActive(true);

                PlayerPrefs.SetInt("Prog", 4);
                prog = PlayerPrefs.GetInt("Prog");
                StartCoroutine(EnableBoss());
            }
        }



        if (o == 1)
        {
            if (Vector3.Distance(transform.position, target.transform.position) >= MinDist)
            {


                if (Vector3.Distance(transform.position, target.transform.position) <= MaxDist)
                {

                    transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                 
                }

            }
            anim2.SetBool("Moving", (Vector3.Distance(transform.position, target.transform.position) <= MaxDist));
            anim2.SetBool("Hit", (Vector3.Distance(transform.position, target.transform.position) <= 5));
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (o == 1)
        {
            if (other.gameObject.CompareTag("Hit"))
            {

                this.healt = this.healt - 5;
                bossslider.value = healt;
                Debug.Log(healt);

                if (healt <= 0)
                {


                    anim2.SetTrigger("Dead");
                    MoveSpeed = 0;
                    Instantiate(dieeffect, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), Quaternion.identity);

                    StartCoroutine(Die());

                }
            }
        }
    }
    IEnumerator EnableBoss()
    {
        yield return new WaitForSeconds(9f);
        o = 1;
    }

    IEnumerator Die()
    {
        
        yield return new WaitForSeconds(3f);
        audioSource.Stop();
        skeleton.SetActive(false);
        trap.SetActive(false);
        PlayerPrefs.SetInt("Prog", 5);
        dieeffect.SetActive(false);
    }
}
