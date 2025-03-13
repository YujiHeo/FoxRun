using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    [Header("BGM Settings")]
    public AudioSource bgmSource; // BGM�� ����ϴ� AudioSource (MainCamera�� AudioSource ���۳�Ʈ �߰�, BGMPlayer �߰�)
    public AudioClip[] bgmClips; // BGM ����Ʈ
    [Range(0f, 1f)] public float bgmVolume = 1f; // BGM ����
    [Range(0f, 1f)] public float sfxVolume = 1f; // SFX ����

    [Header("SFX Settings")]
    public GameObject sfxPrefab; // ȿ���� ����� ���� ������Ʈ ������
    public int poolSize = 10;    // ������Ʈ Ǯ ũ��
    private Queue<AudioSource> sfxPool = new Queue<AudioSource>(); // ����� �ҽ��� ������ ť (������Ʈ Ǯ��)

    [Header("SFX Clips")]
    public AudioClip[] sfxClips; // ȿ���� ����Ʈ
    private Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>(); // ȿ������ ã�� ��ųʸ�

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitSFXPool();  // ������Ʈ Ǯ �ʱ�ȭ
        LoadSFX();      // SFX �����͸� ��ųʸ��� ����

        LoadVolumes();
    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Dictionary�� ȿ���� ���ϸ����� Key�� ����
    private void LoadSFX()
    {
        foreach (var clip in sfxClips)
        {
            sfxDictionary[clip.name] = clip;
        }
    }

    // ������Ʈ Ǯ ����
    private void InitSFXPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject sfxObj = Instantiate(sfxPrefab, transform);
            AudioSource sfxSource = sfxObj.GetComponent<AudioSource>();
            sfxObj.SetActive(false);
            sfxPool.Enqueue(sfxSource);
        }
    }

    // ���� ���� �޼���
    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
        SaveVolumes();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        SaveVolumes();
    }

    // ���� ������Ʈ �޼��� ����
    public void UpdateVolumes()
    {
        if (bgmSource != null)
        {
            bgmSource.volume = bgmVolume;
        }
    }

    // ���� ���� �޼���
    private void SaveVolumes()
    {
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    // ���� �ε� �޼���
    private void LoadVolumes()
    {
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        UpdateVolumes();
    }

    // �� �̸��� ������ �ش� ���� �´� BGM ���, �� ��ȯ �� SoundManager.Instance.PlayBGM(sceneName);
    public void PlayBGM(string sceneName)
    {
        AudioClip bgm = GetBGMByScene(sceneName);

        if (bgm == null)
        {
            Debug.LogWarning($"���� ���� ����� �����ϴ�. Scene �̸� Ȯ��");
            return;
        }

        StartCoroutine(FadeInBGM(bgm));
    }

    // BGM ���̵��� ȿ��(���� BGM ������ ������� ���ο� BGM�� �����)
    private IEnumerator FadeInBGM(AudioClip newBGM)
    {
        float startVolume = bgmSource.volume;
        float targetVolume = bgmVolume; // ����� BGM ���� �� ���

        // ���� BGM ������ ����
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0, t);
            yield return null;
        }

        // ���ο� BGM ���� �� ����
        bgmSource.clip = newBGM;
        bgmSource.Play();

        // ������ ����� ������ ������ ����
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(0, targetVolume, t);
            yield return null;
        }

        // ���� ���� ����
        bgmSource.volume = targetVolume;
    }

    // �� �̸��� ���� �ش��ϴ� BGM ��ȯ
    private AudioClip GetBGMByScene(string sceneName)
    {
        switch (sceneName)
        {
            case "HMJ_Map":
                return bgmClips[0];
            case "HMJ_Test":
                return bgmClips[1];
            default:
                return null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(scene.name); // �� �̸��� ���� BGM �ڵ� ���
    }

    // ȿ���� ����, �ٸ� ��ũ��Ʈ���� �������� ����(ȿ���� �̸�, ��ġ) 
    public void PlaySFX(string sfxName, Vector3 position)
    {
        if (!sfxDictionary.ContainsKey(sfxName))
        {
            Debug.LogWarning($"ȿ���� �̸��� �ٸ��ϴ�. ȿ���� �̸��� ��ũ��Ʈ���� �Ű������� Ȯ��");
            return;
        }

        PlaySFX(sfxDictionary[sfxName], position);
    }

    private void PlaySFX(AudioClip clip, Vector3 position)
    {
        AudioSource sfxSource = GetAudioSource(); // ����� �ҽ� ��������

        sfxSource.transform.position = position; // ��� ��ġ ����
        sfxSource.clip = clip;
        sfxSource.volume = sfxVolume; // SFX ���� ����
        sfxSource.gameObject.SetActive(true);
        sfxSource.Play();

        StartCoroutine(ReturnToPool(sfxSource, clip.length + 2.0f)); // ��� �� �ٽ� Ǯ�� ��ȯ
    }
    
    // Queue�� Ǯ�� ������ Ǯ �߰�
    private AudioSource GetAudioSource()
    {
        if (sfxPool.Count > 0)
        {
            return sfxPool.Dequeue(); // ť���� ����� �ҽ� ��������
        } 

        GameObject sfxObj = Instantiate(sfxPrefab, transform);

        return sfxObj.GetComponent<AudioSource>();
    }

    // ȿ������ ���� �� �ٽ� Ǯ�� ��ȯ
    private IEnumerator ReturnToPool(AudioSource sfxSource, float delay)
    {
        yield return new WaitForSeconds(delay);

        sfxSource.gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
        sfxPool.Enqueue(sfxSource);            // ť�� �ٽ� �߰�
    }
}
