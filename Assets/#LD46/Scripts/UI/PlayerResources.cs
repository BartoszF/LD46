using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{

    public static PlayerResources INSTANCE;
    public TMP_Text moneyText;
    public int muni = 1000;

    void Awake()
    {
        INSTANCE = this;
    }
    void Start()
    {
        moneyText.text = muni.ToString();
    }

    public bool spendMuniIfPossible(int spending)
    {
        if (spending <= muni)
        {
            muni -= spending;
            moneyText.text = muni.ToString();
            return true;
        }
        return false;
    }

    public void addMuni(int muniToAdd)
    {
        muni += muniToAdd;
        moneyText.text = muni.ToString();
    }
}
