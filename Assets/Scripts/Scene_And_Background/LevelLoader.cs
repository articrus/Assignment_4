using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 08-12-2024
 * This script is used to change levels/scenes
 */
public class LevelLoader : MonoBehaviour
{
    public int lvlSceneNum;
    public Animator anim;
    public float waitTime = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log($"Switching to scene: {lvlSceneNum}");
            LoadNextLvl(lvlSceneNum);
        }
    }

    public void LoadNextLvl(int lvlSceneNum)
    {
        StartCoroutine(LoadLvl(lvlSceneNum));
    }

    IEnumerator LoadLvl(int lvlIndex)
    {
        anim.SetTrigger(AnimationStrings.sceneStart);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(lvlIndex, LoadSceneMode.Single);
    }
}