using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Timers;
using System;

public class FPSmove : MonoBehaviour
{
    public Slider hp;
    public Animator transition;
    private int healt = 140;

    public float moveSpeed;
    //public Rigidbody theRb;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

   
   // public Animator anim;
    public Transform pivot;
    public float rotateSpeed;
    public GameObject bullet;
    public GameObject sword;
    public GameObject playerModel;
    public bool shootagain = true;


    // Start is called before the first frame update
    void Start()
    {
        //theRb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();




    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (shootagain)
            {
                shootagain = false;
                Shoot();
                StartCoroutine(ShootingTimeout());
            }
        }
    

    IEnumerator ShootingTimeout()
    {
        yield return new WaitForSeconds(0.8f);
        shootagain = true;
    }

   
    void Shoot()
        {
            var bullet1 = Instantiate(bullet, sword.transform.position, sword.transform.rotation);
            bullet1.GetComponent<Rigidbody>().velocity = bullet1.transform.forward * 30;
            Destroy(bullet1, 1);
        }

        //theRb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRb.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

        //if(Input.GetButtonDown("Jump"))
        //{
        //    theRb.velocity = new Vector3(theRb.velocity.x, jumpForce, theRb.velocity.z);
        //}
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        moveDirection.y = moveDirection.y + (gravityScale * Physics.gravity.y * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        //Move the player in different directions based on camera look direction
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            // transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

      /*  anim.SetFloat("Healt", 100);
        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));*/


    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cave2"))
        {
            LoadNextLevel();

        }
        if (other.gameObject.CompareTag("Hurt"))
        {
            healt = healt - 5;
            hp.value = healt;
            Debug.Log(healt);
            if (healt < 5)
            {
                StartCoroutine(LoadLevelAgain());
            }
        }
    }


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevelAgain()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt("Prog", 3);
        SceneManager.LoadScene("Cave");
    }
    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Game");
    }
}


