using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLayout : MonoBehaviour
{
   [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] GameObject obj;

    List<GameObject> fills = new List<GameObject>();

    [SerializeField] float cellSizeX;

    [SerializeField] Color onColor;
    [SerializeField] Color offColor;

 

    public void SetLayout(int level)
    {
        gridLayout.cellSize = new Vector2(cellSizeX / level, 30);

        for (int i = 0; i < level; i++)
        {
            fills.Add(Instantiate(obj, transform));
            
        }

        foreach (GameObject obj in fills)
        {
            obj.GetComponent<Image>().color = offColor;
        }
    }

    public void ChangeFill(int level)
    {
       

        
        for (int i = 0; i < level; i++)
        {
            fills[i].GetComponent<Image>().color = onColor;
        }
    }

    public void RemoveFill()
    {
        foreach (GameObject obj in fills)
        {
            Destroy(obj);
        }

        fills.Clear();
    }
}
