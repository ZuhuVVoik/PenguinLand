using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void OpenNewScene() // метод для смены сцены
    {
        MainManager.inHouse = false;
        int index = SceneManager.GetActiveScene().buildIndex; // берем индекс запущенной сцены
        if (index == 0)
        {
            index = 2; // меняем индекс с 0 на 1 или с 1 на 0

        }
        else
        {
            index = 0;
            MainManager.ClearData();
        }
        StartCoroutine(AsyncLoad(index)); // запускаем асинхронную загрузку сцены
        MainManager.IsInWarmPlace = true;
    }
    public void OpenOutsideScene()
    {
        MainManager.inHouse = false;
        StartCoroutine(AsyncLoad(1));
        MainManager.IsInWarmPlace = false;
    }
    IEnumerator AsyncLoad(int index)
    {
        AsyncOperation ready = null;
        ready = SceneManager.LoadSceneAsync(index);
        while (!ready.isDone) // пока сцена не загрузилась
        {
            yield return null; // ждём следующий кадр
        }
    }

}
