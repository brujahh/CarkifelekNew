using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HangmanControllerMobile : MonoBehaviour
{
    public Spin spin;
    [SerializeField] GameObject wordContainer;
    [SerializeField] GameObject hintContainer;
    [SerializeField] GameObject keyboardContainerLineOne;
    [SerializeField] GameObject keyboardContainerLineTwo;
    [SerializeField] GameObject keyboardContainerLineThree;
     [SerializeField] GameObject keyboardContainerLineSpin;
    [SerializeField] GameObject letterContainer;
    [SerializeField] GameObject[] hangmanStages;
    [SerializeField] GameObject letterButton;
    [SerializeField] GameObject spinButton;
    [SerializeField] GameObject wheelSpinBtn;
    [SerializeField] TextAsset possibleWord;
    [SerializeField] TextAsset hintWord;
    public bool buttonClicked = false;

    private string word;
    private string hint;

    public int incorrectGuesses, correctGuesses;
    
    public int keyCounter;
    public bool seasonEnded = false;
    public bool letterInWord = false;
    public Texture2D blinkLetterImg;
    public Texture2D blinkLetterImgBlinked;
    public TextMeshProUGUI hintContainerText;
    public string numberOfWord;
    public int wordListElementCnt = 0;
    public int correctGuessesSum;


    


    // Start is called before the first frame update
void Awake()
    {
        InitializeButtons();
         
       
        
    }
    

    void Start()
    {
     InitializeGame();     
      keyboardContainersActive(false);
     generateHint();
    }

    void Update ()
     {
    
     
       // Debug.Log(keyCounter);
       // Debug.Log("seaason ended? " + seasonEnded);
       // Debug.Log("button clicked? " + buttonClicked);
        StartCoroutine(isClicked());
        //Debug.Log("numberOfWord = " + numberOfWord);
         // Debug.Log("wordListElementCnt = " + wordListElementCnt);
          
     }


   public IEnumerator isClicked() 
    {
        //test
       testkeyboardContainersActive(true);
       
      if(buttonClicked == true){
         
      wheelSpinBtn.SetActive(false);
      yield return new WaitUntil(() => spin.isWheelRotating == false);
      keyboardContainersActive(true);
     // buttonClicked = false;
      seasonEnded = false;
      
      } else {
       keyboardContainersActive(false);
       
      }
      
    }

 public void onClick()
    {
        spin.RotateNow();
         
            
    }


    public void InitializeButtons()
    {
        
            
            CreateButtonLineOne(81);
            CreateButtonLineOne(87);
            CreateButtonLineOne(69);
            CreateButtonLineOne(82);
            CreateButtonLineOne(84);
            CreateButtonLineOne(89);
            CreateButtonLineOne(85);
            CreateButtonLineOne(73);
            CreateButtonLineOne(79);
            CreateButtonLineOne(80);
            CreateButtonLineTwo(65);
            CreateButtonLineTwo(83);
            CreateButtonLineTwo(68);
            CreateButtonLineTwo(70);
            CreateButtonLineTwo(71);
            CreateButtonLineTwo(72);
            CreateButtonLineTwo(74);
            CreateButtonLineTwo(75);
            CreateButtonLineTwo(76);
            CreateButtonLineThree(90);
            CreateButtonLineThree(88);
            CreateButtonLineThree(67);
            CreateButtonLineThree(86);
            CreateButtonLineThree(66);
            CreateButtonLineThree(78);
            CreateButtonLineThree(77);
            // Spin Button // CreateButtonLineSpin(13);
            
         

       
    }

    public void keyPressed () {
        seasonEnded = true;
    }
   public void spinBtnClicked () {
        buttonClicked = true;
    }

    public void InitializeGame() 
    {
        //reset data back to original state
        incorrectGuesses = 0;
        correctGuesses = 0;
        foreach(Button child in keyboardContainerLineOne.GetComponentsInChildren<Button>())
        {
            child.interactable = true;
            keyCounter++;
             child.onClick.AddListener(() => keyCounter--);
              child.onClick.AddListener(() => seasonEnded = false);
              child.onClick.AddListener(() => keyboardContainersActive(false));
             child.onClick.AddListener(() => buttonClicked = false);
            
             
           
            
             
        }
        foreach(Button child in keyboardContainerLineTwo.GetComponentsInChildren<Button>())
        {
            child.interactable = true;
            keyCounter++;
            child.onClick.AddListener(() => keyCounter--);
             child.onClick.AddListener(() => seasonEnded = false);
                child.onClick.AddListener(() => keyboardContainersActive(false));            
              child.onClick.AddListener(() => buttonClicked = false);

         
            
        }
        foreach(Button child in keyboardContainerLineThree.GetComponentsInChildren<Button>())
        {
            child.interactable = true;
            keyCounter++;
            child.onClick.AddListener(() => keyCounter--);
            child.onClick.AddListener(() => seasonEnded = false);
              child.onClick.AddListener(() => keyboardContainersActive(false)); 
              child.onClick.AddListener(() => buttonClicked = false);

       
            
        }
        foreach(Transform child in wordContainer.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
        foreach(GameObject stage in hangmanStages)
        {
            stage.SetActive(false);
        }

        //generate new word
        word = generateWord().ToUpper();
        foreach(char letter in word)
        {
          
            var temp = Instantiate(letterContainer, wordContainer.transform);
            
         
        }

         //hint = generateHint().ToUpper();
    
        //Debug.Log("Tuş Sayısı =" + keyCounter);
    }

 public void keyboardContainersActive(bool isActive)
    {
    keyboardContainerLineOne.SetActive(isActive);
        keyboardContainerLineTwo.SetActive(isActive);
        keyboardContainerLineThree.SetActive(isActive);
    }

     public void testkeyboardContainersActive(bool isActive)
    {
    keyboardContainerLineOne.SetActive(isActive);
        keyboardContainerLineTwo.SetActive(isActive);
        keyboardContainerLineThree.SetActive(isActive);
    }


    private void CreateButtonLineOne(int i)
    {
        GameObject temp = Instantiate(letterButton, keyboardContainerLineOne.transform);
        temp.GetComponentInChildren<TextMeshProUGUI>().text = ((char)i).ToString();
        temp.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(CheckLetter(((char)i).ToString())); });
    }

       private void CreateButtonLineTwo(int i)
    {
        GameObject temp = Instantiate(letterButton, keyboardContainerLineTwo.transform);
        temp.GetComponentInChildren<TextMeshProUGUI>().text = ((char)i).ToString();
        temp.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(CheckLetter(((char)i).ToString())); });
    }

       private void CreateButtonLineThree(int i)
    {
        GameObject temp = Instantiate(letterButton, keyboardContainerLineThree.transform);
        temp.GetComponentInChildren<TextMeshProUGUI>().text = ((char)i).ToString();
        temp.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(CheckLetter(((char)i).ToString())); });
    }

         private void CreateButtonLineSpin(int i)
    {
        GameObject temp = Instantiate(spinButton, keyboardContainerLineSpin.transform);
       
        temp.GetComponent<Button>().onClick.AddListener(delegate { CheckLetter(((char)i).ToString()); });
    }

        private void generateHint()
    {
        string[] hintList = hintWord.text.Split("\n");

       
            if(wordListElementCnt == 0){
                hintContainerText.GetComponentInChildren<TextMeshProUGUI>().text = hintList[0];
            } else if(wordListElementCnt == 1){
                hintContainerText.GetComponentInChildren<TextMeshProUGUI>().text = hintList[1];
            }else if(wordListElementCnt == 2){
                hintContainerText.GetComponentInChildren<TextMeshProUGUI>().text = hintList[2];
            }else if(wordListElementCnt == 3){
                hintContainerText.GetComponentInChildren<TextMeshProUGUI>().text = hintList[3];
            }else if(wordListElementCnt == 4){
                hintContainerText.GetComponentInChildren<TextMeshProUGUI>().text = hintList[4];
            }
         
    }

    private string generateWord()
    {
        string[] wordList = possibleWord.text.Split("\n");
        string line = wordList[Random.Range(0, wordList.Length - 1)];
       
        
            if(line == wordList[0]){
                wordListElementCnt = 0;
            } else if(line == wordList[1]){
                wordListElementCnt = 1;
            }else if(line == wordList[2]){
                wordListElementCnt = 2;
            }else if(line == wordList[3]){
                wordListElementCnt = 3;
            }else if(line == wordList[4]){
                wordListElementCnt = 4;
            }
        
        
         
        return line.Substring(0, line.Length - 1);
        
    }
   

    private IEnumerator CheckLetter(string inputLetter)
    {
        
        
        for(int i = 0; i < word.Length; i++)
        {
            
            if(inputLetter == word[i].ToString())
            {
               
                letterInWord = true;
                correctGuesses++;
                
                yield return new WaitForSeconds(1);
                wordContainer.GetComponentsInChildren<RawImage>()[i].texture = blinkLetterImgBlinked;
                yield return new WaitForSeconds(0.15f);
                wordContainer.GetComponentsInChildren<RawImage>()[i].texture = blinkLetterImg;
                yield return new WaitForSeconds(0.15f);
                wordContainer.GetComponentsInChildren<RawImage>()[i].texture = blinkLetterImgBlinked;
                yield return new WaitForSeconds(0.7f);
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = inputLetter;
                
               
                //Debug.Log("correctguess = " + correctGuesses);

            } else if (inputLetter != word[i].ToString())
            {
                // while'dan dolayı inaktif
               // wheelSpinBtn.SetActive(true);
            }
             correctGuessesSum = correctGuesses;
           
           Debug.Log("correctGuessesSum = " + correctGuessesSum);
        }
       
            StartCoroutine(wheelSpnSetActvFunc());

        if(letterInWord == false)
        {
            incorrectGuesses++;
            hangmanStages[incorrectGuesses - 1].SetActive(true);
           
            
        }
        StartCoroutine(CheckOutcome());
    }


public IEnumerator wheelSpnSetActvFunc () {
     switch (correctGuesses) 
        {
            case 0:
            yield return new WaitForSeconds(1);
                    wheelSpinBtn.SetActive(true);
                    correctGuesses = 0;
            break;
            case 1:
            yield return new WaitForSeconds(1);
                    wheelSpinBtn.SetActive(true);
                    correctGuesses = 0;
            break;
        case 2:
            yield return new WaitForSeconds(1);
                    wheelSpinBtn.SetActive(true);
                    correctGuesses = 0;
            break;
        case 3:
            yield return new WaitForSeconds(2);
                    wheelSpinBtn.SetActive(true);
                    correctGuesses = 0;
            break;
        case 4:
            yield return new WaitForSeconds(3);
                    wheelSpinBtn.SetActive(true);
                    correctGuesses = 0;
            break;
        case 5:
            yield return new WaitForSeconds(4);
                    wheelSpinBtn.SetActive(true);
                    correctGuesses = 0;
            break;
        case 6:
            yield return new WaitForSeconds(4);
                    wheelSpinBtn.SetActive(true);
                    correctGuesses = 0;
            break;
        case 7:
            yield return new WaitForSeconds(5);
                    wheelSpinBtn.SetActive(true);
                    correctGuesses = 0;
            break;
        }
}




    private IEnumerator CheckOutcome()
    {
        if(correctGuesses == word.Length) //win
        {
            for(int i = 0; i < word.Length; i++)
            {
                yield return new WaitForSeconds(2);
                
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.green;
            }
            Invoke("InitializeGame", 3f);
        }
        if(incorrectGuesses == hangmanStages.Length) //lose
        {
            for(int i = 0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.red;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = word[i].ToString();
            }
            Invoke("InitializeGame", 3f);
        }

    }
}
