using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player p = other.gameObject.GetComponent<Player>();
        if (other.gameObject.tag == "Player")
        {
            p.Damaged(1,(p.transform.position - transform.position).normalized);
        }
    }
}
