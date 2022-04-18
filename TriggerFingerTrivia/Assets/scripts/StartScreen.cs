using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using System.Linq;
using System;



public class StartScreen : MonoBehaviour
{
    // this is the varible that will chage to true and launch the game
    //private bool startGame;
    public bool cowboyCarlosReady;
    public bool dastardlyDaveReady;
    public bool banditBettyReady;
    public bool prospectorPeteReady;


    // Voice Recognition Stuff
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();


    // Start is called before the first frame update
    void Start()
    {
       // startGame = false;
        cowboyCarlosReady = false;
        dastardlyDaveReady = false;
        banditBettyReady = false;
        prospectorPeteReady = false;

        actions.Add("click", Click);
        actions.Add("draw", Draw);
        actions.Add("bang", Bang);
        actions.Add("pow", Pow);
        actions.Add("start game", StartGame);

        //Voice Recognition stuff
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (cowboyCarlosReady && dastardlyDaveReady && banditBettyReady && prospectorPeteReady)
        {
            SceneManager.LoadScene("TFTGame");
        }
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs Speech)
    {
        //startGame = true;
        Debug.Log(Speech.text);
        actions[Speech.text].Invoke();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("TFTGame");
    }
    public void Click()
    {
        cowboyCarlosReady = true;
        Debug.Log("Cowboy Carlos is Ready = " + cowboyCarlosReady);

    }
    public void Draw()
    {
        dastardlyDaveReady = true;
        Debug.Log("Dastardly Dave is Ready = " + dastardlyDaveReady);

    }
    public void Bang()
    {
        banditBettyReady = true;
        Debug.Log("Bandit Betty is Ready = " + banditBettyReady);

    }
    public void Pow()
    {
        prospectorPeteReady = true;
        Debug.Log("Prospector Pete is Ready = " + prospectorPeteReady);

    }
}
