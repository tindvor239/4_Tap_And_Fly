using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class VolantAnimal : MonoBehaviour
{
    [SerializeField] private float flySpeed, force, gravity, rotateSmooth;
    private float fixedFlySpeed;
    [SerializeField] private ParticleSystem hitEffect;
    private float balanceForce;
    [SerializeField] private AudioClip flySound, dieSound, scoreSound;
    private Quaternion downRotation = Quaternion.Euler(0, 0, -50), forwardRotation = Quaternion.Euler(0, 0, 40);
    [SerializeField] private bool isAlive;

    public static VolantAnimal Instance;
    private void Awake()
    {
        Instance = this;
        fixedFlySpeed = flySpeed;
    }
    #region Properties
    public float FlySpeed
    {
        get { return flySpeed; }
    }
    public float Force
    {
        get { return force; }
    }
    public float Gravity
    {
        get { return gravity; }
    }
    public float RotateSmooth
    {
        get { return rotateSmooth; }
    }
    public Quaternion DownRotation
    {
        get { return downRotation; }
    }
    public Quaternion ForwardRotation
    {
        get { return forwardRotation; }
    }
    public AudioClip FlySound
    {
        get { return flySound; }
    }
    public AudioClip DieSound
    {
        get { return dieSound; }
    }
    public AudioClip ScoreSound
    {
        get { return scoreSound; }
    }
    public AudioSource AudioSource
    {
        get { return GetComponent<AudioSource>(); }
    }
    public Transform Transform
    {
        get { return transform; }
    }
    public Animator Animator
    {
        get { return GetComponent<Animator>(); }
    }
    public bool IsAlive
    {
        get { return isAlive; }
    }
    public float BalanceForce
    {
        get { return balanceForce; }
        set { balanceForce = value; }
    }
    public ParticleSystem HitEffect
    {
        get { return hitEffect; }
    }
    #endregion
    #region Behavior
    public void Fly()
    {
        transform.position += Vector3.up * balanceForce * Time.deltaTime;
        balanceForce = Mathf.Lerp(balanceForce, 0f, gravity * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, rotateSmooth * Time.deltaTime);
    }
    public void MoveForward()
    {
        transform.position += Vector3.right * flySpeed * Time.deltaTime;
    }

    public void ResetBalanceForce()
    {
        balanceForce = force;
    }
    public void Scoring()
    {
        AudioSource.PlayOneShot(scoreSound);
        flySpeed += 0.1f;
    }
    public void BeingHit()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if(!hitEffect.isPlaying)
            hitEffect.Emit(1);
        AudioSource.PlayOneShot(dieSound);
        Animator.SetTrigger("Die");
    }
    public void Die()
    {
        isAlive = false;
    }

    public void Alive()
    {
        isAlive = true;
    }

    public void ResetSpeed()
    {
        flySpeed = fixedFlySpeed;
    }

    [System.Obsolete]
    public IEnumerator StopParticle()
    {
        yield return new WaitForSeconds(.6f);
        hitEffect.enableEmission = false;
    }
    #endregion
}
