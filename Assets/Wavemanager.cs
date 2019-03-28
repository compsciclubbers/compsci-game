using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wavemanager : MonoBehaviour
{
    public GameObject spider;
    public GameObject smallSpider;
    public GameObject shroom;
    public int currentWave;
    private bool spsp;
    private bool smspsp;
    private bool shsp;
    private int spamt;
    private int smspamt;
    private int shamt;
    public int numberOfEnemies;
    private bool waveFinished;
    private double startTime;
    private GameObject wavecanvas;
    public GameObject cactusgun;
    public GameObject shield;
    public GameObject magnet;
    public GameObject healer;
    // Start is called before the first frame update
    void Start()
    {
        currentWave = 1;
        spsp = false;
        smspsp = true;
        shsp = false;
        spamt = 0;
        smspamt = 6;
        shamt = 2;
        waveFinished = true;
        startTime = Time.time;
        wavecanvas = GameObject.FindGameObjectWithTag("WaveCanvas");
        cactusgun.SetActive(false);
        shield.SetActive(false);
        magnet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(currentWave >= 2)
        {
            shield.SetActive(true);
            shsp = true;
        }
        if(currentWave >= 3)
        {
            cactusgun.SetActive(true);
            spsp = true;
        }
        if(currentWave >= 4)
        {
            magnet.SetActive(true);
        }
        if (waveFinished)
        {
            if (Time.time - startTime > 1.5)
            {
                wavecanvas.SetActive(false);
                startWave();
            }
            else
            {
               wavecanvas.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Wave " + currentWave;
               wavecanvas.SetActive(true); 
            }
            
        }
        else
        {
            startTime = Time.time;
        }
        if(numberOfEnemies < 2 && !waveFinished)
        {
            endWave();
        }
    }

    private void startWave()
    {
        print(currentWave + " " + spsp + " " + smspsp + " " + shsp + " " + spamt + " " + smspamt + " " + shamt);
        numberOfEnemies = 0;
        if (spsp)
        {
            int loopTimes = spamt  + (currentWave - 1) / 2;
            for (int i = 0; i < loopTimes; i++)
            {
                float randx = Random.Range(50, 155);
                float randz = Random.Range(60, 160);
                GameObject spawned = Instantiate(spider, new Vector3(randx, 35, randz), Quaternion.identity);
                spawned.SetActive(true);
                numberOfEnemies++;
            }
        }
        if (smspsp)
        {
            int loopTimes = smspamt + currentWave / 4;
            for (int i = 0; i < loopTimes; i++)
            {
                float randx = Random.Range(50, 155);
                float randz = Random.Range(60, 160);
                GameObject spawned = Instantiate(smallSpider, new Vector3(randx, 35, randz), Quaternion.identity);
                spawned.SetActive(true);
                numberOfEnemies++;
            }
        }
        if (shsp)
        {
            int loopTimes = shamt + currentWave / 2;
            for (int i = 0; i < loopTimes; i++)
            {
                float randx = Random.Range(50, 155);
                float randz = Random.Range(60, 160);
                GameObject spawned = Instantiate(shroom, new Vector3(randx, 35, randz), Quaternion.identity);
                spawned.SetActive(true);
                numberOfEnemies++;
            }
        }
        waveFinished = false;
    }
    private void endWave()
    {
        healer.SetActive(true);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (obj.activeSelf)
            {
                Destroy(obj);
            }
        }
        waveFinished = true;
        currentWave++;
    }
        

}
