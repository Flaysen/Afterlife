﻿using Afterlife.Assets.WorldGeneration.Scripts;
using Maze;
using UnityEngine;

public class RoomInteriorGenerator : MonoBehaviour
{
    [SerializeField] private Texture2D _pattern;
    [SerializeField] private Texture2D[] _patterns;
    [SerializeField] private ObjectFromPattern[] _objectFromPattern;
    [SerializeField] private Color32 _wallsH;
    [SerializeField] private Color32 _wallsV;
    private RoomController _roomController;

    private void Awake()
    {
        _roomController = GetComponentInParent<RoomController>();
    }

    private void Start()
    {
        _pattern = _patterns[UnityEngine.Random.Range(0, _patterns.Length)];
        
        if(_roomController.RoomType == RoomType.COMMON)
        {
            GenerateRoomTiles(RandomPatternOrientation(_pattern));
        } 
    }
    private void GenerateRoomTiles(Texture2D pattern)
    {
        for (int x = 0; x < pattern.width; x++)
        {
            for (int y = 0; y < pattern.height; y++)
            {
                GenerateTile(x, y, pattern);
            }
        }
    }
    private void GenerateTile(int x, int y, Texture2D pattern)
    {
        Color pixelColor = pattern.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            return;
        }
     
        foreach (ObjectFromPattern mapping in _objectFromPattern)
        {           
            if (mapping.color.Equals(pixelColor))
            {
                Vector3 spawnPos = PositionFromTileGrid(x, y, pattern);
                GameObject objectToSpawn = mapping.prefabs[UnityEngine.Random.Range(0, mapping.prefabs.Length)];
                var a = Instantiate(objectToSpawn, spawnPos + transform.position,
                    objectToSpawn.transform.rotation, transform);
            }
        }
    }
    private Vector3 PositionFromTileGrid(int x, int y, Texture2D pattern)
    {
        int w = pattern.width;
        int h = pattern.height;

        return new Vector3(x - (w - 1)/2, 0, y - (h - 1)/2);
    }
    private Texture2D RandomPatternOrientation(Texture2D originalPattern)
    {
        int x = UnityEngine.Random.Range(0,5);

        switch (x)
        {
            case 0:
                break;
            case 1:
                originalPattern = RotateTexture(originalPattern, true);
                break;
            case 2:
                originalPattern = RotateTexture(originalPattern, false);
                break;

            case 3:
                originalPattern = TurnTexture(originalPattern, true);
                break;
            case 4:
                originalPattern = TurnTexture(originalPattern, false);
                break;

        }
        return originalPattern;
    }
    private Texture2D RotateTexture(Texture2D originalTexture, bool clockwise)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        for(int i = 0; i < original.Length; i++)
        {
            if (original[i].Equals(_wallsH))
            {
                original[i] = _wallsV;
            }
            else if (original[i].Equals(_wallsV))
            {
                original[i] = _wallsH;
            }
        }
        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }
        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();

        return rotatedTexture;
    }

    private Texture2D TurnTexture(Texture2D originalTexture, bool horizontal)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] turned = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iTurned, iOriginal;

        for(int j = 0; j < h; j++)
        {
            for (int i = 0; i < w; i++)
            {              
                iTurned = i + w * j;
                iOriginal = (horizontal == true) ? (w - 1) * h + i - j * h : w - 1 - i + j * w ;
                turned[iTurned] = original[iOriginal];
            }
        }

        Texture2D turnedTexture = new Texture2D(h, w);
        turnedTexture.SetPixels32(turned);
        turnedTexture.Apply();

        return turnedTexture;
    }
}
