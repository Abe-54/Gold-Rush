using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TargetGameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allTargetGroups;
    [SerializeField] private List<GameObject> currentTargetList;

    public bool playerWins = false;
    public int numOfWins = 0;

    [SerializeField] private Tilemap roomWallsTileMap;
    [SerializeField] private Transform[] tileMapErasers;
    [SerializeField] private Image[] progressImages;
    [SerializeField] private Sprite completedImage;
    [SerializeField] private Sprite emptyImage;

    [SerializeField] private AudioSource targetHitSoundFX;
    
    public GameObject loadingZone;

    public int numberOfPhasesToComplete = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnableNewTargetGroup());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentTargetList.Count == 0 && !playerWins)
        {
            StartCoroutine(EnableNewTargetGroup());
            progressImages[numOfWins++].sprite = completedImage;
        }

        if (numOfWins != numberOfPhasesToComplete) return;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
            
        playerWins = true;

        foreach (var targetGroup in allTargetGroups)
        {
            targetGroup.SetActive(false);
        }
            
        foreach (var eraser in tileMapErasers)
        {
            var positionToErase = roomWallsTileMap.WorldToCell(eraser.position);
            
            roomWallsTileMap.SetTile(positionToErase, null);
        }
        
        roomWallsTileMap.GetComponent<CompositeCollider2D>().GenerateGeometry();

        loadingZone.SetActive(true);
    }

    private int GetRandomGroup()
    {
        return Random.Range(0, allTargetGroups.Count);
    }

    private IEnumerator EnableNewTargetGroup()
    {
        // Debug.Log("New Targets");
        int randomGroup = GetRandomGroup();
        
        allTargetGroups[randomGroup].SetActive(true);

        Target[] allVisibleTargets = allTargetGroups[randomGroup].GetComponentsInChildren<Target>(true);

        foreach (var target in allVisibleTargets)
        {
            target.gameObject.SetActive(true);
            target.hitTarget += hitTarget;
            currentTargetList.Add(target.gameObject);
            yield return new WaitForSeconds(1f);
        }

        // yield return new WaitForSeconds(0.15f);
    }

    private void hitTarget(GameObject target)
    {
        Debug.Log("Hit Target");
        targetHitSoundFX.Play();
        target.SetActive(false);
        currentTargetList.Remove(target);
    }
}
