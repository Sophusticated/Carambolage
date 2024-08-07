using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class General : MonoBehaviour
{
    public GameObject Houses;
    public GameObject gameOverCard;

    public GameObject BaseCar;
    public GameObject pauseButton;
    public static int levelNo;
    public static List<GameObject> cars;
    public static List<string> carNames;
    public static GameObject carParent;
    public static Sprite[] spriteArray;
    public static Sprite[] tutorialSpriteArray;
    public static bool gameOver = false;
    public static System.Random rnd;
    public static int totalTrains;
    public static int startTrains;
    public static int arrivedTrains;
    public static int Score = 0;
    public List<GameObject> houseList;
    public static List<Vector2> housePositions;

    void Awake()
    {
        levelNo = SceneManager.GetActiveScene().buildIndex;
        housePositions = new List<Vector2>(houseList.Count);
        Houses = GameObject.Find("Houses");
        for (int i = 0; i < houseList.Count; i++)
        {
            housePositions.Add(Houses.transform.GetChild(i).transform.position);
        }
        resetAll();
        CreateHouses();
        if (levelNo == 1)
        {
            InvokeRepeating("CreateCarsWrapper", 0f, 3f);
        }
        if (levelNo == 2)
        {
            InvokeRepeating("CreateCarsWrapper", 0f, 1.4f);
        }
    }

    public void resetAll()
    {
        if (levelNo == 1)
        {
            totalTrains = 10;
        }
        else if (levelNo == 2)
        {
            totalTrains = 90;
        }
        arrivedTrains = 0;
        startTrains = totalTrains;
        Score = 0;


        carParent = GameObject.Find("Trains");
        spriteArray = Resources.LoadAll<Sprite>("Cars");
        tutorialSpriteArray = Resources.LoadAll<Sprite>("Cars/TutorialCars");
        cars = new List<GameObject>();
        rnd = new System.Random();
        
        
        gameOver = false;
        pauseButton.SetActive(true);
        gameOverCard.GetComponent<Canvas>().sortingOrder = -10;
        gameOverCard.transform.GetChild(0).gameObject.SetActive(false);
        gameOverCard.transform.GetChild(1).gameObject.SetActive(false);
        gameOverCard.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().sortingOrder = 10;
        gameOverCard.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void CreateCarsWrapper()
    {
        CreateCars(1);
    }

    void Update()
    {
        if (totalTrains == 0)
        {
            CancelInvoke();
        }
        if (arrivedTrains == startTrains)
        {
            gameOver = true;
        }

        if (gameOver)
        {
            pauseButton.SetActive(false);
            if (Score > PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", Score);
                gameOverCard.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = ("Highscore!!" + System.Environment.NewLine + "   " + Score + " points!" + System.Environment.NewLine + "High Score: " + PlayerPrefs.GetInt("Highscore"));
            }
            else
            {
                gameOverCard.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = ("Game Over!" + System.Environment.NewLine + "   " + Score + " points!" + System.Environment.NewLine + "High Score: " + PlayerPrefs.GetInt("Highscore"));
            }

            gameOverCard.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameOverCard.GetComponent<Canvas>().sortingOrder = 10;
            gameOverCard.transform.GetChild(0).gameObject.SetActive(true);
            gameOverCard.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void CreateCars(int carsNum)
    {
        if (levelNo == 1)
        {
            //Tutorial
            for (int i = 0; i < carsNum; i++)
            {
                int colourNumber = rnd.Next(0, 6);
                GameObject carClone = Instantiate(BaseCar, new Vector2(-5f, -13.5f), BaseCar.transform.rotation);
                carClone.name = tutorialSpriteArray[colourNumber].name;
                carClone.GetComponent<SpriteRenderer>().sprite = tutorialSpriteArray[colourNumber];

                carClone.transform.parent = carParent.transform;
                cars.Add(carClone);

                totalTrains--;

            }
        }
        else if (levelNo == 2)
        {
            //Challenge
            for (int i = 0; i < carsNum; i++)
            {

                int colourNumber = rnd.Next(0, 14);
                GameObject carClone = Instantiate(BaseCar, new Vector2(25, -3.5f), BaseCar.transform.rotation);
                carClone.name = spriteArray[colourNumber].name;
                carClone.GetComponent<SpriteRenderer>().sprite = spriteArray[colourNumber];

                carClone.transform.parent = carParent.transform;
                cars.Add(carClone);

                totalTrains--;

            }

        }
    }


    public void CreateHouses()
    {   
        housePositions.Shuffle();
        for (int i = 0; i < housePositions.Count; i++)
        {
            houseList[i].transform.position = housePositions[i];
        }
    }
}
