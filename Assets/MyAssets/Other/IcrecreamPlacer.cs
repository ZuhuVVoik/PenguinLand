using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IcrecreamPlacer : MonoBehaviour
{
    public Text text;

    public GameObject IcecreamPrefub; /*Префаб мороженки*/
    public GameObject[] CrystalPlaces; /*Массив возможных мест*/

    public Material[] Materials;/*Материалы*/

    /*Вероятность создания, не влияет на материал, в материалах полный рандом*/
    [Range(0,1)]
    public float generationChance;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject place in CrystalPlaces)
        {
            float seed = Random.Range(0, 1);
            if(seed <= generationChance) /*Создасться ли мороженка*/
            {
                GameObject icecream = Instantiate(IcecreamPrefub, place.transform.position, Quaternion.Euler(0,0,0));
                int materialIndex = Random.Range(1, Materials.Length + 1) - 1;/*Получим индекс текстурки из массива*/
                icecream.GetComponent<Renderer>().material = Materials[materialIndex];
            }
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
