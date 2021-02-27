using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTileEffectHandlerScript : MonoBehaviour
{
    [SerializeField] Renderer tileRender;
    [SerializeField] Material tileMaterial;
    [SerializeField] GameObject selectEffectGroup;

    private void Awake()
    {
        tileMaterial = tileRender.material;
    }

    public void ToggleEffect_On()
    {
        if (tileRender != null)
        {
            tileMaterial.SetInt("IsOn", 1);
        }
    }

    public void ToggleEffect_Off()
    {
        if (tileRender != null)
        {
            tileMaterial.SetInt("IsOn", 0);
        }
    }

    public void SelectTile()
    {
        if (selectEffectGroup != null)
        {
            selectEffectGroup.SetActive(true);
        }
    }

    public void DeselectTile()
    {
        if (selectEffectGroup != null)
        {
            selectEffectGroup.SetActive(false);
        }
    }
}
