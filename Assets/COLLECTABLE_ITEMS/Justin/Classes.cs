using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagableEntity : MonoBehaviour
{
    public int hp;
    private int maxhp;
    public int attdmg;
    public string type;
    public bool isDead;
    public bool isHit;
    public GameObject self;
    public int incomingDmg; 
    public DamagableEntity(int c_hp, int c_attdmg, string c_type)
    {
        hp = c_hp;
        maxhp = c_hp;
        attdmg = c_attdmg;
        type = c_type;
        isDead = false;
        incomingDmg = 0;
        isHit = false;
    }
    public void damage()
    {
        isHit = true;
        if(hp - incomingDmg <= 0)
        {
            hp = 0;
            isDead = true;
        }
        else
        {
            hp -= incomingDmg;
            print(type + " hp: " + hp);
        }
    }
    public void dealDamage(GameObject reciever)
    {
        isHit = true;
        DamagableEntity rec = reciever.GetComponent<DamagableEntity>();
        rec.incomingDmg = attdmg;
        rec.damage();
    }
    public void updateHealth()
    {
        Image health = null;
        foreach(Transform c in transform)
        {
            if(c.gameObject.tag == "Health")
            {
                health = c.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            }
        }
        health.fillAmount = (float)hp / (float)maxhp;
        if(health.fillAmount > .65)
        {
            health.color = new Color(0, (float)health.fillAmount * 255, 0, 255);
        }
        else if(health.fillAmount > .3)
        {
            health.color = new Color(((float)1 - health.fillAmount) * 255, (float)health.fillAmount * 255, 0, 255);
        }
        else
        {
            health.color = new Color(((float)1 - health.fillAmount) * 255, 0, 0, 255);
        }
    }
    public bool getDead()
    {
        return isDead;
    }
    public bool getHit()
    {
        return isHit;
    }
    public void setHit(bool hit)
    {
        isHit = hit;
    }

}
