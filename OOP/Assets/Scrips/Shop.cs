
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Shop : MonoBehaviour 
{    public GoodsList Goods;
 private void Start() {
   
    foreach(var good in Goods.Goods)
    Debug.Log (good.name);
    //Goods.Goods.First().SomeState = 15;
}

}