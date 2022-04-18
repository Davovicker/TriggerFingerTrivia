using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //this is where all the audio sources go themem music sound effedt etc. 
    public AudioSource[] QuestionVoices;

    public static AudioManager S;

    private void Awake()
    {
        if ( S == null )
        {
            S = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame there is nothing in update
    void Update()
    {
        
    }
}
