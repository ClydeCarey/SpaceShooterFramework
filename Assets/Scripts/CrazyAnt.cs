using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyAnt : MonoBehaviour
{
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    private float _fireRate = 3.0f;
    private float _canFire = -1;

    private int randomX = 0;
    private int randomY = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomValues());

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

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        

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
    IEnumerator RandomValues()
    {
        while(true)
        {            
            randomX = Random.Range(-1, 2);
            randomY = Random.Range(-1, 2);
            yield return new WaitForSeconds(Random.Range(1.3f, 2.8f));  //(4.0f); 
        }
             
    }
    void CalculateMovement()
    {
        //transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Translate(new Vector3(randomX, randomY) * _speed * Time.deltaTime);

        if (transform.position.y < -5.0f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            //_speed = 0;
            transform.GetComponent<BoxCollider2D>().enabled = false;
            _audioSource.Play();
            Destroy(this.gameObject, 2.0f);
        }

        if (other.tag == "Laser")
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


}
