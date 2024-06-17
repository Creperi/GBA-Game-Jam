using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Star;
    public TMP_Text CollectedStars;
    public TMP_Text TotalStars;
    private int stars = 0;
    void Start()
    {
        Star = GameObject.FindGameObjectsWithTag("Collectible");
        TotalStars.text = Star.Length.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Collectible"))
        {
            stars += 1;
            CollectedStars.text = stars.ToString();
            Destroy(other.gameObject);
        }
    }
}
