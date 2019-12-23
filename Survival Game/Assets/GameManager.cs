using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public Canvas[] canvas;
    public GameObject player;

    public bool isCursorLocked;
    public bool isInventory;
    public bool isKeyEnabled;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Time.timeScale);

        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].enabled = false;
        }

        isCursorLocked = true;

        SetCursorLocked();

        isKeyEnabled = true;
    }

    public void Resume()
    {
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].enabled = false;
        }

        isKeyEnabled = true;

        UnlockPlayer();

        yield return new WaitForSeconds(0.05f);

        isCursorLocked = true;
        SetCursorLocked();
    }

    void UnlockPlayer()
    {
        player.GetComponent<FirstPersonController>().enabled = true;
    }

    void LockPlayer()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void SetInventory()
    {
        if (!isInventory)
        {
            for (int i = 0; i < canvas.Length; i++)
            {
                canvas[i].enabled = false;
                canvas[2].enabled = true;
            }

            LockPlayer();

            isCursorLocked = false;
            SetCursorLocked();

            isInventory = true;
        }
        else if (isInventory)
        {
            for (int i = 0; i < canvas.Length; i++)
            {
                canvas[i].enabled = false;
                //canvas[2].enabled = true;
            }

            UnlockPlayer();

            isCursorLocked = true;
            SetCursorLocked();

            isInventory = false;
        }
    }
    void SetCursorLocked()
    {
        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isKeyEnabled)
        {
            LockPlayer();

            for (int i = 0; i < canvas.Length; i++)
            {
                canvas[i].enabled = false;
                canvas[1].enabled = true;
            }

            Debug.Log(Time.timeScale);

            isCursorLocked = false;
            SetCursorLocked();

            isKeyEnabled = false;
        }

        if (Input.GetKeyDown(KeyCode.I) && !isInventory && isKeyEnabled)
        {
            // pops the inventory canvas
            SetInventory();

            isKeyEnabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isInventory)
        {
            SetInventory();

            isKeyEnabled = true;
        }
    }
}
