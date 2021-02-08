using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Inventory; // ссылка на панель с инвентарём

    [SerializeField]
    UIObject[] objects; //массив элементов UI, отображающих предметы
    private void Start()
    {
        Inventory.SetActive(false); // скрываем инвентарь
        if (MainManager.HaveBackpack)
        {
            objects[0].State = true;
        }
        if (MainManager.HaveScarf)
        {
            objects[1].State = true;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // отслеживаем нажатие клавиши “I”
        {
            Inventory.SetActive(!Inventory.activeSelf); // инвертируем состояние инвентаря
            if (Inventory.activeSelf) UpdateUI();
            // обновляем предметы в инвентаре, если инвентарь открытый
        }
        if (MainManager.HaveBackpack)
        {
            objects[0].State = true;
            if (Inventory.activeSelf) UpdateUI();
        }
        if (MainManager.HaveScarf)
        {
            objects[1].State = true;
            if (Inventory.activeSelf) UpdateUI();
        }
    }
    public void AddItem(GameObject objectInScene)
    // публичный метод для добавления объекта в инвентарь
    {
        foreach (UIObject obj in objects) // обходим массив UI объектов
        {
            if (objectInScene.Equals(obj.myObject()))
            // если имя подобранного объекта совпадаем с именем одного из объектов в массиве
            {
                obj.State = true; // отмечаем объект в массиве как активный (подобранный)
                break; // выходим из цикла, если нашли подходящий объект
            }
        }
        if (Inventory.activeSelf) UpdateUI();
        // если после добавления элемента инвентарь был открыт - обновляем его
    }
    void UpdateUI() // метод обновления инвентаря
    {
        foreach (UIObject obj in objects) // обходим массив объектов
        {
            obj.UpdateImage(); // обновляем каждый из объектов
        }
    }

}
