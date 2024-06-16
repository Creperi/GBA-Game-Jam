using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Star;
    void Start()
    {
        Star = GameObject.FindGameObjectsWithTag("Collectible");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
        }
    }
}
