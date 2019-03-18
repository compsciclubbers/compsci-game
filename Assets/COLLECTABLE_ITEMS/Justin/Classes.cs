using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : DamagableEntity
{
    public int hp;
    public int attdmg;
    public string type;
    public Entity(int c_hp, int c_attdmg, string c_type)
    {
        hp = c_hp;
        attdmg = c_attdmg;
        type = c_type;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public abstract class DamagableEntity
{
    public void damage(int attdmg, int hp, GameObject self)
    {
        if(hp - attdmg <= 0)
        {
            hp = 0;
            die(self);
        }
        else
        {
            hp -= attdmg;
        }

    }
    private void die(GameObject self)
    {
        self.SetActive(false);
    }
}
