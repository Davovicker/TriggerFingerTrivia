using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Windows.Speech;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    // This is the instructional text in the center of the screen
    public Text Shooter;

    

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }


    // voice recognition stuff ----------------------------------------------------
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Player information ----------------------------------------------------------
    public int shooterOne;
    public int shooterTwo;
    public int asweringShooter;
    public int damagedShooter;


    public int maxHealth = 5;

    //prospector pete
    public int P1CurrentHealth;
    public bool P1ActivePlayer = false;
    //public bool P1Loses = false;

    //Dastardly Dave
    public int P2CurrentHealth;
    public bool P2ActivePlayer = false;
    //public bool P2Loses = false;


    //Bandit Betty
    public int P3CurrentHealth;
    public bool P3ActivePlayer = false;
    //public bool P3Loses = false;


    //Cowboy Carlos
    public int P4CurrentHealth;
    public bool P4ActivePlayer = false;
    //public bool P4Loses = false;



    public P1HealthBar prospectorPeteHealthBar;
    public P2HealthBar dastardlyDaveHealthBar;
    public P3HealthBar luckyLucyHealthBar;
    public P4HealthBar cowboyCarlos;

    

    //this is used to display who is answering a question
    public string activeShooter = " ";



    // player answering value
    string answeringPlayer = "place holder";

    // Correct and False timer
    public float CandFTimer;
    public Text CandFTimerDisplay;



    // Start is called before the first frame update
    void Start()
    {
        
        // player health setters -------------------------------------
        P1CurrentHealth = maxHealth; //COWBOY CARLOS, CRACK
        P2CurrentHealth = maxHealth; //DASTARDLY DAVE, DRAW
        P3CurrentHealth = maxHealth; //BANDIT BETTY, BANG
        P4CurrentHealth = maxHealth; //PROSPECTOR PETE, POW

        cowboyCarlos.SetMaxHealth(maxHealth); // PLAYER 1
        dastardlyDaveHealthBar.SetMaxHealth(maxHealth); //PLAYER 2
        luckyLucyHealthBar.SetMaxHealth(maxHealth); //PLAYER 3
        prospectorPeteHealthBar.SetMaxHealth(maxHealth); //PLAYER 4




        // These are trigger words -------------------------------------------------------------

        actions.Add("click", Click); // cowboy carlos
        actions.Add("draw", Draw); // Dastardly Dave
        actions.Add("Bang", Bang); // Bandit Betty
        actions.Add("pow", Pow); // prospector pete




        // Correct Answers Go Here -------------------------------------------------------
        actions.Add("Twenty Four Hours", twentyFourHours);
        actions.Add("One Hundred Centimeters", centimeters);
        actions.Add("1899", eightTeenNintyNine);





        // False Answers  -------------------------------------------------------
        //question 1
        actions.Add("600 Minutes", sixHundredMinutes);
        actions.Add("5,000,000 seconds", fiveMillSeconds);
        actions.Add("Never", never);
        //question 2 
        actions.Add("Forty Inches", fortyInches);
        actions.Add("2 yards", twoYards);
        actions.Add("A Quarter Mile", quarterMile);
        //question 3
        actions.Add("1868", eighteenSixtyEight);
        actions.Add("nine teen twenty three", nineTeenTwentyThree);
        actions.Add("nine teen forty three", nineTeenFortyThree);




        // voice recognition stuff --------------------------------------------
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();



    }



    // This should count down from 3 that should be executed after an answer and befor the next question
    // CandF Timer
    IEnumerator CandFDisplayTimer()
    {
        while (CandFTimer >= 0)
        {
            CandFTimerDisplay.text = CandFTimer.ToString();

            yield return new WaitForSeconds(1f);

            CandFTimer--;

            if (CandFTimer <= 0)
            {
                CandFTimer = 3;
                //firstQuestionComplete = true;
                // GameManager.S.resetShooter();
            }
        }

    }





    // voice recognition stuff --------------------------------------------
    private void RecognizedSpeech(PhraseRecognizedEventArgs Speech)
    {
        Debug.Log(Speech.text);
        actions[Speech.text].Invoke();

    }







    // Update is called once per frame -------------------------------------------------------------
    void Update()
    {
        Debug.Log("Game Manager working");

        // these effect players health bars based on key inputs
        // to be updated to be called based on answers 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            P1TakeDamage(1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            P2TakeDamage(1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            P3TakeDamage(1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            P4TakeDamage(1);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            //playerPicker();
        }

        //checkForEndGame();




    }


    // End game Conditions -----------------------------------------------------------------
    /*void checkForEndGame()
    {
        if (P1CurrentHealth == 0)
        {
            P1Loses = true;
            Debug.Log("Cowboy Carlos Loses:" + P1Loses);
        }
        else if (P2CurrentHealth == 0)
        {
            P2Loses = true;
            Debug.Log("Dastardly Dave Loses:" + P1Loses);

        }
        else if (P3CurrentHealth == 0)
        {
            P3Loses = true;
            Debug.Log("Bandit Betty Loses:" + P1Loses);

        }
        else if (P4CurrentHealth == 0)
        {
            P4Loses = true;
            Debug.Log("Prospector Pete Loses:" + P1Loses);

        }

        if (P2Loses && P3Loses && P4Loses)
        {
            SceneManager.LoadScene("cowboyCarlosWins");
        }
        else if ( P1Loses && P3Loses && P4Loses)
        {
            SceneManager.LoadScene("dastardlyDaveWins");
        }
        else if ( P1Loses && P2Loses && P4Loses)
        {
            SceneManager.LoadScene("banditBettyWins");
        }
        else if ( P1Loses && P2Loses && P3Loses)
        {
            SceneManager.LoadScene("prospectorPeteWins");

        }
    }
*/




    // to be updated to be called based on answers  -------------------------------------------------------------
    void P1TakeDamage(int damage)
    {
        P1CurrentHealth -= damage;

        cowboyCarlos.SetHealth(P1CurrentHealth);
    }
    void P2TakeDamage(int damage)
    {
        P2CurrentHealth -= damage;

        dastardlyDaveHealthBar.SetHealth(P2CurrentHealth);

    }
    void P3TakeDamage(int damage)
    {
        P3CurrentHealth -= damage;

        luckyLucyHealthBar.SetHealth(P3CurrentHealth);

    }
    void P4TakeDamage(int damage)
    {
        P4CurrentHealth -= damage;

        prospectorPeteHealthBar.SetHealth(P4CurrentHealth);

    }

    public void resetShooter()
    {
        Shooter.text = "";
        
    }


    




    // Trigger word Functions -------------------------------------------------------------


    private void Click()
    {
        if(shooterOne == 0)
        {
            // tells computer which player is answering
            Debug.Log("Cowboy Carlos Answers");

            // This is what is Displayed on Screen
            answeringPlayer = "Cowboy Carlos";
            Debug.Log(answeringPlayer);

            //This is used for comparison and to deal damage
            asweringShooter = 0;
            

            Shooter.text = "Cowboy Carlos";
            P2ActivePlayer = true;


            //keywordRecognizer.Stop();
            //Debug.Log("Listening has stopped");
        }
        else if (shooterTwo == 0 )
        {
            // tells computer which player is answering
            Debug.Log("Cowboy Carlos Answers");

            // This is what is Displayed on Screen
            answeringPlayer = "Cowboy Carlos";
            Debug.Log(answeringPlayer);

            //This is used for comparison and to deal damage
            asweringShooter = 0;
            damagedShooter = shooterOne;


            Shooter.text = "Cowboy Carlos";
            P2ActivePlayer = true;


            //keywordRecognizer.Stop();
            //Debug.Log("Listening has stopped");
        }
        else
        {
            Shooter.text = "Its not your turn";
        }



    }
    private void Draw()
    {
        if( shooterOne == 1)
        {
            // tells computer which player is answering
            Debug.Log("Dastardly Dave Answers");

            // This is what is Displayed on Screen
            answeringPlayer = "Dastardly Dave";
            //Debug.Log(answeringPlayer);

            //This is used for comparison and to deal damage
            asweringShooter = 1;
            damagedShooter = shooterTwo;

            Shooter.text = "Dastardly Dave \n You can now answer the question";
            Debug.Log(answeringPlayer);
            P3ActivePlayer = true;


            //keywordRecognizer.Stop();
            //Debug.Log("Listening has stopped");
        }
        else if ( shooterTwo == 1)
        {
            // tells computer which player is answering
            Debug.Log("Dastardly Dave Answers");

            // This is what is Displayed on Screen
            answeringPlayer = "Dastardly Dave";
            //Debug.Log(answeringPlayer);

            //This is used for comparison and to deal damage
            asweringShooter = 1;
            damagedShooter = shooterOne;


            Shooter.text = "Dastardly Dave \n You can now answer the question";
            Debug.Log(answeringPlayer);
            P3ActivePlayer = true;


            //keywordRecognizer.Stop();
            //Debug.Log("Listening has stopped");
        }
        else
        {
            Shooter.text = "Its not your turn";
        }



    }
    private void Bang()
    {
        if (shooterOne == 2)
        {
            // tells computer which player is answering
            Debug.Log("Bandit Betty Answers");
            answeringPlayer = "Bandit Betty";


            //This is used for comparison and to deal damage
            asweringShooter = 2;
            damagedShooter = shooterTwo;


            Shooter.text = "Bandit Betty Shot first";
            Debug.Log(answeringPlayer);
            P4ActivePlayer = true;


            // keywordRecognizer.Stop();
            // Debug.Log("Listening has stopped");
        }
        else if ( shooterTwo == 2)
        {
            // tells computer which player is answering
            Debug.Log("Bandit Betty Answers");
            answeringPlayer = "Bandit Betty";


            //This is used for comparison and to deal damage
            asweringShooter = 2;
            damagedShooter = shooterOne;


            Shooter.text = "Bandit Betty Shot first";
            Debug.Log(answeringPlayer);
            P4ActivePlayer = true;


            // keywordRecognizer.Stop();
            // Debug.Log("Listening has stopped");
        }
        else
        {
            Shooter.text = "Its not your turn";
        }

    }



    private void Pow()
    {
        if( shooterOne == 3)
        {
            // tells computer which player is answering
            Debug.Log("Prospector Pete Answers");

            // This is what is Displayed on Screen
            answeringPlayer = "Prospector Pete";


            //This is used for comparison and to deal damage
            asweringShooter = 3;
            damagedShooter = shooterTwo;


            Shooter.text = "Prospector Pete";
            Debug.Log(answeringPlayer);
            P1ActivePlayer = true;

            //keywordRecognizer.Stop();
            //Debug.Log("Listening has stopped");

        }
        else if ( shooterTwo == 3)
        {
            // tells computer which player is answering
            Debug.Log("Prospector Pete Answers");

            // This is what is Displayed on Screen
            answeringPlayer = "Prospector Pete";


            //This is used for comparison and to deal damage
            asweringShooter = 3;
            damagedShooter = shooterOne;


            Shooter.text = "Prospector Pete";
            Debug.Log(answeringPlayer);
            P1ActivePlayer = true;

            //keywordRecognizer.Stop();
            //Debug.Log("Listening has stopped");
        }
        else
        {
            Shooter.text = "Its not your turn";
        }



    }











    // Correct  -------------------------------------------------------------
    //this is a function that recognises if a player is active to be used for dealing damage
    //maybe add multiple active players and then an active shooter
    // use an int for comparison between answering player and active shooters
    // deal damage to active player 
    // this will use the Shooter One and Shooter Two Varibles
    // Use the active shooter for comparison?

    public void answeringPlayerIsCorrect()
    {
        if (damagedShooter == 0)
        {
            P1TakeDamage(1);
        }
        else if (damagedShooter == 1)
        {
            P2TakeDamage(1);
        }
        else if (damagedShooter == 2)
        {
            P3TakeDamage(1);
        }
        else if (damagedShooter == 3)
        {
            P4TakeDamage(1);
        }
    }

    public void answeringPlayerIsWrong()
    {
        if (asweringShooter == 0)
        {
            P1TakeDamage(1);
        }
        else if (asweringShooter == 1)
        {
            P2TakeDamage(1);
        }
        else if (asweringShooter == 2)
        {
            P3TakeDamage(1);
        }
        else if (asweringShooter == 3)
        {
            P4TakeDamage(1);
        }
    }




    // Correct Answer Functions -------------------------------------------------------------------
    private void twentyFourHours()
    {
        Debug.Log("Correct");
        Shooter.text = "Correct";

        //checkForActivePlayerCorrect();

        answeringPlayerIsCorrect();


    }
    private void centimeters()
    {
        Debug.Log("Correct");
        Shooter.text = "Correct";

        //checkForActivePlayerCorrect();
        answeringPlayerIsCorrect();


    }
    private void eightTeenNintyNine()
    {
        Debug.Log("Correct");
        Shooter.text = "Correct";

        //checkForActivePlayerCorrect();
        answeringPlayerIsCorrect();


    }

    // False Answer Functions ----------------------------------------------------------
    //question 1
    private void sixHundredMinutes()
    {
        Debug.Log("False");
        Shooter.text = "False";

        //checkForActivePlayerFalse();
        answeringPlayerIsWrong();
    }
    private void fiveMillSeconds()
    {
        Debug.Log("False");
        Shooter.text = "False";

        //checkForActivePlayerFalse();
        answeringPlayerIsWrong();



    }
    private void never()
    {
        Debug.Log("False");
        Shooter.text = "False";

        //checkForActivePlayerFalse();
        answeringPlayerIsWrong();


    }
    //question 2 
    private void fortyInches()
    {
        Debug.Log("False");
        Shooter.text = "False";

        //checkForActivePlayerFalse();
        answeringPlayerIsWrong();


    }
    private void twoYards()
    {
        Debug.Log("False");
        Shooter.text = "False";

        //checkForActivePlayerFalse();
        answeringPlayerIsWrong();


    }
    private void quarterMile()
    {
        Debug.Log("False");
        Shooter.text = "False";

        //checkForActivePlayerFalse();
        answeringPlayerIsWrong();


    }
    //question 3
    private void eighteenSixtyEight()
    {
        Debug.Log("False");
        Shooter.text = "False";

        //checkForActivePlayerFalse();
        answeringPlayerIsWrong();


    }
    private void nineTeenTwentyThree()
    {
        Debug.Log("False");
        Shooter.text = "False";
        //checkForActivePlayerFalse();
        answeringPlayerIsWrong();


    }
    private void nineTeenFortyThree()
    {
        Debug.Log("False");
        Shooter.text = "False";

        //checkForActivePlayerFalse();
        answeringPlayerIsWrong();


    }
}
