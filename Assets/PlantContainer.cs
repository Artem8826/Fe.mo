using UnityEngine;

namespace Assets
{
    public class PlantContainer : MonoBehaviour
    {
        public SpriteRenderer SpriteShower;
        public Sprite[] GrowingPlantStages;
        public int ChangeRate = 5;
        public int StartIndex = 0;


        public void Update()
        {
            if (StartIndex < GrowingPlantStages.Length)
            {
                if (Time.time > StartIndex * ChangeRate)
                {
                    SpriteShower.sprite = GrowingPlantStages[StartIndex];

                    StartIndex++;
                }
            }
        }
    }
}
