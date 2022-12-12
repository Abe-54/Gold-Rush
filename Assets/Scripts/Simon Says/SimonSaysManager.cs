using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SimonSaysManager : MonoBehaviour
{
    [Header("Tile Light Settings")]
    [Range(0.1f, 3f)] public float tileLitUp = 1.5f;
    [Range(0.1f, 3f)] public float totalFlashDuration = 1f;

    [Header("Tile Light Game Objects")]
    public List<string> visibleColorSequence;
    public Light2D[] _tileLights = new Light2D[4];
    public Light2D[] _UILight = new Light2D[4];
    public GameObject loadingZone;

    [Header("Tile Light Game Sequence")] 
    public bool playerWins = false;
    public int numOfWins = 0;
    [SerializeField] private string currentColor;
    [SerializeField] private bool showingSequence;
    [SerializeField] private Tilemap roomWallsTileMap;
    [SerializeField] private Transform[] tileMapErasers;
    [SerializeField] private Image[] progressImages;
    [SerializeField] private Sprite completedImage;
    [SerializeField] private Sprite emptyImage;

    [Header("Miscellaneous")] 
    [SerializeField] private AudioSource enteredColoredTileSoundFX;

    private Queue<string> currentColorSequence;
    private Queue<Light2D> tileLightsSequence;
    private Queue<Light2D> UILightsSequence;
    private ColoredTile[] coloredTiles;
    private float flashDuration;
    
    private readonly string[] _colors = new[] { "Blue", "Red", "Yellow", "Green"};

    // Start is called before the first frame update
    void Start()
    {
        flashDuration = totalFlashDuration / 2f;
        loadingZone.SetActive(false);
        
        coloredTiles = FindObjectsOfType<ColoredTile>();

        tileLightsSequence = new Queue<Light2D>();
        UILightsSequence = new Queue<Light2D>();

        foreach (var coloredTile in coloredTiles)
        {
            coloredTile.EnteredColoredTile += EnteredColoredTile;
            coloredTile.ExitedColoredTile += ExitedColoredTile;
        }

        StartCoroutine(NewSequence());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentColorSequence == null) return;

        visibleColorSequence = new List<string>(currentColorSequence);
        
        if (currentColorSequence.Count == 0 && !playerWins)
        {
            StartCoroutine(NewSequence());
            progressImages[numOfWins++].sprite = completedImage; 
            Debug.Log("New Sequence Called");
        }

        if (numOfWins == 3)
        {
            playerWins = true;
            StartCoroutine(WinSequence());
            loadingZone.SetActive(true);
            Debug.Log("You Win!");
        }
    }

    IEnumerator ShowSequence()
    {
        showingSequence = true;
        
        while (tileLightsSequence.Count > 0)
        {
            Light2D currentLight = tileLightsSequence.Dequeue();
            Light2D currentUiLight = UILightsSequence.Dequeue();

            Sequence flashLight = DOTween.Sequence().SetAutoKill(false).SetId("FlashLights");

            flashLight.Append(DOTween.To(() => currentLight.intensity, value => currentLight.intensity = value, tileLitUp, flashDuration))
                .Join(DOTween.To(() => currentUiLight.intensity, value => currentUiLight.intensity = value, tileLitUp, flashDuration))
                .Append(DOTween.To(() => currentLight.intensity, value => currentLight.intensity = value, 0f, flashDuration))
                .Join(DOTween.To(() => currentUiLight.intensity, value => currentUiLight.intensity = value, 0f, flashDuration));

            yield return flashLight.WaitForCompletion();
        }

        showingSequence = false;
    }

    IEnumerator WrongSequence()
    {
        numOfWins = 0;
        showingSequence = true;

        foreach (var img in progressImages)
        {
            img.sprite = emptyImage;
        }

        foreach (var light2D in _tileLights)
        {
            light2D.intensity = 0f;
        }

        Sequence flashAllLights = DOTween.Sequence().SetAutoKill(false);

        for (int i = 0; i <= 1; i++)
        {
            Debug.Log("FLASHING ALL LIGHTS");
            foreach (var light2D in _tileLights)
            {
                flashAllLights.Join(DOTween.To(() => light2D.intensity, value => light2D.intensity = value, tileLitUp, flashDuration));
            }

            flashAllLights.AppendInterval(flashDuration);
        
            foreach (var light2D in _tileLights)
            {
                flashAllLights.Join(DOTween.To(() => light2D.intensity, value => light2D.intensity = value, 0f, flashDuration));
            }
            flashAllLights.AppendInterval(flashDuration);
        }
        
        yield return flashAllLights.WaitForCompletion();
        
        currentColorSequence = RandomColorSequence();
        currentColor = currentColorSequence.Peek();
        
        StartCoroutine(ShowSequence());
    }

    IEnumerator NewSequence()
    {
        currentColorSequence = RandomColorSequence();
        currentColor = currentColorSequence.Peek();
        
        yield return new WaitForSeconds(0.3f);

        StartCoroutine(ShowSequence());
    }

    IEnumerator WinSequence()
    {
        DOTween.Kill("FlashLights");
        
        Sequence allLightOn = DOTween.Sequence().SetAutoKill(false);

        for (int i = 0; i <= 1; i++)
        {
            Debug.Log("FLASHING ALL LIGHTS");
            foreach (var light2D in _tileLights)
            {
                allLightOn.Join(DOTween.To(() => light2D.intensity, value => light2D.intensity = value, tileLitUp, flashDuration));
            }

            allLightOn.AppendInterval(flashDuration);
        }
        
        yield return allLightOn.WaitForCompletion();

        foreach (var eraser in tileMapErasers)
        {
            var positionToErase = roomWallsTileMap.WorldToCell(eraser.position);
            
           roomWallsTileMap.SetTile(positionToErase, null);
        }
    }
    
    Queue<string> RandomColorSequence()
    {
        Queue<string> colorSequence = new Queue<string>();

        /*
         * Blue = 0
         * Red = 1
         * Yellow = 2
         * Green = 3
         */

        while (colorSequence.Count < 4)
        {
            int randomNum = Random.Range(0, 4);
            
            if (!colorSequence.Contains(_colors[randomNum]))
            {
                colorSequence.Enqueue(_colors[randomNum]);
                tileLightsSequence.Enqueue(_tileLights[randomNum]);
                UILightsSequence.Enqueue(_UILight[randomNum]);
            }
        }

        return colorSequence;
    }
    
    void EnteredColoredTile(string color, Light2D light2D)
    {
        if (showingSequence) return;

        enteredColoredTileSoundFX.Play();
        
        light2D.intensity = tileLitUp;
    }
    
    void ExitedColoredTile(string color, Light2D light2D)
    {
        if (showingSequence) return;
        
        light2D.intensity = 0f;
        
        if (currentColor.CompareTo(color) == 0)
        {
            currentColorSequence.Dequeue();
            
            if(currentColorSequence.Count == 0) return;

            currentColor = currentColorSequence.Peek();
        }
        else
        {
            StartCoroutine(WrongSequence());
        }
    }
    
    void printColorSequence<T>(Queue<T> sequence)
    {
        String output = "";
        foreach (var color in sequence)
        {
            output += color + ", ";
        }

        Debug.Log(output);
    }
}
