using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 offsetA = new Vector3(-2, 0, 0); // ระยะห่างจากตำแหน่งเริ่มต้นไปยังตำแหน่ง A
    public Vector3 offsetB = new Vector3(2, 0, 0);  // ระยะห่างจากตำแหน่งเริ่มต้นไปยังตำแหน่ง B
    public float speed = 2.0f;     // ความเร็วในการเคลื่อนที่ของแพลตฟอร์ม

    private Vector3 pointA;         // ตำแหน่งจริงของจุดเริ่มต้น
    private Vector3 pointB;         // ตำแหน่งจริงของจุดปลายทาง
    private Vector3 targetPosition; // ตำแหน่งเป้าหมายปัจจุบันของแพลตฟอร์ม

    void Start()
    {
        // คำนวณตำแหน่ง pointA และ pointB ตามตำแหน่งเริ่มต้นของแพลตฟอร์ม
        pointA = transform.position + offsetA;
        pointB = transform.position + offsetB;

        // เริ่มต้นแพลตฟอร์มให้เคลื่อนที่ไปยังตำแหน่ง B
        targetPosition = pointB;
    }

    void Update()
    {
        // เคลื่อนที่แพลตฟอร์มไปยังตำแหน่งเป้าหมายปัจจุบัน
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // หากแพลตฟอร์มถึงตำแหน่งเป้าหมายแล้ว ให้เปลี่ยนเป้าหมาย
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = targetPosition == pointA ? pointB : pointA;
        }
    }

    private void OnDrawGizmos()
    {
        // คำนวณตำแหน่ง pointA และ pointB โดยอ้างอิงจากตำแหน่งปัจจุบันของแพลตฟอร์ม
        Vector3 gizmoPointA = transform.position + offsetA;
        Vector3 gizmoPointB = transform.position + offsetB;

        // วาดเส้นสีแดงระหว่างตำแหน่ง pointA และ pointB
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gizmoPointA, gizmoPointB);
    }
}
