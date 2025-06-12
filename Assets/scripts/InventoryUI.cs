using UnityEngine;using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] slotPrefabs;//Это бета версия, можно изменит позже
    public int slotCount = 2;//место для предмета
    public Transform slotParent;
    public PlayerController2d pc;
    public List<Buttons> knopki=new List<Buttons>();
    GameObject slotPrefab;

    int myVeaponNumber=0;
    void Start()
    {
        Application.targetFrameRate = 60;
        for (int i = 0; i < slotCount; i++)
        {
            addBattn(i,false);
        }
    }

    public void chooseButton()
    {
        foreach (var slot in knopki)
        {
            if (slot.getNumber()!= myVeaponNumber)
            {
                slot.unSelected();
            }else{
                pc.setVeapon(slot);
                pc.HaveBullets();
            }
        }
    }
    public void selectVeapon(int i){
        myVeaponNumber=i;
        chooseButton();
    }
    public void deleteTheButton(Buttons b){
        knopki.Remove(b);
        pc.notHaveBullets();
        foreach (var slot in knopki)
        {
            slot.selected();
            pc.HaveBullets();
        }
    }
    public void addBullet(int index,int bulls){ 
        if(index==0){  //Если индекс равен 0, то все оружие будет снабжено боеприпасами.
                foreach(var slot in knopki){   slot.patrony+=bulls+1;      slot.minus1();  }//но для этого должно быть какое-то оружие
        }else{
            bool isHave=false;
                foreach(var slot in knopki){
                    if(slot.id==slotPrefabs[index-1].GetComponent<Buttons>().id){//Здесь проводится проверка изъятого оружия.
                        slot.patrony+=bulls+1;
                        slot.minus1();
                        isHave=true;
                    }
                } 
            if(isHave==false) 
            {
                addBattn(index-1,true);//Если приобретенного оружия не существует, будет добавлено новое.
            }
        }
    }
    void addBattn(int i,bool isnew){//Add Button => добавляет новую кнопку
        slotPrefab=slotPrefabs[i];
        Buttons battn = Instantiate(slotPrefab, slotParent).GetComponent<Buttons>();
        battn.setNumber(i);
        knopki.Add(battn);
        battn.setInventory(this);
        if(i==slotCount-1){
            battn.selected();
        }
        if(isnew){
            battn.extraAmmo();
        }
    }
    
}