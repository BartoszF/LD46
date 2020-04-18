using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{

    public int muni = 1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool spendMuniIfPossible(int spending) {
        if (spending <= muni) {
            muni -= spending;
            return true;   
        }
        return false;
    }

    public void addMuni(int muniToAdd) {
        muni += muniToAdd;
    }
}
