using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private AudioClip _bgMusicClip;
    [SerializeField]
    private AudioClip _secondaryFireClip;
    private AudioSource _bgMusicSource;
    private AudioSource _secondaryFireSource;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _bgMusicSource = GetComponent<AudioSource>();
        //_secondaryFireSource = GetComponent<AudioSource>();
        _bgMusicSource.clip = _bgMusicClip;
        _bgMusicSource.Play();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (_player.isSecondaryFireActive == true)
    //    {
    //        //play sound 2
    //        _bgMusicSource.clip = _secondaryFireClip;
    //        Debug.Log("secondary is active!!!!");
    //        //_secondaryFireSource.Play();
    //    }
        
    //    else
    //    {
    //        _bgMusicSource.clip = _bgMusicClip;
    //        //_bgMusicSource.Play();

    //    }
    //    //_bgMusicSource.Play();
    //}
}
