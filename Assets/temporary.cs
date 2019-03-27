using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class temporary : DamagableEntity
{
    public GameObject endgame;
    public temporary() :this(20,1,"player"){
        
    }
    public temporary(int hp, int dmg, string type) :base(hp,dmg,type){
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (getDead())
        {
            gameObject.SetActive(false);
            endgame.SetActive(true);
            endgame.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Waves Survived: " + GameObject.FindGameObjectWithTag("WaveCheck").GetComponent<Wavemanager>().currentWave;
        }
        updateHealth();
    }
}
