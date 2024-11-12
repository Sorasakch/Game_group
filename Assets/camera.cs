using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    public Transform target; // ตัวละครที่ต้องการให้กล้องติดตาม
    public float smoothSpeed = 0.125f; // ความนุ่มนวลในการเคลื่อนที่ของกล้อง
    public Vector3 offset; // การตั้งค่า offset ของกล้อง

    void LateUpdate()
    {
        if (target != null)
        {
            // ตำแหน่งใหม่ของกล้อง โดยตามตำแหน่ง X ของตัวละครเท่านั้น
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
