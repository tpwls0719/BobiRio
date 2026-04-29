using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Header("Players")]
    public Transform player1;
    public Transform player2;

    [Header("Background (필수)")]
    public SpriteRenderer background;

    [Header("Movement")]
    public float moveSpeed = 8f;

    [Header("Zoom")]
    public float minZoom = 2.5f;
    public float zoomPadding = 2.0f; // 🔥 여유 공간 (중요)
    public float zoomSpeed = 10f;

    [Header("Start Effect")]
    public float startZoom = 5f;
    public float startDelay = 2f;
    public Transform stageCenter;

    private Camera cam;
    private bool isStarting = true;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    IEnumerator Start()
    {
        if (stageCenter != null)
        {
            transform.position = new Vector3(stageCenter.position.x, stageCenter.position.y, transform.position.z);
        }

        cam.orthographicSize = startZoom;

        yield return new WaitForSeconds(startDelay);

        isStarting = false;
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null || background == null) return;
        if (isStarting) return;

        // ⭐ 순서 중요
        ZoomCamera();
        MoveCamera();
    }

    // 🔥 캐릭터 크기까지 포함한 줌
    void ZoomCamera()
    {
        float requiredZoom = GetRequiredZoom();

        // 최소 줌 제한
        requiredZoom = Mathf.Max(requiredZoom, minZoom);

        // 배경 기준 최대 줌 (유일한 제한)
        float maxZoomFromMap = GetMaxZoomFromBounds();
        requiredZoom = Mathf.Min(requiredZoom, maxZoomFromMap);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, requiredZoom, Time.deltaTime * zoomSpeed);

        // ⭐ 줌 후 위치 다시 보정
        transform.position = ClampPosition(transform.position);
    }

    // 🔥 카메라 이동
    void MoveCamera()
    {
        Vector3 center = GetCenterPoint();

        Vector3 targetPos = new Vector3(center.x, center.y, transform.position.z);

        targetPos = ClampPosition(targetPos);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }

    Vector3 GetCenterPoint()
    {
        return (player1.position + player2.position) / 2f;
    }

    // 🔥 핵심: 캐릭터 "크기" 포함
    Bounds GetPlayersBounds()
    {
        Renderer r1 = player1.GetComponent<Renderer>();
        Renderer r2 = player2.GetComponent<Renderer>();

        Bounds bounds = r1.bounds;
        bounds.Encapsulate(r2.bounds);

        return bounds;
    }

    float GetRequiredZoom()
    {
        Bounds bounds = GetPlayersBounds();

        float height = bounds.extents.y;
        float width = bounds.extents.x / cam.aspect;

        float requiredZoom = Mathf.Max(height, width);

        return requiredZoom + zoomPadding;
    }

    // 🔒 배경 밖 이동 제한
    Vector3 ClampPosition(Vector3 pos)
    {
        Bounds bounds = background.bounds;

        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        float minX = bounds.min.x + width;
        float maxX = bounds.max.x - width;
        float minY = bounds.min.y + height;
        float maxY = bounds.max.y - height;

        float x = Mathf.Clamp(pos.x, minX, maxX);
        float y = Mathf.Clamp(pos.y, minY, maxY);

        return new Vector3(x, y, pos.z);
    }

    // 🔒 배경 기준 최대 줌
    float GetMaxZoomFromBounds()
    {
        Bounds bounds = background.bounds;

        float heightLimit = bounds.size.y / 2f;
        float widthLimit = bounds.size.x / (2f * cam.aspect);

        return Mathf.Min(heightLimit, widthLimit);
    }
}