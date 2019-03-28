using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Godrays : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<DamagableEntity>().hp += 10;
            print("playe hp: " + other.gameObject.GetComponent<DamagableEntity>().hp);
            gameObject.SetActive(false);
        }
    }
}
