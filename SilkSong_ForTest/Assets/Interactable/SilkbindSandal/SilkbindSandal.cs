using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SilkbindSandal : MonoBehaviour
{
    private bool used;
    private WorldData worldState;
    //public float tutorialMinTime = 3f;
    //public GameObject tutorial;
    //public GameObject promt;
    private InputMaster inputMaster;
    //private AudioSource sound;
    //private CanvasGroup canvasGroup;
    //public float fadeSpeed = 0.3f;
    public GameObject interactText;
    private bool canPick;

    private void Awake()
    {
        worldState = GameMaster.instance.worldData;
        if (worldState.doubleJump)
            Destroy(this.gameObject);
    }

    private void Start()
    {
        inputMaster = new InputMaster();
        inputMaster.Enable();
        interactText.SetActive(false);
        canPick = false;
        //sound = GetComponent<AudioSource>();
        //canvasGroup = tutorial.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        HandlePick();
    }

    private void HandlePick()
    {
        GameObject hero = GameObject.Find("Tenroh").gameObject;
        Player player = hero.GetComponent<Player>();
        bool pressUp = inputMaster.Gameplay.Attack.WasPressedThisFrame();
        if (player && !used  && pressUp && canPick)
        {
            used = true;
            worldState.doubleJump = true;
            GameMaster.instance.playerData.inventoryItemAmount[(int)InventoryItem.ItemName.silkbindSandal] += 1;
            player.playerStat.extraJump += 1;
            //StartCoroutine(DoubleJumpTutorial(player));
            canPick = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p)
        {
            canPick = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player p = other.GetComponent<Player>();
        if (p)
        {
            canPick = false;
            interactText.SetActive(false);
        }
    }

    // private IEnumerator DoubleJumpTutorial(Player player)
    // {
    //     canvasGroup.alpha = 0;
    //     player.disableControlCounter += 1;
    //     promt.SetActive(false);
    //     tutorial.SetActive(true);
    //     sound.Play();
    //
    //     // Fade in tutorial
    //     while (canvasGroup.alpha < 1)
    //     {
    //         canvasGroup.alpha += fadeSpeed * Time.deltaTime;
    //         yield return null;
    //     }
    //
    //     yield return new WaitForSeconds(tutorialMinTime);
    //     promt.SetActive(true);
    //     
    //     // Fade out
    //     while (canvasGroup.alpha > 0)
    //     {
    //         canvasGroup.alpha -= fadeSpeed * Time.smoothDeltaTime;
    //         yield return null;
    //     }
    //
    //     tutorial.SetActive(false);
    //     player.disableControlCounter -= 1;
    //     StopAllCoroutines();
    //     Destroy(this.gameObject);
    // }
}
