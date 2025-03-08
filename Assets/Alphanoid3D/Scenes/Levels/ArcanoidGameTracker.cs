using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// —“–¿ÿÕ¿ﬂ –≈¿À»«¿÷»ﬂ ¡”ƒ≈“ œ≈–≈œ»—¿Õ¿
/// </summary>
public class ArcanoidGameTracker : MonoBehaviour
{
    public Player soloPlayer;
    public AudioSource audioSource;

    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    private void OnEnable()
    {
        soloPlayer.health.OnDead += Gameover;
        Block.OnLastBlockDestroy += Win;
    }
    private void OnDisable()
    {

        soloPlayer.health.OnDead -= Gameover;
        Block.OnLastBlockDestroy -= Win;
    }
    private void Gameover()
    {
        audioSource.Stop();
        audioSource.clip = loseSound;
        audioSource.Play();
        Restart();
    }
    private void Win()
    {
        audioSource.Stop();
        audioSource.clip = winSound;
        audioSource.Play();
        Restart();
    }
    private void Restart()
    {
        StartCoroutine(ProcessCoroutine());
        SceneManager.LoadSceneAsync("DefaultLevel");
    }
    public IEnumerator ProcessCoroutine()
    {
        Debug.Log(audioSource.clip.length);
        yield return new WaitForSeconds(audioSource.clip.length);
    }
}
