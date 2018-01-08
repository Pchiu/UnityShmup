using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Animations
{
    public class AnimationSpawner : MonoBehaviour
    {
        public List<AnimationSpawnerItem> Items { get; set; }
        public float Radius { get; set; }
        public float Duration { get; set; }
        public float ElapsedTime { get; set; }
        public GameObject Parent { get; set; }
        public Random Random { get; set; }

        // Use this for initialization
        void Awake()
        {
            Items = new List<AnimationSpawnerItem>();
            ElapsedTime = 0f;
            
        }

        // Update is called once per frame
        void Update()
        {
            ElapsedTime += Time.deltaTime;

            if (ElapsedTime >= Duration)
            {
                Destroy(Parent);
                //Destroy(this.gameObject);
            }
        }

        public IEnumerator SpawnAnimation(AnimationSpawnerItem item, float radius)
        {
            while (true)
            {
                var randomPosition = Random.insideUnitCircle * radius;
                Instantiate(item.Sprite, new Vector3(transform.position.x + randomPosition.x, transform.position.y + randomPosition.y, 0), transform.rotation);
                yield return new WaitForSeconds(item.Interval);
            }
        }

        public void Initialize()
        {
            foreach (var item in Items)
            {
                StartCoroutine(SpawnAnimation(item, Radius));
            }
        }
    }
}