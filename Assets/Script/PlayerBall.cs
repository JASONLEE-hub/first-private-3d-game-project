using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerBall : MonoBehaviour
{
    public int ringCount;
    public int totalRingCount;
    public int lifeCount;
    public int stageCount;
    public float JumpPower;
    public bool isJump;
    Rigidbody rigid;
    public TextMeshProUGUI totalRingCountText;
    public TextMeshProUGUI playerRingCountText;
    public TextMeshProUGUI lifeCountText;
    private AudioSource audioRing;
    public AudioClip ringSound;
    private AudioSource audioGoal;
    public AudioClip goalSound;
    private AudioSource audioE;
    public AudioClip eSound;
    private AudioSource audioLife;
    public AudioClip lifeSound;
    private AudioSource audioGreat;
    public AudioClip greatSound;
    private AudioSource audioYes;
    public AudioClip yesSound;
    private AudioSource audioDead;
    public AudioClip deadSound;
    private AudioSource audioJump;
    public AudioClip jumpSound;
    public GameObject ringAlarmText;
    public GameObject goalText;
    public GameObject bgm;
    public GameObject eEmerald;
    public GameObject dEmerald;



    void Start() {
        this.audioRing = this.gameObject.AddComponent<AudioSource>();
        this.audioRing.clip = this.ringSound;
        this.audioRing.loop = false;

        this.audioGoal = this.gameObject.AddComponent<AudioSource>();
        this.audioGoal.clip = this.goalSound;
        this.audioGoal.loop = false;

        this.audioE = this.gameObject.AddComponent<AudioSource>();
        this.audioE.clip = this.eSound;
        this.audioE.loop = false;

        this.audioLife = this.gameObject.AddComponent<AudioSource>();
        this.audioLife.clip = this.lifeSound;
        this.audioLife.loop = false;

        this.audioGreat = this.gameObject.AddComponent<AudioSource>();
        this.audioGreat.clip = this.greatSound;
        this.audioGreat.loop = false;

        this.audioYes = this.gameObject.AddComponent<AudioSource>();
        this.audioYes.clip = this.yesSound;
        this.audioYes.loop = false;

        this.audioDead = this.gameObject.AddComponent<AudioSource>();
        this.audioDead.clip = this.deadSound;
        this.audioDead.loop = false;

        this.audioJump = this.gameObject.AddComponent<AudioSource>();
        this.audioJump.clip = this.jumpSound;
        this.audioJump.loop = false;

        totalRingCountText.text = "/"+ totalRingCount.ToString();
    }


    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();       
        lifeCount = PlayerPrefs.GetInt("Life");
        GetLife(lifeCount);
    }

    void Update() {
        
        if(Input.GetButtonDown("Jump") && !isJump) {
            this.audioJump.Play();
            isJump = true;
            rigid.AddForce(new Vector3( 0, JumpPower, 0 ), ForceMode.Impulse);
        }
    }

  
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3( h, 0 ,v ), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Cube"){ // 이단 점프 방지
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other) {

        if(other.tag == "Ring") {
            ringCount++;
            this.audioRing.Play();
            other.gameObject.SetActive(false);
            GetRing(ringCount);

            if(ringCount == totalRingCount){
                eEmerald.SetActive(true);
                dEmerald.SetActive(false);
            }
        }
        else if(other.tag == "Goal") {
            if(ringCount == totalRingCount){
                rigid.AddForce(new Vector3( 0, 500, 0 ), ForceMode.Impulse);
                eEmerald.SetActive(false);
                goalText.SetActive(true);
                bgm.SetActive(false);
                Invoke("NOTING",0.5f);
                this.audioYes.Play();
                Invoke("NOTING",2f);
                this.audioGoal.Play();
                Invoke("NOTING",2f);
                this.audioE.Play();
                Invoke("StageUP",4.2f);
            }
            else{
                ringAlarmText.SetActive(true);
                Invoke("RingAlarmMessageOff",0.7f);
            }
        }
        else if(other.tag == "Life") {
            lifeCount++;
            PlayerPrefs.SetInt("Life",lifeCount);
            GetLife(lifeCount);
            bgm.SetActive(false);
            this.audioLife.Play();
            other.gameObject.SetActive(false);
            Invoke("BGMActive",3.1f);
            this.audioGreat.Play();
        }
        else if(other.tag == "Dead") {
            if (lifeCount <= 0){
                PlayerPrefs.SetInt("Life",2);
                PlayerPrefs.SetInt("Stage",0);
                this.audioDead.Play();
                Invoke("NOTING",1.5f);  // 시간차 문제
                LoadingSceneController.LoadScene(10);
            }else{
                lifeCount--;
                PlayerPrefs.SetInt("Life",lifeCount);
                GetLife(lifeCount);
                this.audioDead.Play();
                Invoke("Replay",0.7f);
            }
        }
        
    }
    public void Move(Vector2 inputDirection)
    {
        Vector2 moveInput = inputDirection;
        rigid.AddForce(moveInput, ForceMode.Impulse);
    }
    public void GetRing(int count) {
        playerRingCountText.text = count.ToString();
    }

    public void GetLife(int count) {
        lifeCountText.text = count.ToString();
    }

    void Replay(){
        stageCount = PlayerPrefs.GetInt("Stage");
        LoadingSceneController.LoadScene(stageCount);
    }

    void RingAlarmMessageOff(){
        ringAlarmText.SetActive(false);
    }

    void StageUP(){
        stageCount++;
        PlayerPrefs.SetInt("Stage",stageCount);
        LoadingSceneController.LoadScene(stageCount);
    }

    void BGMActive(){
        bgm.SetActive(true);
    }
    void NOTING(){
        Debug.Log("gsdfas");
    }

}
