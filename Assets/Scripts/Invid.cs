using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invid : MonoBehaviour
{
    [SerializeField]    
    private float _speed = 2.0f;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;

    private float _fireRate = 3.0f;
    private float _canFire = -1;
    [SerializeField]
    private GameObject _bossMissilePrefab;

    // Start is called before the first frame update
    void OnEnable()
    {
        transform.position = new Vector3(0, 8, 0);
        StartCoroutine(InvidMoveDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 3)
        {
            Vector3 farLeft = new Vector3(-7.7f, 3, 0);
            Vector3 farRight = new Vector3(7.7f, 3, 0);

            transform.position = Vector3.Lerp(farLeft, farRight, (Mathf.Sin(_speed * Time.time) + 1.0f) / 2.0f);

        }

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_bossMissilePrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }



    IEnumerator InvidMoveDown()
    {
        _speed = 0.5f;
        while (transform.position.y > 3)
        {
            Debug.Log("boss coroutine");
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            yield return null; // new WaitForSeconds(0.04f);
            
        }
       
        yield return new WaitForSeconds(0.75f);       
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

            //_anim.SetTrigger("OnEnemyDeath");
            ////_speed = 0;
            //transform.GetComponent<BoxCollider2D>().enabled = false;
            //_audioSource.Play();
            //Destroy(this.gameObject, 2.0f);
        }

        //if (other.tag == "Laser" && other.tag != "EnemyLaser")
        //{
        //    Destroy(other.gameObject);
        //    if (_player != null)
        //    {
        //        _player.AddScore(10);
        //    }



        //    _anim.SetTrigger("OnEnemyDeath");
        //    //_speed = 0;
        //    transform.GetComponent<BoxCollider2D>().enabled = false;
        //    _audioSource.Play();
        //    Destroy(this.gameObject, 2.0f);
        //}

        //if (other.tag == "Missile")
        //{
        //    Destroy(other.gameObject);
        //    if (_player != null)
        //    {
        //        _player.AddScore(10);
        //    }



        //    _anim.SetTrigger("OnEnemyDeath");
           
        //    _audioSource.Play();
        //    Destroy(this.gameObject, 2.0f);
        //}
    }

}
