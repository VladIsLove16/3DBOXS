using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : LootNode
{
    private List<GameObject> _items=new List<GameObject>();
    public void Open()
    {
        _items.ForEach(x =>x.SetActive(true));


    }
    protected override void PopulateItem(GameObject item)
    {
        base.PopulateItem(item);
        item.SetActive(false);
    }
}
