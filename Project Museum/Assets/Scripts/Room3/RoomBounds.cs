using UnityEngine;

public class RoomBounds : MonoBehaviour {
    private Collider roomCollider;

    private void Start() {
        roomCollider = GetComponent<Collider>();
    }

    public bool IsInsideRoomBounds(Vector3 position) {
        return roomCollider.bounds.Contains(position);
    }
}