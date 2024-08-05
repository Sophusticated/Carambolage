using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;


public class Tilerotator : MonoBehaviour
{
    [SerializeField]
    private Tilemap _tilemap;

    [SerializeField]
    private TileBase _tile1;
    
    [SerializeField]
    private TileBase _tile2;

    public int currentTile;

    AudioManager audioManager;



    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        var switchPos = transform.position;
        var cellPos = _tilemap.WorldToCell(switchPos);
        currentTile = 1;
        _tilemap.SetTile(cellPos, _tile1);
    }


    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            audioManager.PlaySFX(audioManager.trackSwitch);
            var switchPos = transform.position;
            var cellPos = _tilemap.WorldToCell(switchPos);
            if (currentTile == 1)
            {
                _tilemap.SetTile(cellPos, _tile2);
                currentTile = 2;
            }
            else
            {
                _tilemap.SetTile(cellPos, _tile1);
                currentTile = 1;
            }
        }

        
    }
}
