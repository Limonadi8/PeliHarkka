using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryLine : MonoBehaviour
{
    
    public int spawn;
    public GameObject sudet;
    public GameObject coin;
    public GameObject caveblock;
    public GameObject avain;
    public GameObject kallioblock;
    public GameObject kalliotrigger;
    public GameObject luolatrigger;
    public GameObject pickups;
    public Text task;
    public Text taskdesc;
    private int progres;
    // Start is called before the first frame update
    void Start()
    {
        kalliotrigger.SetActive(false);
        kallioblock.SetActive(true);
        InvokeRepeating("Progres", 0, 3f);
    }
    void Progres()
    {
        progres = PlayerPrefs.GetInt("Prog");
        if (progres == 1)
        {
            task.text = "Löysit kaikki! Suuntaa vuorelle etsimään avainta ja miekkaa!(Tab-lisätietoja)";
            kalliotrigger.SetActive(true);
            kallioblock.SetActive(false);
            pickups.SetActive(false);
            taskdesc.text = "Löysit kaikki palikat. Nyt suuntaa Yksinäisillevuorille muumitalon edessä, sieltä löydät avaimen ja miekan...? (seuraa polkua, joudut hieman hyppimään)";
        }
        if (progres == 2)
        {
            task.text = "Hienoa sait avaimen mörön luolaan!(Tab-lisätietoja)";
            caveblock.SetActive(false);
            avain.SetActive(false);
            pickups.SetActive(false);
            taskdesc.text = "Avaimella pääset mörönluolaan, joka sijaitsee muumitalon takana, kivien välissä!";
        }
        if (progres == 3)
        {
            task.text = "Mörkö vaanii luolan varjoissa..!!";
            kalliotrigger.SetActive(true);
            caveblock.SetActive(false);
            kallioblock.SetActive(false);
            pickups.SetActive(false);
            avain.SetActive(false);
           // taskdesc.text = "Etsi mörkö luolista ja näytä sille... Vapauta muumilaakso!";
        }
        if (progres == 4)
        {
            task.text = "Tuhoa mörkö ja pakene !!?";
            kalliotrigger.SetActive(true);
            caveblock.SetActive(false);
            kallioblock.SetActive(false);
            pickups.SetActive(false);
            avain.SetActive(false);
            //taskdesc.text = "Käytä miekkaasi Vasemmalla Hiiren näppäimellä ja ammu mörköä! Mörkö on vahva!";
        }
        if (progres == 5)
        {
            task.text = "LOISTAVAA pelastit muumilaakson, toistaiseksi...?";
            kalliotrigger.SetActive(true);
            caveblock.SetActive(false);
            kallioblock.SetActive(false);
            pickups.SetActive(false);
            avain.SetActive(false);
            sudet.SetActive(false);
           taskdesc.text = "Nyt voit rauhassa tutustua muumilaaksoon, läpäisit pelin!";
        }
        //COINS
        if (spawn == 1)
        {
            if (Random.Range(0, 3) == 1)
            {

                Instantiate(coin, new Vector3(Random.Range(120, 170), -18.5f, Random.Range(210, 320)), Quaternion.identity);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
