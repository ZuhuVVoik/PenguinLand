using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static Messenger messenger;
    public static SceneChanger sceneChanger;
    static InventoryManager inventory;
    public static GameManager game;
    public static InventoryManager Inventory
    {
        get
        {
            if (inventory == null)
            {
                inventory = FindObjectOfType<InventoryManager>();
            }
            return inventory;
        }
        private set
        {
            inventory = value;
        }
    }

    /*Поднят ли именно банан*/
    public static bool IsBananaPickedUp = false;
    /*Текущие объект бананов*/
    public static GameObject CurrentBanana;

    /*Очки за мороженное*/
    public static int scores = 0;

    /*количество принесенных бананов*/
    public static int AllBananas = 4;
    private static int bananasCount = 0;
    public static int BananasCount
    {
        get { return bananasCount; }
        set
        {
            bananasCount = value;
            if(bananasCount == AllBananas)
            {
                game.WinGame();
            }
            else
            {
                if(bananasCount >= 1 && bananasCount < AllBananas)
                {
                    Messenger.WriteMessage("Вы собрали " + bananasCount + " из " + AllBananas + " бананов");
                }
            }
        }
    }

    /* Чтобы не прыгать в доме */
    public static bool inHouse = false;

    /* Обязательные предметы */
    public static bool HaveScarf = false;
    public static bool HaveBackpack = false;
    public static bool HaveHat = false;

    /* В теплом ли месте пингвин */
    public static bool IsInWarmPlace = true;

    /* Ссылка на контроллер температуры */
    public static Temperature Temperature;

    public static Messenger Messenger
    {
        get
        {
            if (messenger == null) // инициализация по запросу
            { messenger = FindObjectOfType<Messenger>(); }
            return messenger;
        }
        private set { messenger = value; }
    }
    private void OnEnable()
    {
        game = GetComponent<GameManager>();
        DontDestroyOnLoad(gameObject);
        Inventory = GetComponent<InventoryManager>();
        sceneChanger = GetComponent<SceneChanger>();
        Temperature = GetComponent<Temperature>();
    }
    public static void ClearData()
    {
        scores = 0;
        IsBananaPickedUp = false;
        HaveBackpack = false;
        HaveHat = false;
        HaveScarf = false;
        BananasCount = 0;
        IsInWarmPlace = true;
    }
}

