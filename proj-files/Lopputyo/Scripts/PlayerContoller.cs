using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Timers;
using System;

public class PlayerContoller : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject canvasd;
    public GameObject maincanvas;
    public GameObject nyrkki;

    bool swim;
    bool triggered = false;
    private static System.Timers.Timer aTimer;
    private int count;
    public Text countText;
    public Text coinText;

    public float moveSpeed;
    //public Rigidbody theRb;
    public float jumpForce;
    public CharacterController controller;

    private bool inhand = false;
    private Vector3 moveDirection;
    public float gravityScale;


    public Animator transition;
    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;
    Vector3 pickupposition;

    string e;
    private int coincount = 0;
    private int prog;

    public GameObject playerModel;
    public GameObject fishingrod;
    public GameObject righthand;
    public GameObject pickups;
    private bool shootagain = true;

    public float timer = 1f;


    public Slider hpSlide;

    // Start is called before the first frame update
    void Start()
    {
        //theRb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        hpSlide.maxValue = 100;
        hpSlide.value = 100;
        var onki = fishingrod.GetComponent<Collider>();
        onki.isTrigger = true;
        rb = GetComponent<Rigidbody>();
        nyrkki.SetActive(false);
        canvasd.SetActive(true);

        count = PlayerPrefs.GetInt("count");
        coincount = PlayerPrefs.GetInt("coincount");
        coinText.text = "RAHAA : " + coincount;
        //InvokeRepeating("VoidFall", 0, 0.5f);
        if (count != 0)
        {
            countText.text = "Löydetty: " + count.ToString() + "/5";
        }
   
    }

    /*void VoidFall()
    {
        if (!controller.isGrounded)
        {
            timer = timer + 1f;
            Debug.Log(timer);
            if (timer > 20)
            {
                StartCoroutine(EndGame());
            }

        }
        if (controller.isGrounded)
        {
            timer = 0;
        }
    }*/


    // Update is called once per frame
    void Update()
    {
      

            //theRb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRb.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

            //if(Input.GetButtonDown("Jump"))
            //{
            //    theRb.velocity = new Vector3(theRb.velocity.x, jumpForce, theRb.velocity.z);
            //}
            //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
            float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical") ) + (transform.right * Input.GetAxis("Horizontal") );
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
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 )
            {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

        if(hpSlide.value < 10)
        {
            
            StartCoroutine(EndGame());


        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            fishingrod.transform.parent = null;
            moveSpeed = 10;
            inhand = false;
           

        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            maincanvas.SetActive(true);
            canvasd.SetActive(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            moveSpeed = 40;


        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("Prog", 1);
            LoadNextLevel2();


        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("Prog", 3);
            LoadNextLevel();

        }
        
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            maincanvas.SetActive(false);
            canvasd.SetActive(true);

            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            
            
        }

        if (controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (shootagain)
                {
                    shootagain = false;
                    Shoot();
                    StartCoroutine(SetHitFalse());
                    
                }


            }
        }
        IEnumerator SetHitFalse()
        {

            yield return new WaitForSeconds(1f);
            shootagain = true;
            moveSpeed = 10f;
            nyrkki.SetActive(false);
            anim.ResetTrigger("shoot");
        }
        void Shoot()
        {
            inhand = false;
            moveSpeed = 0.1f;
            nyrkki.SetActive(true);
            anim.SetTrigger("shoot");
        }






        if (Input.GetKeyDown(KeyCode.E) && triggered)
        {
            
            
            
            fishingrod.transform.parent = righthand.transform;
            
            fishingrod.transform.rotation = righthand.transform.rotation;
            fishingrod.transform.localEulerAngles = new Vector3(0, -90, 0);
            pickupposition = righthand.transform.position;
            
            fishingrod.transform.position = pickupposition;
            inhand = true;
        }
        if (inhand == true)
        {

            moveSpeed = 8;

        }




        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        anim.SetFloat("Healt", hpSlide.value);
        
    }
    

        public void ChangeScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Water2")
        {
            anim.SetBool("swim", true);
            moveSpeed = 8;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = PlayerPrefs.GetInt("count");
            if (count < 6)
            {
                count = count + 1;
                PlayerPrefs.SetInt("count", count);
                Debug.Log(count);
                e = "Löydetty: " + count.ToString() + "/5";
                SetCountText(e);
            }
            if (count == 5)
            {
                PlayerPrefs.SetInt("Prog",1);
            }
            
        }
        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Prog", 2);

        }
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            coincount = coincount + 1;
            PlayerPrefs.SetInt("coincount", coincount);
            coinText.text = "RAHAA : " + coincount;
        }

        if (other.gameObject.CompareTag("Hurt"))
        {
            hpSlide.value = hpSlide.value - 5;
            
        }
        if (other.gameObject.CompareTag("Cave"))
        {
            PlayerPrefs.SetInt("Prog", 3);
            LoadNextLevel();

        }
        if (other.gameObject.CompareTag("Cave2"))
        {
            
            LoadNextLevelGame2();

        }
        
        if (other.gameObject.CompareTag("Mountain"))
        {
            
            LoadNextLevel2();

        }
        if (other.gameObject.CompareTag("FishingRod"))
        {
            triggered = true;
           
        }
        if (other.gameObject.CompareTag("Healing"))
        {
            hpSlide.value = hpSlide.value + 20f;
            Debug.Log(hpSlide.value);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FishingRod"))
        {
            triggered = false;
        }

        

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {


            anim.SetBool("swim", false);
            moveSpeed = 10;

        }
    }

    void SetCountText(string e)
    {
        Debug.Log(e);
        countText.text = e;
       
    }

    public void LoadNextLevel()
    {
       StartCoroutine( LoadLevel());
    }
    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Cave");
    }
    public void LoadNextLevel2()
    {
        StartCoroutine(LoadLevel2());
    }
    IEnumerator LoadLevel2()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Mountain");
    }
    public void LoadNextLevelGame2()
    {
        StartCoroutine(LoadLevelGame());
    }
    IEnumerator LoadLevelGame()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Game");
    }
    IEnumerator EndGame()
    {

        countText.text = "Hävisit pelin =(";
        moveSpeed = 0;
        jumpForce = 0;
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(5f);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }


}

