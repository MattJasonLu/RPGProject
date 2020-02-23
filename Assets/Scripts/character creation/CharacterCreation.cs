using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public UIInput nameInput; // 用来得到输入文本
    private GameObject[] characterGameObjects;
    private int selectedIndex = 0;
    private int length; // 所有可供选择的角色的个数

    // Start is called before the first frame update
    void Start()
    {
        length = characterPrefabs.Length;
        characterGameObjects = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            characterGameObjects[i] = Instantiate(characterPrefabs[i], transform.position, transform.rotation);
        }
        UpdateCharacterShow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCharacterShow()
    {
        characterGameObjects[selectedIndex].SetActive(true);
        for (int i = 0; i < length; i++)
        {
            if (i != selectedIndex)
            {
                characterGameObjects[i].SetActive(false);
            }
        }
    }

    public void OnNextBtnClick()
    {
        selectedIndex++;
        selectedIndex %= length;
        UpdateCharacterShow();
    }

    public void OnPrevBtnClick()
    {
        selectedIndex--;
        if (selectedIndex == -1)
        {
            selectedIndex = length - 1;
        }
        UpdateCharacterShow();
    }

    public void OnOkBtnClick()
    {
        // 存储选择的角色
        PlayerPrefs.SetInt("selectedCharacterIndex", selectedIndex); 
        PlayerPrefs.SetString("name", nameInput.value);
        // 加载下一个场景
        SceneManager.LoadScene(2);
    }
}
