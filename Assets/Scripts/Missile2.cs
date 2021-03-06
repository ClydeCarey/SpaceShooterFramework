﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile2 : MonoBehaviour
{

    [SerializeField]
    private float _missileSpeed = 8.0f;
    //[//SerializeField]
    //private GameObject _smoke;

    // Update is called once per frame
    void Update()
    {
        //Missile1MoveUp();
        Missile2MoveUp();
    }

    //void Missile1MoveUp()
    //{
    //    //transform.Translate(Vector3.up * _missileSpeed * Time.deltaTime);
    //    transform.Translate(new Vector3(-5, 1) * _missileSpeed * Time.deltaTime);

    //    if (transform.position.y > 18.0f)
    //    {
    //        if (transform.parent != null)
    //        {
    //            Destroy(transform.parent.gameObject);
    //        }

    //        Destroy(this.gameObject);
    //    }
    //}

    void Missile2MoveUp()
    {        
        transform.Translate(new Vector3(Random.Range(-2f, -0f), Random.Range(0.5f, 2f)) * _missileSpeed * Time.deltaTime);

        if (transform.position.y > 18.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

}

