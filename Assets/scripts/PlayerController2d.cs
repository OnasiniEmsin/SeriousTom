using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController2d : MonoBehaviour
{
    public float moveSpeed = 5f,bulletForce=10,hitPoint=100;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public FixedJoystick joystick;//Для управления
    public GameObject bulletPrefab;//префаб патрона
    public Transform firePoint;//точка для появления стрелки.
    public GameObject[] Veapons; //оружия на руке игрока
    public Image hBar;//полоса здаровия
    public AudioSource ak;//звук ак47
    bool isFiring,
            isHaveBullets,
            autoFire,
            ready=true;

    Buttons moyaOrujiya;//переменная для оружия в руке игрока
    Transform enemy;//переменная для ближайшего врага
    int koeffitsienDvijeniya=1, //переменная для игрока, чтобы посмотреть влево(-1) или вправо(1)
            maxHP;//знаменатель, чтобы найти значение healtbar.
    float perezaryatkaMAgazina=.15f;//время между каждым выстрелом
    Animator animator;
    void Start()
    {
        animator=GetComponent<Animator>();
        maxHP=(int)hitPoint;
        rb = GetComponent<Rigidbody2D>();
    }
    float moveX;
    float moveY;
    void Update()
    {
        // Input olish
        moveX = joystick.Horizontal; 
        moveY = joystick.Vertical; 
        isWalking();
        if(autoFire==false){ //Автоматический огонь включается, когда враг находится слишком близко.
            if(joystick.Horizontal>0){
                koeffitsienDvijeniya=1;
            }
            if(joystick.Horizontal<0){
                koeffitsienDvijeniya=-1;
            }
            
            moveInput = new Vector2(moveX, moveY).normalized;
            transform.localScale=new Vector2(2.5f*koeffitsienDvijeniya  ,2.5f);
            if(isFiring&&isHaveBullets&&ready){
                fireF();
            }
        }else{
            koeffitsienDvijeniya=(int) Mathf.Sign((enemy.position-transform.position).x);В режиме автоматического огня игрок движется только лицом к монстру.
            moveInput = new Vector2(moveX, moveY).normalized;
            transform.localScale=new Vector2(2.5f*koeffitsienDvijeniya  ,2.5f);
            if(isHaveBullets&&ready){
                fireF();
            }
            koeffitsienDvijeniya=1;
        }
        
        
        
    }
    void isWalking(){
        if(moveX!=0||moveY!=0){
            animator.SetBool("walking",true);
        }else{
            animator.SetBool("walking",false);
        }
    }

    void FixedUpdate()
    {
        // Harakatni qo‘llash
        rb.velocity = moveInput * moveSpeed;
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(koeffitsienDvijeniya* firePoint.right * bulletForce, ForceMode2D.Impulse);
        moyaOrujiya.minus1();
        ready=false;
        ak.Play();
        StartCoroutine(timerPerez());
    }
    public void setAcFire(){
        isFiring=true;
    }
    public void unAcFire(){
        isFiring=false;
    }
    void fireF(){//fire function
        Shoot();
    }
    public void setVeapon(Buttons b){
        moyaOrujiya=b;
        for(int i=0;i<Veapons.Length;i++){
            if(i==b.getNumber()){
                Veapons[i].active=true;
            }else{
                Veapons[i].active=false;
            }
        }
    }
    public void notHaveBullets(){
        isHaveBullets=false;
    }
    public void HaveBullets(){
        isHaveBullets=true;
    }
    public void setAutoFire(Transform t){
        enemy=t;
        autoFire=true;
    }
    public void notAutoFire(){
        autoFire=false;
    }
    public void getDamage(int damage){
        hitPoint-=damage;
        hBar.fillAmount=hitPoint/maxHP;
        if(hitPoint<=0){
            Destroy(gameObject);
        }
    }
    IEnumerator timerPerez(){
        yield return new WaitForSeconds(perezaryatkaMAgazina);
        ready=true;
    }
}