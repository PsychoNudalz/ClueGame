using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This handles the effects and shader on the tile
/// </summary>
public class BoardTileEffectHandlerScript : MonoBehaviour
{
    [SerializeField] Renderer tileRender;
    [SerializeField] Material tileMaterial;
    [SerializeField] GameObject selectEffectGroup;

    private void Awake()
    {
        tileMaterial = tileRender.material;
    }

    /// <summary>
    /// turn on the effect for when player can move to that tile
    /// </summary>
    public void ToggleEffect_On()
    {
        if (tileRender != null)
        {
            tileMaterial.SetInt("IsOn", 1);
        }
    }

    /// <summary>
    /// turn off the effect for when player can move to that tile
    /// </summary>
    public void ToggleEffect_Off()
    {
        if (tileRender != null)
        {
            tileMaterial.SetInt("IsOn", 0);
        }
    }

    /// <summary>
    /// display the glow box effect for when the cursor hovering on it
    /// </summary>
    public void SelectTile()
    {
        if (selectEffectGroup != null)
        {
            selectEffectGroup.SetActive(true);
        }
    }
    /// <summary>
    /// disable the glow box effect for when the cursor hovering on it
    /// </summary>
    public void DeselectTile()
    {
        if (selectEffectGroup != null)
        {
            selectEffectGroup.SetActive(false);
        }
    }
}
