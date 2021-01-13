using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleSystemHandler : MonoBehaviour
{
    public static BloodParticleSystemHandler Instance { get; private set; }

    [SerializeField] private MeshParticleSystem meshParticleSystem;

    private List<Single> singleList;

    private void Awake()
    {
        Instance = this;
        singleList = new List<Single>();
        meshParticleSystem.GetComponent<MeshRenderer>().sortingLayerName = "Background";
    }

    private void Update()
    {
        for (int i = 0; i < singleList.Count; i++)
        {
            singleList[i].Update();
            if (singleList[i].IsMovementComplete())
            {
                //singleList[i].DestroySelf();
                singleList.RemoveAt(i);
            }
        }
    }

    public void SpawnBlood(Vector3 position, Vector3 direction)
    {
        var spawnedBlood = Random.Range(5, 10);

        for (int i = 0; i < spawnedBlood; i++)
        {
            singleList.Add(new Single(position, direction, meshParticleSystem));
        }
    }

    private class Single
    {
        private MeshParticleSystem meshParticleSystem;
        private Vector3 position;
        private Vector3 direction;
        private int quadIndex;
        private Vector3 quadSize;
        private float rotation;
        private float moveSpeed;
        private int uvIndex;

        public Single(Vector3 position, Vector3 direction, MeshParticleSystem meshParticleSystem)
        {
            this.position = position;
            this.direction = direction;
            this.meshParticleSystem = meshParticleSystem;

            quadSize = new Vector3(0.5f, 0.5f);
            rotation = Random.Range(0, 360);
            moveSpeed = Random.Range(1, 2);
            uvIndex = Random.Range(0, 5);

            quadIndex = meshParticleSystem.AddQuad(position, 0, quadSize, true, uvIndex);
        }

        public void Update()
        {
            position += direction * moveSpeed * Time.deltaTime;
            rotation += 360f * (moveSpeed/10f) * Time.deltaTime;

            meshParticleSystem.UpdateQuad(quadIndex, position, rotation, quadSize, true, uvIndex);

            float slowDownfactor = 1f;
            moveSpeed -= moveSpeed * slowDownfactor * Time.deltaTime;
        }

        public bool IsMovementComplete()
        {
            return moveSpeed < .1f;
        }

        public void DestroySelf()
        {
            meshParticleSystem.DestroyQuad(quadIndex);
        }
    }
}
