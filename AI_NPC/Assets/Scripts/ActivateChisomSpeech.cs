using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChisomSpeech : MonoBehaviour
{
    public GameObject locationIndicator;
    public GameObject chisomAudioSource;
    public GameObject chisomSequence;
    public AudioSource chisomAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            locationIndicator.SetActive(false);
            chisomAudioSource.SetActive(true);
            chisomSequence.SetActive(true);
            chisomAudio.Play();
        }
    }
}
