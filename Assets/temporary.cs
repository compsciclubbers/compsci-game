using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporary : DamagableEntity
{
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
        }
        updateHealth();
    }
}
