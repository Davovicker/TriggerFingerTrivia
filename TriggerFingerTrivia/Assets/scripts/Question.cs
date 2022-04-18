using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;



public class Question : MonoBehaviour
{

    //Players
    public int playerOne;
    public int previosPlayerOne;
    public int playerTwo;
    public string playerOneDisplay;
    public string playerTwoDisplay;
    public int playerOneHealth;
    public int playerTwoHealth;

    public int P1Health;
    public bool P1Loses;

    public int P2Health;
    public bool P2Loses;

    public int P3Health;
    public bool P3Loses;

    public int P4Health;
    public bool P4Loses;



    // Game stuff
    public GameObject questionBanner; // this will be the boc where the tect is displayed reciept
    public GameObject questionText; // this will be the quetion display  
    public GameObject answerText;
    

    private TextAsset QandAText; 
    private string[] questionsList;
    private Text questionDisplay;
    public Text answerDisplay;
    public string questionName;

    public GameObject instructionsText;
    public Text instructionsDisplay;
    public string instructions;

    public string[] answersChecklist;


    private bool firstQuestion = true;
    public bool newQuestion = true;

    private bool firstQuestionComplete;

    //might need to come in and add start and end points for the banner and the text box if you want the banner tto move

    private int prevQ;

    //Timmer
    public float firstQuestionTime;
    public float otherQuestionTime;
    public float questionTime;
    private float timeLeft;
    private float prevTime;

    //Timer Diplay
    public Text countdownDisplay;
    public float countdownTime;

    public static Question S;


    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }

    private void Start()
    {
        //questionDisplay = GetComponentInChildren<Text>();
        //questionDisplay.text = "";
       
        
        StartCoroutine(GetTextFromFile());
        StartCoroutine(countdownToNextQuestion());

        //Timer
        timeLeft = firstQuestionTime;
        questionTime = firstQuestionTime;

        firstQuestionComplete = false;
    }


    IEnumerator countdownToNextQuestion()
    {
        while (countdownTime >= 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
            
            
            if (countdownTime <= 0)
            {
                //Change back to 30 SEC --------------------------------------------------------------------------------------
                countdownTime = 10;
                //firstQuestionComplete = true;
                // GameManager.S.resetShooter();
                checkForEndGame();
            }
        }

    }

    IEnumerator GetTextFromFile()
    {
        // load the text file with the orders in it
        QandAText = Resources.Load("QandA") as TextAsset;

        // split the question and answer file into a string array where each line is an element of the array
        questionsList = QandAText.text.Split('\n');

        yield return null;

    }

    private void Update()
    {
        /* Debug.Log("Qustion is working");
         Debug.Log("fisrt question = " + firstQuestion);
         Debug.Log("New question = " + newQuestion);

         Debug.Log("first question is complete " + firstQuestionComplete);*/
        //checkForEndGame();


        //start with easy question
        if (firstQuestion && newQuestion)
        {
            //Debug.Log("If statement has been hit");
            FirstQuestion();
            firstQuestion = false;
            newQuestion = false;
      
        }

        //when the player needs the next question, spawn next question
        if (newQuestion)
        {
            firstQuestionComplete = true;

            NewQuestion();
            newQuestion = false;

            //Timer
            timeLeft = questionTime;
            //change question?
        }

        if (firstQuestionComplete)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }

            else if (timeLeft <= 0)
            {
                //Timer start
                questionTime = otherQuestionTime;
                timeLeft = questionTime;

                // Get new order
                NewQuestion();
                newQuestion = false;


            }
        }

       

        float time = Mathf.FloorToInt(timeLeft % 60);

       // Debug.Log(time);

        if (time < 16)
        {
            prevTime = time;
        }

    }


    void FirstQuestion()
    {
        //Now using these to pick players and to referance varribles to Game manager
        pickShooterOne();
        pickShooterTwo();
        displayOpponentInstructions();

        // get the first question
        string[] fullQandALine = questionsList[0].Split('=');

        //Debug.Log("question list array is : " + questionsList.Length);
        
        prevQ = 0;

        // display the question on the banner
        questionName = fullQandALine[0];

        //Debug.Log(questionName);

        QuestionName.S.SetText();
  

        // display answers below the banner
        string answers = fullQandALine[1].Replace("+", "\n");
        answerDisplay.text = answers;

        //Debug.Log(answers);

        firstQuestionComplete = true;


    }

    void NewQuestion()
    {
        //Now using these to pick players and to referance varribles to Game manager
        pickShooterOne();
        pickShooterTwo();
        displayOpponentInstructions();


        //Debug.Log("New Question has been hit");

        int Q = Random.Range(1, questionsList.Length);

        if ( Q == prevQ)
        {
            prevQ = Q;
            Q = Random.Range(1, questionsList.Length);
        }
        prevQ = Q;
        string[] fullQandALine = questionsList[Q].Split('=');

        // display the question on the banner
        questionName = fullQandALine[0];
        //Debug.Log(fullQandALine);
        QuestionName.S.SetText();

        // make an array of answers for checking scores later
        answersChecklist = fullQandALine[1].Split('+');

        // display answers below the banner
        string answer = fullQandALine[1].Replace("+", "\n");
        answerDisplay.text = answer;

        //play associated audio clip
        //AudioManager.S.QuestionVoices[Q - 1].Play(0);

        //update timer if needed
        questionTime = otherQuestionTime;

    }


    public void pickShooterOne()
    {
        playerOne = Random.Range(0, 3);

        if (playerOne == 0)
        {
            playerOneHealth = GameManager.S.P1CurrentHealth;
        }
        if (playerOne == 1)
        {
            playerOneHealth = GameManager.S.P2CurrentHealth;

        }
        if (playerOne == 2)
        {
            playerOneHealth = GameManager.S.P3CurrentHealth;

        }
        if (playerOne == 3)
        {
            playerOneHealth = GameManager.S.P4CurrentHealth;

        }

        while (playerOne == previosPlayerOne || playerOneHealth == 0)
        {
            Debug.Log("Player One is " + playerOne + "Previous Player One was " + previosPlayerOne);
            playerOne = Random.Range(0, 3);

            if (playerOne == 0)
            {
                playerOneHealth = GameManager.S.P1CurrentHealth;
            }
            else if (playerOne == 1)
            {
                playerOneHealth = GameManager.S.P2CurrentHealth;

            }
            else if (playerOne == 2)
            {
                playerOneHealth = GameManager.S.P3CurrentHealth;

            }
            else if (playerOne == 3)
            {
                playerOneHealth = GameManager.S.P4CurrentHealth;

            }
        }

       

        GameManager.S.shooterOne = playerOne;
     /*   Debug.Log("Player One is " + playerOne + " Game Manger Shooter One is " + GameManager.S.shooterOne);
        Debug.Log("Player Ones's Health is " + playerOneHealth);*/

    }


    public void pickShooterTwo()
    {
        playerTwo = Random.Range(0, 3);
        //Debug.Log(playerTwo);

        if (playerTwo == 0)
        {
            playerTwoHealth = GameManager.S.P1CurrentHealth;
        }
        else if (playerTwo == 1)
        {
            playerTwoHealth = GameManager.S.P2CurrentHealth;

        }
        else if (playerTwo == 2)
        {
            playerTwoHealth = GameManager.S.P3CurrentHealth;

        }
        else if (playerTwo == 3)
        {
            playerTwoHealth = GameManager.S.P4CurrentHealth;

        }

        while (playerOne == playerTwo || playerTwoHealth <= 0)
        {
            playerTwo = Random.Range(0, 3);

            if (playerTwo == 0)
            {
                playerTwoHealth = GameManager.S.P1CurrentHealth;
            }
            else if (playerTwo == 1)
            {
                playerTwoHealth = GameManager.S.P2CurrentHealth;

            }
            else if (playerTwo == 2)
            {
                playerTwoHealth = GameManager.S.P3CurrentHealth;

            }
            else if (playerTwo == 3)
            {
                playerTwoHealth = GameManager.S.P4CurrentHealth;

            }
        }
        GameManager.S.shooterTwo = playerTwo;
/*        Debug.Log("Player Two is " + playerTwo + " Game Manger Shooter Two is " + GameManager.S.shooterTwo);
        Debug.Log("Player Two's Health is " + playerTwoHealth);*/

    }

    public void displayOpponentInstructions()
    {
        if (playerOne == 0)
        {
            playerOneDisplay = "Cowboy Carlos VS ";
        }
        else if (playerOne == 1)
        {
            playerOneDisplay = "Dastardly Dave VS ";

        }
        else if (playerOne == 2)
        {
            playerOneDisplay = "Bandit Betty VS ";

        }
        else if (playerOne == 3)
        {
            playerOneDisplay = "Prospector Pete VS ";

        }

        if (playerTwo == 0)
        {
            playerTwoDisplay = "Cowboy Carlos";

        }
        else if (playerTwo == 1)
        {
            playerTwoDisplay = "Dastardly Dave";

        }
        else if (playerTwo == 2)
        {
            playerTwoDisplay = "Bandit Betty";

        }
        else if (playerTwo == 3)
        {
            playerTwoDisplay = "Prospector Pete";

        }

        instructions = playerOneDisplay + playerTwoDisplay;
        instructionsDisplay.text = instructions;
    }


  /*  public void setTimer()
    {
        timeLeft = 0;
        countdownTime = 0;
        countdownToNextQuestion();
    }*/

    void checkForEndGame()
    {
        P1Health = GameManager.S.P1CurrentHealth;
        Debug.Log("P1 Health is:" + P1Health);

        P2Health = GameManager.S.P2CurrentHealth;
        Debug.Log("P2 Health is:" + P1Health);

        P3Health = GameManager.S.P3CurrentHealth;
        Debug.Log("P3 Health is:" + P1Health);

        P4Health = GameManager.S.P4CurrentHealth;
        Debug.Log("P4 Health is:" + P1Health);


        if (P1Health <= 0)
        {
            Debug.Log("Player Ones health is less than 0");
            P1Loses = true;
            Debug.Log("Cowboy Carlos Loses:" + P1Loses);
        }
        if (P2Health <= 0)
        {
            Debug.Log("Player two health is less than 0");
            P2Loses = true;
            Debug.Log("Dastardly Dave Loses:" + P2Loses);

        }
        if (P3Health <= 0)
        {
            Debug.Log("Player three health is less than 0");
            P3Loses = true;
            Debug.Log("Bandit Betty Loses:" + P3Loses);

        }
        if (P4Health <= 0)
        {
            Debug.Log("Player four health is less than 0");
            P4Loses = true;
            Debug.Log("Prospector Pete Loses:" + P4Loses);
        }

        if (P2Loses && P3Loses && P4Loses)
        {
            SceneManager.LoadScene("cowboyCarlosWins");
        }
        else if (P1Loses && P3Loses && P4Loses)
        {
            SceneManager.LoadScene("dastardlyDaveWins");
        }
        else if (P1Loses && P2Loses && P4Loses)
        {
            SceneManager.LoadScene("banditBettyWins");
        }
        else if (P1Loses && P2Loses && P3Loses)
        {
            SceneManager.LoadScene("prospectorPeteWins");

        }
    }
}




