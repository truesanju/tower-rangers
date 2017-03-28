using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public static int Gold;

    public int InitialGold = 1000;

    public static int Lives;

    public int InitialLives = 1000;



    public static int Rounds;



    void Start()

    {

        Gold = InitialGold;

        Lives = InitialLives;



        Rounds = 0;

    }



}
