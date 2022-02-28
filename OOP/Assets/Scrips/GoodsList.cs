using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Shop/Goods List")]
public class GoodsList :ScriptableObject
{
   [SerializeField] private List<Good> _goods; 
   public IEnumerable<Good> Goods => _goods;
}
