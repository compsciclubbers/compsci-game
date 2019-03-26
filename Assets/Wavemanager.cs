using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavemanager : MonoBehaviour
{
    public GameObject spider;
    public GameObject smallSpider;
    public GameObject shroom;
    private int currentWave;
    private bool spsp;
    private bool smspsp;
    private bool shsp;
    private int spamt;
    private int smspamt;
    private int shamt;
    public int numberOfEnemies;
    private bool waveFinished;
    // Start is called before the first frame update
    void Start()
    {
        currentWave = 1;
        spsp = false;
        smspsp = true;
        shsp = false;
        spamt = 1;
        smspamt = 6;
        shamt = 3;
        waveFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWave >= 2)
        {
            shsp = true;
        }
        if(currentWave >= 3)
        {
            spsp = true;
        }
        if (waveFinished)
        {
            startWave();
            
        }
        if(numberOfEnemies < 3 && !waveFinished)
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
            int loopTimes = spamt + currentWave / 2;
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
            int loopTimes = smspamt + currentWave / 2;
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
            int loopTimes = shamt + currentWave;
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
