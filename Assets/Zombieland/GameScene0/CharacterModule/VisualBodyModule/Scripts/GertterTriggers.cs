using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class GertterTriggers
    {
        private readonly IVisualBodyController _visualBodyController;

        public GertterTriggers(IVisualBodyController visualBodyController)
        {
            _visualBodyController = visualBodyController;
        }

        public List<GameObject> GetSensorTriggers()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            if (_visualBodyController.CharacterInScene != null)
            {
                Transform[] childTransforms = _visualBodyController.CharacterInScene.GetComponentsInChildren<Transform>();

                foreach (Transform child in childTransforms)
                {
                    Collider collider = child.GetComponent<Collider>();
                    if (collider != null && collider.isTrigger)
                    {
                        CharacterJoint characterJoint = child.GetComponent<CharacterJoint>();
                        if (characterJoint != null)
                        {
                            gameObjects.Add(child.gameObject);
                            CreateColliderDuplicate(child);
                        }
                    }
                }
            }

            return gameObjects;
        }

        private void CreateColliderDuplicate(Transform triggerTransform)
        {
            Debug.Log("CreateColliderDuplicate: " + triggerTransform.name);
            
            Collider triggerCollider = triggerTransform.GetComponent<Collider>();
            if (triggerCollider != null)
            {
                // Создать новый объект для дубля
                GameObject duplicateObject = new GameObject(triggerTransform.gameObject.name + "_Collider");
                duplicateObject.transform.SetParent(triggerTransform, false);

                // Копировать параметры коллайдера
                if (triggerCollider is BoxCollider)
                {
                    BoxCollider boxTrigger = (BoxCollider)triggerCollider;
                    BoxCollider newCollider = duplicateObject.AddComponent<BoxCollider>();
                    CopyBoxCollider(boxTrigger, newCollider);
                }
                else if (triggerCollider is SphereCollider)
                {
                    SphereCollider sphereTrigger = (SphereCollider)triggerCollider;
                    SphereCollider newCollider = duplicateObject.AddComponent<SphereCollider>();
                    CopySphereCollider(sphereTrigger, newCollider);
                }
                else if (triggerCollider is CapsuleCollider)
                {
                    CapsuleCollider capsuleTrigger = (CapsuleCollider)triggerCollider;
                    CapsuleCollider newCollider = duplicateObject.AddComponent<CapsuleCollider>();
                    CopyCapsuleCollider(capsuleTrigger, newCollider);
                }
                else if (triggerCollider is MeshCollider)
                {
                    MeshCollider meshTrigger = (MeshCollider)triggerCollider;
                    MeshCollider newCollider = duplicateObject.AddComponent<MeshCollider>();
                    CopyMeshCollider(meshTrigger, newCollider);
                }

                // Отключить триггер на новом коллайдере
                duplicateObject.GetComponent<Collider>().isTrigger = false;
            }
        }

        private void CopyBoxCollider(BoxCollider source, BoxCollider target)
        {
            target.center = source.center;
            target.size = source.size;
            target.isTrigger = false;
        }

        private void CopySphereCollider(SphereCollider source, SphereCollider target)
        {
            target.center = source.center;
            target.radius = source.radius;
            target.isTrigger = false;
        }

        private void CopyCapsuleCollider(CapsuleCollider source, CapsuleCollider target)
        {
            target.center = source.center;
            target.radius = source.radius;
            target.height = source.height;
            target.direction = source.direction;
            target.isTrigger = false;
        }

        private void CopyMeshCollider(MeshCollider source, MeshCollider target)
        {
            target.sharedMesh = source.sharedMesh;
            target.convex = source.convex;
            target.isTrigger = false;
        }
    }
}