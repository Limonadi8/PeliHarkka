using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Press : MonoBehaviour

{
    public Animator transition;
    public GameObject leveloader;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = text.GetComponent<Text>();
        leveloader.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScene()
    {
        leveloader.SetActive(true);
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Game");
    }
    public void Ohjeet()
    {
        text.enabled = true;
    }
}
