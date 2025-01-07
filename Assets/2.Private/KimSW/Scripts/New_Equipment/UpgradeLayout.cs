using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLayout : MonoBehaviour
{
   [SerializeField] GridLayoutGroup gridLayout;
    [SerializeField] GameObject obj;

    [SerializeField] int cnt;

    private void Start()
    {
        gridLayout.cellSize = new Vector2(150 / cnt, 30);

        for (int i = 0; i < cnt; i++)
        {
            Instantiate(obj, transform);
        }
    }
}
