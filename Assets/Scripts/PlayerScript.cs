using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Star;
    public GameObject Hammer;
    public TMP_Text CollectedStars;
    public TMP_Text TotalStars;
    public Animator PlayerAnimator;
    public bool isSwinging;
    public bool isHavingHammer = false;
    public Animation HammerAnimation;
    private int stars = 0;
    void Start()
    {
        Star = GameObject.FindGameObjectsWithTag("Collectible");
        TotalStars.text = stars.ToString() + "/" + Star.Length.ToString();
        Hammer = GameObject.FindGameObjectWithTag("Hammer");
        HammerAnimation.Stop();
        PlayerAnimator = GetComponent<Animator>();
        isSwinging = false;
        for(int i = 0; i < Star.Length; i++){
            Star[i].GetComponent<Transform>().transform.Rotate(Vector3.up * 50f * Time.deltaTime);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Z) && isHavingHammer){
            isSwinging = true;
            PlayerAnimator.Play("Swing");
            StartCoroutine(PlayAnimationForDuration("Swing", 2.00f));
            HammerAnimation.Play();
            isSwinging = false;
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Collectible"))
        {
            stars += 1;
            // CollectedStars.text = stars.ToString() + "/";
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Hammer"))
        {
            isHavingHammer = true;
             other.gameObject.transform.SetParent(gameObject.GetComponent<Transform>());

            other.gameObject.transform.localPosition = new Vector3(1.98000002f,8.0643549f,-2.9000001f);
            other.gameObject.transform.localRotation = Quaternion.identity;

        }
    }
    private IEnumerator PlayAnimationForDuration(string animName, float duration)
    {
        PlayerAnimator.Play(animName);
        yield return new WaitForSeconds(duration);
        PlayerAnimator.Play("Idle"); 
    }

}
