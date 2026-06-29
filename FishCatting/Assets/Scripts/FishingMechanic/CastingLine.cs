using System.Collections;
using UnityEngine;

public class CastingLine : MonoBehaviour
{
    [SerializeField]
    private GameObject bobber;
    [SerializeField]
    private GameObject bobberModel;
    [SerializeField]
    private Transform uncastPosition;
    [SerializeField]
    private Material water;
    [SerializeField]
    private Animator animator;

    [Header("Casting Settings")]
    [SerializeField, Range(1, 89)]
    private float castAngle = 45f;
    [SerializeField]
    private float castSpeed = 1f;
    [SerializeField, Range(1, 89)]
    private float reelInAngle = 30f;
    [SerializeField]
    private float reelInSpeed = 10f;

    private GetClickPosition clickPosition;
    private FishCatching fishCatching;

    private Touch tap;

    private bool bobberCasted = false;
    private bool casted = false;

    private FishingMinigame minigame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clickPosition = new();
        fishCatching = GetComponent<FishCatching>();
        minigame = GetComponent<FishingMinigame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (minigame.BoolMiniGame() || minigame.BoolGetAnimation() || casted) return;
        if (Input.touchCount > 0)
        {
            tap = Input.GetTouch(0);
            if (tap.phase == TouchPhase.Ended)
            {
                StartCoroutine(CastBobber());
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CastBobber());
        }
    }

    private IEnumerator CastBobber()
    {
        if (clickPosition.GetTapPos("AllowCast") == Vector3.zero) yield break;
        casted = true;
        bobberCasted = !bobberCasted;
        if (!bobberCasted)
        {
            //bobber.transform.position = unCastPosition.position;
            bobberModel.transform.position = Vector3.zero;
            StopAllCoroutines();
            fishCatching.ReelIn();
            yield break;
        }
        fishCatching.SetFishTime();
        animator.SetTrigger("Casting");
        Vector3 tapPos = clickPosition.GetTapPos("AllowCast");
        if (!fishCatching.GetFishBitten()) yield return new WaitForSeconds(0.9f);
        yield return ThrowReel(tapPos, bobber.transform.position, castSpeed, castAngle);
        StartCoroutine(fishCatching.StartFishing());
    }

    public IEnumerator ThrowReel(Vector3 target, Vector3 beginPos, float speed, float angle)
    {
        // this part of the script was gotten from https://discussions.unity.com/t/throw-an-object-along-a-parabola/490479 from user: Stephan-B
        // Short delay added before Projectile is thrown

        // Calculate distance to target
        float target_Distance = Vector3.Distance(beginPos, target);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / (9.81f * speed));

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        bobber.transform.rotation = Quaternion.LookRotation(target - beginPos);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            bobber.transform.Translate(0, (Vy - (9.81f * speed * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
        bobber.transform.rotation = Quaternion.Euler(Vector3.zero);
        StartCoroutine(LandingWaves());
        casted = false;
    }

    private IEnumerator LandingWaves()
    {
        if (bobber.transform.position.y > 0)
        {
            water.SetFloat("_Power", 0);
        }
        else
        {
            water.SetVector("_BeginPos", new Vector4(bobber.transform.position.x, bobber.transform.position.z, 0, 0));
            water.SetFloat("_Power", 1);
        }
        while (water.GetFloat("_Power") > 0)
        {
            water.SetFloat("_Power", water.GetFloat("_Power") - 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        water.SetFloat("_Power", 0);
        yield return null;
    }
    public void SetCastingbool(bool pBool)
    {
        bobberCasted = pBool;
    }

    public GameObject GetBobber()
    {
        return bobber;
    }

    public Transform GetUnCassed()
    {
        return uncastPosition;
    }

    public float GetReelInAngle()
    {
        return reelInAngle;
    }
    public float GetReelInSpeed()
    {
        return reelInSpeed;
    }

    public Animator GetAnimator()
    {
        return animator;
    }
    private void OnApplicationQuit()
    {
        water.SetFloat("_Power", 0);
        water.SetVector("_BeginPos", Vector4.zero);
    }

    public void GetAnimator(Animator pAni)
    {
        animator = pAni;
    }
}