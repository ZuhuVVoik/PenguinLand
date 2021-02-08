using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    /* Класс для работы с температурой пингвина 
        Тут опеределно, насколько сильно влияют предметы и костры на температуру, 
        а также сила мороза
     */


    int maxTemperature = 1000;    //Начальные очки температуры,
                                    //Температура не в градусах, а в очках, как здоровье в играх
    int currentTemperature;       //Текушая температура
    int CurrentTemperature        //Используем свойства чтобы быстро реагировать на изменения
    {
        get { return currentTemperature; }
        set
        {
            currentTemperature = value;
            if(currentTemperature > maxTemperature) // не больше максимальной
            {
                CurrentTemperature = maxTemperature;
            }
            if (currentTemperature <= 700 && warnings == 0) // начинает мерзнуть
            {
                MainManager.Messenger.WriteMessage("Вы начинаете замерзать");
            }
            if (currentTemperature <= 500 && warnings == 1) 
            {
                MainManager.Messenger.WriteMessage("Вы серьезно замерзать");
            }
            if(currentTemperature <= 200 && warnings == 3)
            {
                MainManager.Messenger.WriteMessage("Вы сильно замерзли, подойдите к костру");
            }
            if (currentTemperature <= 0) // если замерз
            {
                MainManager.game.LoseGame();
            }
        }
    }
    int warnings = 0;

    int fireStrength = 30;        //Сила обогрева от костра, в очках температуры

    /* Изменения от предметов */
    int hatChange = 3;     //Для шапки
    int scarfChange = 2;   //Для шарфика

    int currentChange = 10;       //Изменение от мороза

    // Start is called before the first frame update
    void Start()
    {
        CurrentTemperature = maxTemperature;
        InvokeRepeating("ChangeTemperature", 1f, 1f);  //1s delay, repeat every 1s
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeTemperature()
    {
        if (MainManager.IsInWarmPlace) // Если в теплом месте
        {
            FireWarming();
        }
        else
        {
            FrostHit();
        }
    }
    void FrostHit()    // убираем нужно кол-во очков температуры
    {
        CurrentTemperature -= currentChange;
    }
    public void CalculateMultiplier(string itemName) // Посчитаем множитель влияния температуры
    {
        if (itemName == "Hat")
        {
            currentChange -= hatChange;
        }

        if (itemName == "Scarf")
        {
            currentChange -= scarfChange;
        }

        
    }

    void FireWarming()
    {
        currentTemperature += fireStrength;
    }
}
