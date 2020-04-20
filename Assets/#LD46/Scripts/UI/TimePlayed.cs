using UnityEngine;
using System;
using TMPro;

public class TimePlayed : MonoBehaviour
{

    public float timeSinceStartInSeconds = 0.0f;

    public TMP_Text moneyText;


    public static TimePlayed INSTANCE;

    void Awake() {
        INSTANCE = this;
    }

    void FixedUpdate() {
            timeSinceStartInSeconds += Time.fixedDeltaTime;
            TimeSpan t = TimeSpan.FromSeconds(timeSinceStartInSeconds);
            moneyText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", 
                            t.Hours, 
                            t.Minutes, 
                            t.Seconds);
    }


}