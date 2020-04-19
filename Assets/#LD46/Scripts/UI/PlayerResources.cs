using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{

    public static PlayerResources INSTANCE;
    public TMP_Text moneyText;
    public int muni = 1000;

    // Start is called before the first frame update
    void Start()
    {
        INSTANCE = this;
        moneyText.text = muni.ToString();
    }

    // Update is called once per frame
    void Update()
    {

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
