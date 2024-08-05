using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class General : MonoBehaviour
{
    public static GameObject Redhouse;
    public static GameObject newhouse;
    public static GameObject Houses;
    public static GameObject gameOverCard;

    public static GameObject BaseCar;
    public static GameObject pauseButton;
    public static List<GameObject> cars;
    public static List<string> carNames;
    public static GameObject carParent;
    public static Sprite[] spriteArray;
    public static bool gameOver = false;
    public static System.Random rnd;
    public static int totalTrains;
    public static int startTrains;
    public static int arrivedTrains;
    public static int Score = 0;
    public static List<GameObject> houseList;
    public static List<Vector2> housePositions;

    void Awake()
    {
        houseList = new List<GameObject>(13);
        housePositions = new List<Vector2>(13);
        Houses = GameObject.Find("Houses");
        pauseButton = GameObject.Find("pauseButton");
        for (int i = 0; i < 13; i++)
        {
            houseList.Add(Houses.transform.GetChild(i).gameObject);
            housePositions.Add(Houses.transform.GetChild(i).transform.position);
        }
        resetAll();
        CreateHouses();
        InvokeRepeating("CreateCarsWrapper", 0f, 1.4f);
    }

    public static void resetAll()
    {
        arrivedTrains = 0;
        totalTrains = 90;
        startTrains = totalTrains;
        Score = 0;
        Redhouse = GameObject.Find("Red");

        carParent = GameObject.Find("Trains");
        BaseCar = GameObject.Find("/Trains/Base");
        spriteArray = Resources.LoadAll<Sprite>("Cars");
        cars = new List<GameObject>();
        rnd = new System.Random();
        
        gameOver = false;
        gameOverCard = GameObject.Find("gameOverCard");
        if (gameOverCard == null)
        {
            Debug.LogWarning("gameovercard is null");
        }
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
        for (int i = 0; i < carsNum; i++)
        {
            
            int colourNumber = rnd.Next(0, 13);
            GameObject carClone = Instantiate(BaseCar, new Vector2(25 + (i*3), -3.5f), BaseCar.transform.rotation);
            carClone.name = spriteArray[colourNumber].name;

            carClone.transform.parent = carParent.transform;
            cars.Add(carClone);

            carClone.GetComponent<SpriteRenderer>().sprite = spriteArray[colourNumber];

            totalTrains--;
            
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
