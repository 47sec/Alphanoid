using System;
using System.Collections;
using UnityEngine;
public class Block : MonoBehaviour
{
    public static Action<Collider, int> OnBlockDestroy;
    public static Action OnLastBlockDestroy;
    public static int blockCount = 0;

    private AudioSource _audioSource;
    private MeshRenderer _meshRenderer;
    private BoxCollider _boxCollider;

    // Очки блока
    [field : SerializeField] public int Points { get; private set; }
    private void Start()
    {
        blockCount++;

        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnBlockDestroy?.Invoke(collision.collider, Points);
        
        if(blockCount == 0)
            OnLastBlockDestroy?.Invoke();

        ArcanoidBall ball = collision.collider.GetComponent<ArcanoidBall>();
        if (ball != null)
        {
            ball.platform.owner.score.AddPoints(Points);
        }

        _audioSource.Play();
        Destroy(_meshRenderer);
        Destroy(_boxCollider);
        StartCoroutine(WaitDestroy());
    }
    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        blockCount--;

        if (blockCount == 0)
            OnLastBlockDestroy?.Invoke();
    }
}
