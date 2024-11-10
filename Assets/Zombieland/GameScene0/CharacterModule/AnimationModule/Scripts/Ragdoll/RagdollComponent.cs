using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class RagdollComponent
    {
        public Transform Transform { get; private set; }
        public Rigidbody RigidBody { get; private set; }
        public Collider Collider { get; private set; }
        public CharacterJoint Joint { get; private set; }
        public Vector3 ConnectedAnchorDefault { get; private set; }
        public Quaternion PrivRotation { get; set; }
        public Quaternion StoredRotation { get; set; }

        public Vector3 PrivPosition { get; set; }
        public Vector3 StoredPosition { get; set; }

        public RagdollComponent(Transform transform)
        {
            Transform = transform;
            RigidBody = transform.GetComponent<Rigidbody>();
            Collider = transform.GetComponent<Collider>();
            Joint = transform.GetComponent<CharacterJoint>();
            ConnectedAnchorDefault = (Joint != null) ? Joint.connectedAnchor : Vector3.zero;
        }

        public void IsKinematikBone(bool isKinematic)
        {
            if (RigidBody != null)
            {
                RigidBody.isKinematic = isKinematic;
            }
        }
    }
}