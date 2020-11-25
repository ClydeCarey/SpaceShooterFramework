using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invid : MonoBehaviour
{
    private float _speed = 1.0f;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
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
    }



    IEnumerator InvidMoveDown()
    {
        while (transform.position.y > 3)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            yield return new WaitForSeconds(0.04f);
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
