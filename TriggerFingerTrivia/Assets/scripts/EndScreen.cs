using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using System.Linq;
using System;



public class EndScreen : MonoBehaviour
{



    // Voice Recognition Stuff
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();



    // Start is called before the first frame update
    void Start()
    {
        actions.Add("New Game", newGame);


        //Voice Recognition stuff
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs Speech)
    {
        Debug.Log(Speech.text);
        actions[Speech.text].Invoke();
    }

    public void newGame()
    {
        SceneManager.LoadScene("Start Screen");
    }


}
