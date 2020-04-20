using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class RevenuePerMinute : MonoBehaviour
{

    public TMP_Text rpmText;


    public static RevenuePerMinute INSTANCE;


    public struct TimeToMoney {
        public float time;
        public int money;
    }
    public List<TimeToMoney> timeToMoney = new List<TimeToMoney>();


    void Awake() {
        INSTANCE = this;
    }

    void Start() {
        StartCoroutine(calculateRPM());
    }


    IEnumerator calculateRPM(){
        while(true) { 
            timeToMoney.RemoveAll(timeToMoney => timeToMoney.time < (TimePlayed.INSTANCE.timeSinceStartInSeconds - 60.0f));
            int sumOfMoney = timeToMoney.Sum(timeToMoney => timeToMoney.money);
            rpmText.text = ((float)sumOfMoney).ToString("0.00");
            yield return new WaitForSeconds(1f);
        }
    }

}

