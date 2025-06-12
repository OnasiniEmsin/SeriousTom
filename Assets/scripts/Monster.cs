using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public Transform playertransform;
    public int distanceOfView=10,moveSpeed;
    public float hitPoint=10;
    public Image hBar;
    public GameObject audio;
    public GameObject[] bonuses;
    
    Rigidbody2D rb;
    Vector2 dir;
    float maxHP;
    PlayerController2d pc;
    Score score;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        playertransform=GameObject.Find("player").transform;
        score=GameObject.Find("Canvas/Score").GetComponent<Score>();
        maxHP=hitPoint;
        pc=playertransform.GetComponent<PlayerController2d>();
    }

    // Update is called once per frame
    void Update()
    {
        dir=-transform.position+playertransform.position;
        if(dir.magnitude<=2){
            pc.setAutoFire(transform);
        }else if(dir.magnitude<=distanceOfView){
            animator.SetBool("walking",true);
            goingToPlayer();
        }else{
            animator.SetBool("walking",false);
        }

    }
    void goingToPlayer(){
        audio.active=true;
        rb.velocity = dir.normalized*moveSpeed;
        transform.localScale=new Vector2(2.5f*Mathf.Sign(dir.x)  ,2.5f);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("patronTag"))
        {
            hitPoint--;
            hBar.fillAmount=hitPoint/maxHP;
            if(hitPoint<=0){
                umer();
            }
            Destroy(other.gameObject);

        }
        if (other.CompareTag("Player")){
            pc.getDamage(10);
        }
    }
    void umer(){
        score.setScore(35);
        pc.notAutoFire();
        Destroy(gameObject);
        Instantiate(bonuses[Random.Range(0,bonuses.Length)],transform.position,Quaternion.identity);
    }
}
