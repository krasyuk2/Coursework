using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlockOrTurret : ShopItem
{
    public GameObject createPrefabControllerBlock;
    public GameObject UISetAndCancel;
    public override void OnClickBuy()
    {
        UISetAndCancel.SetActive(true);
        Instantiate(createPrefabControllerBlock);
    }
}
