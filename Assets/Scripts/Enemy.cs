﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _laserPrefab;

    private bool moveDownNow = false;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    private float _fireRate =3.0f;
    private float _canFire = -1;

    private Vector3 _playerPositionEnemyScript;
    private Vector3 _playerLaserPosition;
    private Vector3 _distanceToPlayer;
    [SerializeField]
    private float _kamikazeSpeed = 0.05f;
    private float _ramThreshold = 5.0f;
    private float _laserDistanceThreshold = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The animator is NULL");
        }

        StartCoroutine(EnemyMoveDown());
    }

    // Update is called once per frame
    void Update()
    {
        

        if (moveDownNow == true)
        {
            CalculateMovement();
        }

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
                
            }
        }
    }

    void CalculateMovement()
    {
        _playerLaserPosition = GameObject.FindGameObjectWithTag("Laser").transform.position;
        _playerPositionEnemyScript = GameObject.FindGameObjectWithTag("Player").transform.position;
        
        _distanceToPlayer = _playerPositionEnemyScript - transform.position;
        

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        

        if (Vector3.Distance(_playerPositionEnemyScript, transform.position) < _ramThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerPositionEnemyScript, _kamikazeSpeed);
        }

        if (Vector3.Distance(_playerLaserPosition, transform.position) < _laserDistanceThreshold)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }

        if (transform.position.y < -5.0f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7, 0);
        }
                
    }

    private void OnTriggerEnter2D(Collider2D other)   
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player !=null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            //_speed = 0;
            transform.GetComponent<BoxCollider2D>().enabled = false;
            _audioSource.Play();
            Destroy(this.gameObject, 2.0f);            
        }

        if (other.tag == "Laser" && other.tag != "EnemyLaser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }



            _anim.SetTrigger("OnEnemyDeath");
            //_speed = 0;
            transform.GetComponent<BoxCollider2D>().enabled = false;
            _audioSource.Play();
            Destroy(this.gameObject,2.0f);            
        }

        if (other.tag == "Missile")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }



            _anim.SetTrigger("OnEnemyDeath");
            //_speed = 0;
            transform.GetComponent<BoxCollider2D>().enabled = false;
            _audioSource.Play();
            Destroy(this.gameObject, 2.0f);
        }
    }
    IEnumerator EnemyMoveDown()
    {
        _speed = 4.0f;
        while (transform.position.y > 3)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime); 
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            if (transform.position.x > 11.0f)
            {                
                transform.position = new Vector3(-11.0f, 7, 0);
            }
            yield return null;

        }

        yield return new WaitForSeconds(0.75f);
        moveDownNow = true;
    }


}
