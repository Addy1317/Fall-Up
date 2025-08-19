using UnityEngine;

namespace SS.FallUp.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerDiscreteSwipe2D : MonoBehaviour
    {
        [Header("Lane Layout")]
        [Tooltip("Total number of horizontal lanes.")]
        [SerializeField] private int laneCount = 3;

        [Tooltip("If true, lane centers are derived from playerData.minX/maxX.")]
        [SerializeField] private bool deriveLaneWidthFromBounds = true;

        [Tooltip("Manual lane width when not deriving from bounds.")]
        [SerializeField] private float laneWidth = 1.5f;

        [Tooltip("Start lane index (0..laneCount-1).")]
        [SerializeField] private int startLaneIndex = 1;

        [Header("Bounds (optional, used when deriveLaneWidthFromBounds=true)")]
        [SerializeField] private float minX = -1.5f;
        [SerializeField] private float maxX = 1.5f;

        [Header("Step Movement")]
        [Tooltip("Seconds to slide to the next lane.")]
        [SerializeField] private float stepDuration = 0.1f;

        [Tooltip("Clamp horizontal speed to keep physics sane.")]
        [SerializeField] private float maxHorizontalSpeed = 12f;

        [Header("Fall Feel")]
        [Tooltip("Terminal fall speed (negative value).")]
        [SerializeField] private float maxFallSpeed = -20f;

        [Header("Swipe Settings")]
        [Tooltip("Base pixel threshold (auto-scales by device DPI).")]
        [SerializeField] private float baseSwipeThresholdPx = 48f;

        [Header("Flip Animation")]
        [SerializeField] private bool rotateOnStep = false;
        [Tooltip("How much to rotate around Z during a step (e.g., 90).")]
        [SerializeField] private float stepRotateDegrees = 90f;

        [Header("Debug")]
        [SerializeField] private bool debugLogs = true;
        [SerializeField] private Color gizmoLaneColor = new Color(0.2f, 0.8f, 1f, 0.35f);

        // Refs
        private Rigidbody2D rb2d;
        private PlayerController playerController; // optional

        // Lane state
        private int currentLane;
        private int minLane;
        private int maxLane;
        private float laneCenterX0;   // world X of lane 0
        private float computedLaneWidth;

        // Step tween
        private bool stepping;
        private float stepT;
        private Vector3 stepFrom;
        private Vector3 stepTo;
        private float startZRot;

        // Swipe
        private Vector2 startPos;
        private bool tracking;
        private float swipeThresholdPx;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            playerController = GetComponent<PlayerController>(); // ok if null

            // Prefer playerController.rigidbody2d if you store it there
            if (playerController != null && playerController.rigidbody2d != null)
                rb2d = playerController.rigidbody2d;

            rb2d.interpolation = RigidbodyInterpolation2D.Interpolate;
            rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb2d.freezeRotation = true; // keep the cube stable; we�ll apply visual Z-rot ourselves if desired

            // Pull bounds from playerData if present
            if (playerController != null && playerController.playerData != null)
            {
                minX = playerController.playerData.minX;
                maxX = playerController.playerData.maxX;
            }

            // Compute lanes
            minLane = 0;
            maxLane = Mathf.Max(0, laneCount - 1);

            if (deriveLaneWidthFromBounds)
            {
                if (laneCount <= 1)
                {
                    computedLaneWidth = 0f;
                    laneCenterX0 = (minX + maxX) * 0.5f;
                }
                else
                {
                    computedLaneWidth = (maxX - minX) / Mathf.Max(1, laneCount - 1);
                    laneCenterX0 = minX; // lane 0 at minX, last lane at maxX
                }
            }
            else
            {
                computedLaneWidth = laneWidth;
                // center lanes around x=0
                float half = (laneCount - 1) * 0.5f;
                laneCenterX0 = -half * computedLaneWidth;
            }

            currentLane = Mathf.Clamp(startLaneIndex, minLane, maxLane);
            // Snap to starting lane
            var p = rb2d.position;
            p.x = LaneIndexToWorldX(currentLane);
            rb2d.position = p;

            // DPI-normalized threshold
            float dpi = Screen.dpi <= 0 ? 160f : Screen.dpi;
            swipeThresholdPx = baseSwipeThresholdPx * (dpi / 160f);
        }

        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            // Mouse emulate touch
            if (Input.GetMouseButtonDown(0))
            {
                tracking = true;
                startPos = Input.mousePosition;
            }
            else if ((Input.GetMouseButtonUp(0) || Input.GetMouseButton(0) == false) && tracking)
            {
                HandleSwipe((Vector2)Input.mousePosition - startPos);
                tracking = false;
            }

            // Keyboard quick tests
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) StepLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) StepRight();
#else
            if (Input.touchCount > 0)
            {
                var t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    tracking = true;
                    startPos = t.position;
                }
                else if ((t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) && tracking)
                {
                    HandleSwipe(t.position - startPos);
                    tracking = false;
                }
            }
#endif
        }

        private void FixedUpdate()
        {
            // Fall feel: clamp terminal speed
            if (rb2d.linearVelocity.y < maxFallSpeed)
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, maxFallSpeed);

            // Perform the step tween in physics
            if (stepping)
            {
                stepT += Time.fixedDeltaTime / Mathf.Max(0.0001f, stepDuration);
                float t = Mathf.Clamp01(stepT);

                float newX = Mathf.Lerp(stepFrom.x, stepTo.x, t);
                var pos = rb2d.position;
                pos.x = newX;
                rb2d.MovePosition(pos);

                // Optional flip animation (visual Z rotation)
                if (rotateOnStep)
                {
                    float dir = Mathf.Sign(stepTo.x - stepFrom.x); // +1 right, -1 left
                    float targetZ = startZRot + (-dir) * stepRotateDegrees; // right swipe usually looks like negative Z
                    float z = Mathf.LerpAngle(startZRot, targetZ, t);
                    transform.rotation = Quaternion.Euler(0f, 0f, z);
                }

                // Horizontal speed clamp (prevent spikes)
                var v = rb2d.linearVelocity;
                v.x = Mathf.Clamp(v.x, -maxHorizontalSpeed, maxHorizontalSpeed);
                rb2d.linearVelocity = v;

                if (t >= 1f)
                {
                    stepping = false;
                    // Snap to exact lane center to avoid drift
                    pos = rb2d.position;
                    pos.x = stepTo.x;
                    rb2d.MovePosition(pos);

                    if (rotateOnStep)
                    {
                        // snap back to clean 0/90/180 if you want�here we keep final
                        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Round(transform.eulerAngles.z / 90f) * 90f);
                    }

                    if (debugLogs) Debug.Log($"[PlayerDiscreteSwipe2D] Lane reached: {currentLane} (x={pos.x:F2})");
                }
            }
        }

        private void HandleSwipe(Vector2 delta)
        {
            if (delta.magnitude < swipeThresholdPx) return;

            // Only horizontal swipes matter
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x > 0f) StepRight();
                else StepLeft();
            }
        }

        private void StepLeft()
        {
            if (stepping || currentLane <= minLane) return;
            StepToLane(currentLane - 1);
        }

        private void StepRight()
        {
            if (stepping || currentLane >= maxLane) return;
            StepToLane(currentLane + 1);
        }

        private void StepToLane(int targetLane)
        {
            targetLane = Mathf.Clamp(targetLane, minLane, maxLane);
            if (debugLogs) Debug.Log($"[PlayerDiscreteSwipe2D] Step {currentLane} -> {targetLane}");

            currentLane = targetLane;
            stepFrom = rb2d.position;
            stepTo = new Vector3(LaneIndexToWorldX(currentLane), stepFrom.y, 0f);

            stepT = 0f;
            stepping = true;

            if (rotateOnStep)
                startZRot = transform.eulerAngles.z;
        }

        private float LaneIndexToWorldX(int laneIndex)
        {
            return laneCenterX0 + laneIndex * computedLaneWidth;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (laneCount <= 0) return;

            // Recompute preview lane params for gizmo
            float _minX = minX, _maxX = maxX, _laneWidth = laneWidth, _x0;
            if (deriveLaneWidthFromBounds)
            {
                if (laneCount <= 1)
                {
                    _laneWidth = 0f;
                    _x0 = (_minX + _maxX) * 0.5f;
                }
                else
                {
                    _laneWidth = (_maxX - _minX) / Mathf.Max(1, laneCount - 1);
                    _x0 = _minX;
                }
            }
            else
            {
                float half = (laneCount - 1) * 0.5f;
                _x0 = -half * _laneWidth;
            }

            Gizmos.color = gizmoLaneColor;
            float y = Application.isPlaying ? rb2d.position.y : transform.position.y;
            for (int i = 0; i < laneCount; i++)
            {
                float x = _x0 + i * _laneWidth;
                Gizmos.DrawLine(new Vector3(x, y - 8f, 0f), new Vector3(x, y + 12f, 0f));
#if UNITY_EDITOR
                UnityEditor.Handles.Label(new Vector3(x, y + 1.2f, 0f), $"Lane {i}");
#endif
            }
        }
#endif
    }
}
