using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    public class AvailablePosition
    {
        // избавиться от магического числа
        private readonly LayerMask _layerMaskToIgnoreGround = 6;

        private Vector3[,] _arrayPositions;
        public Vector3 GetSpawnPosition(Vector3 originSpawnPoint, float spawnRadius, float agentRadius, SpawnType spawnType)
        {
            Vector3 freePoint = new Vector3();

            switch (spawnType)
            {
                case SpawnType.InPoint:
                    freePoint = FillMatrixWithFreePoints(originSpawnPoint, spawnRadius, agentRadius);
                    break;

                case SpawnType.InRadius:
                    freePoint = FillMatrixWithFreePoints(originSpawnPoint, spawnRadius, agentRadius);
                    break;

                default:
                    freePoint = originSpawnPoint;
                    break;
            }
            
            return freePoint;
        }

        private Vector3 FillMatrixWithFreePoints(Vector3 originPoint, float spawnRadius, float agentRadius)
        {
            int sizeMatrix = GetMatrixSize(spawnRadius, agentRadius);

            _arrayPositions = new Vector3[sizeMatrix, sizeMatrix];

            FillMatrix(originPoint, agentRadius, sizeMatrix);

            int iInd = _arrayPositions.GetLength(0) / 2;
            int jInd = _arrayPositions.GetLength(0) / 2;

            int iStep = 1;
            int jStep = 1;

            for (int i = 0; i < sizeMatrix; i++)
            {
                Debug.Log("Шаг итераций = " + i);

                for (int x = 0; x < i; x++)
                {
                    Debug.Log("1 цикл вектор = " + _arrayPositions[iInd, jInd += jStep]);
                    if (IsThisPointFree(_arrayPositions[iInd, jInd += jStep], agentRadius))
                    {
                        return _arrayPositions[iInd, jInd += jStep];
                    }
                }

                for (int y = 0; y < i; y++)
                {
                    Debug.Log("2 цикл вектор = " + _arrayPositions[iInd += iStep, jInd]);
                    if (IsThisPointFree(_arrayPositions[iInd += iStep, jInd], agentRadius))
                    {
                        return _arrayPositions[iInd += iStep, jInd];
                    }
                }

                jStep = -jStep;
                iStep = -iStep;
            }

            return Vector3.zero;
        }

        private int ChooseRandomPoint()
        {
            return Random.Range(0, _arrayPositions.Length);
        }

        private bool IsThisPointFree(Vector3 inputPoint, float agentRadius)
        {
            Collider[] hitCollliders = Physics.OverlapSphere(inputPoint, agentRadius);

            foreach (Collider col in hitCollliders)
            {
                if (col.gameObject.layer != _layerMaskToIgnoreGround)
                {
                    return false;
                }
            }
            return true;
        }

        private void FillMatrix(Vector3 originPoint, float agentRadius, int sizeMatrix)
        {
            float offset = agentRadius * (int)(sizeMatrix / 2);

            Vector3 zeroCell = new Vector3(originPoint.x - offset, originPoint.y, originPoint.z + offset);

            for (int z = 0; z < sizeMatrix; z++)
            {
                float positionZ = zeroCell.z - agentRadius * z;

                for (int x = 0; x < sizeMatrix; x++)
                {
                    float positionX = zeroCell.x + agentRadius * x;
                    var position = new Vector3(positionX, originPoint.y, positionZ);
                    _arrayPositions[x, z] = position;
                }
            }
        }

        private static int GetMatrixSize(float spawnRadius, float agentRadius)
        {
            int sizeMatrix = (int)Math.Ceiling(spawnRadius / agentRadius);

            if (sizeMatrix % 2 == 0)
            {
                sizeMatrix += 1;
            }

            return sizeMatrix;
        }

        /*
        private const string CHARACTER_PREFAB_NAME = "Character0Ragdoll1";

        public GameObject CreateCharacter(Vector3 spawnPosition, Quaternion spawnRotation)
        {
          GameObject prefab = Resources.Load<GameObject>(CHARACTER_PREFAB_NAME);

          return GameObject.Instantiate(prefab, spawnPosition, spawnRotation);
        }*/
    }
}