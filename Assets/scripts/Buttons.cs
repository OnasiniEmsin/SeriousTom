using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Buttons : MonoBehaviour
{
    public GameObject deleteButton;
    public int patrony=30,id=3003;
    public TMP_Text textOfBullets;
    InventoryUI iUI;
    int _myNumber=0;
    // Start is called before the first frame update
    void Start()
    {
        textOfBullets.text=patrony.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selected(){
        deleteButton.active=true;
        iUI.selectVeapon(_myNumber);
    }
    public void unSelected(){
        deleteButton.active=false;
    }
    public int getNumber(){
        return _myNumber;
    }
    public void setNumber(int i){
        _myNumber=i;
    }
    public void setInventory(InventoryUI iui){
        iUI=iui;
    }
    public void deleteMe(){
        iUI.deleteTheButton(this);
        Destroy(gameObject);
    }
    public void minus1(){
        patrony--;
        textOfBullets.text=patrony.ToString();
        if(patrony<=0){
            deleteMe();
        }
    }
}
