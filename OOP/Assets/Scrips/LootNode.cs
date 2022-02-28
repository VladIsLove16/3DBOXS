using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootNode : MonoBehaviour
{
    [SerializeField] private int _2chance;
     [SerializeField] private int _1chance;

    void Start()
    {
        var item = GetRandomItem();
        PopulateItem(item);
        
    }
    protected virtual void PopulateItem(GameObject item)
        {
           item.transform.parent=item.transform;//помещаем под себя?
           item.transform.localPosition=Vector3.zero;//оцентровка
        }
    protected GameObject GetRandomItem()
    {
        return new GameObject("Item"+ Random.Range(0,1000));
    }
}
