using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkService : MonoBehaviour
{
    Material maintexture; // ссылка на текстуру экрана
    private void Start()
    {
        maintexture = GetComponent<Renderer>().material; // инициализируем ссылку на текстуру
        StartCoroutine(ShowImages()); // запускаем корутину, сменяющую изображения
    }
    //массив из 10 изображений для загрузки, замените ссылки на свои!
    private string[] webImages = { "https://sun9-76.userapi.com/impf/hqhSMJ9NejQfdJLXSyJ560EMt9vaLNAwvGIj0Q/s0zlVI8ujhE.jpg?size=1920x1080&quality=96&proxy=1&sign=3d39fd749ccc2820f0e1a8dd2f671f10&type=album",
        "https://vgr.by/wp-content/uploads/2020/04/kot_uhan.jpg",
        "https://images.aif.by/007/618/a28f5e2141c6816501dee50f2f5cba2f.jpg",
        "https://sun9-60.userapi.com/impf/c850528/v850528825/18fc3e/zxg57KXt5CQ.jpg?size=1200x1600&quality=96&proxy=1&sign=fd7a11685d1c0fb4c3fa404465f3918f&type=album",
        "https://zooclub.ru/attach/24000/24586.jpg",
        "https://sun9-70.userapi.com/impf/PUouKP_kXAc00jrhzbRxFD6FATRGsOM-TVrLHA/IUIWW0LFdkQ.jpg?size=1080x1078&quality=96&proxy=1&sign=32482ab208dfad3837d2fcc931ac1c4a&type=album",
        "https://sun9-48.userapi.com/impf/c630920/v630920011/4db1/6cSCTQ7Eoos.jpg?size=2560x1920&quality=96&proxy=1&sign=dfec32b6e743caf08d3fd905237862c0&type=album",
        "https://sun9-49.userapi.com/impf/khH9YOMDUxXnVs7D_kB1Xkn6d0LUyUtPKJ25eQ/sm3huxuuCyo.jpg?size=1200x1600&quality=96&proxy=1&sign=12df0822d4723732f91bb7341bb48a69&type=album",
        "https://sun9-17.userapi.com/impf/c856028/v856028211/2458c9/wAXhzyozJ5Q.jpg?size=923x1080&quality=96&proxy=1&sign=93eaf2f3b0c6852b9203f70947e145df&type=album",
        "https://sun9-2.userapi.com/impf/UL-IihLLWVPCwG2LDcMTV_6Tkt4NENpS3ldtTA/uEdB730VS2M.jpg?size=983x687&quality=96&proxy=1&sign=ef523cd7341bb7e30a7b4d0539373ee5&type=album"
    };
    private Texture[] Images = new Texture[10]; // массив из загруженных изображений
    int i = 0; // счетчик, чтобы знать какое изображение показывается
    IEnumerator ShowImages() // корутина смены изображений
    {
        while (true)
        {
            if (Images[i] == null) // если требуемой текстуры нет в массиве
            {
                WWW www = new WWW(webImages[i]); // загружаем изображение по ссылке       
                yield return www; // ждем когда изображение загрузится
                Images[i] = www.texture; // записываем загруженную текстуру в массив
            }
            maintexture.mainTexture = Images[i]; // устанавливаем текстуру из массива изображений
            i++; // увеличиваем счетчик
            if (i == 10) i = 0; // если загрузили уже 9, возвращаемся к первому
            yield return new WaitForSeconds(3f); // ждем 3 секунды между сменой изображений
        }
    }

}
