using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Spin : MonoBehaviour
{
   public HangmanControllerMobile hangmanContoller;
   [SerializeField] GameObject score;
[SerializeField] GameObject scoreText;

public TextMeshProUGUI earnedPointsOnScreen;

public AnimationCurve curve;
public Transform parent;
public int numberOfGift = 12;
public float timeRotate;
public float numberCircleRotate;
private const float CIRCLE = 360.0f;
private float angleOfOneGift;

private float currentTime;

public int earnedPoints;







public bool isWheelRotating = false;


private void Start() {
angleOfOneGift = CIRCLE / numberOfGift;
setPositionData ();
}

private void Update() 
{
   
}




    

 

public IEnumerator RotateWheel () {
   isWheelRotating = true;
    float startAngle = transform.eulerAngles.z;
currentTime = 0;

int indexGiftRandom = Random.Range(1, numberOfGift);
Debug.Log("indexGiftRandom = " + indexGiftRandom);



 if (indexGiftRandom == 1)
        {
            
            Win(1000);
            earnedPoints += 1000*hangmanContoller.correctGuessesSum;
        earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
        
        }
        else if (indexGiftRandom == 2)
        {
           Win(750);
           earnedPoints += 750*hangmanContoller.correctGuessesSum;
          
           earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
           
        }
         else if (indexGiftRandom == 3)
        {
           Win(500);
           earnedPoints += 500*hangmanContoller.correctGuessesSum;
           earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
           
        }
         else if (indexGiftRandom == 4)
        {
           Win(250);
           earnedPoints += 250*hangmanContoller.correctGuessesSum;
           earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
           
        }
         else if (indexGiftRandom == 5)
        {
           Win(100);
           earnedPoints += 100*hangmanContoller.correctGuessesSum;
           earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
           
        }
         else if (indexGiftRandom == 6)
        {
           Win(50);
           earnedPoints += 50*hangmanContoller.correctGuessesSum;
           earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
           
        }
         else if (indexGiftRandom == 7)
        {
           Win(2500);
           earnedPoints += 2500*hangmanContoller.correctGuessesSum;
           earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
           
        }
         else if (indexGiftRandom == 8)
        {
           Win(5000);
           earnedPoints += 5000*hangmanContoller.correctGuessesSum;
           earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
           
        }
         else if (indexGiftRandom == 9)
        {
           Win(10000);
           earnedPoints += 10000*hangmanContoller.correctGuessesSum;
           earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
          
        }
         else if (indexGiftRandom == 10)
        {
           Win(25000);
           earnedPoints += 25000*hangmanContoller.correctGuessesSum;
           earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
          
        }
         else if (indexGiftRandom == 11)
        {
           Win(0); 
           earnedPoints += 0;
           earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
           
        }
         else if (indexGiftRandom == 12)
        {
           Win(0);
           earnedPoints += 0;
           earnedPointsOnScreen.GetComponentInChildren<TextMeshProUGUI>().text = earnedPoints.ToString();
           
        }
   
        

float angleWant = (numberCircleRotate * CIRCLE) + angleOfOneGift * indexGiftRandom - startAngle;

while(currentTime < timeRotate){
    yield return new WaitForEndOfFrame();
    currentTime += Time.deltaTime;
    float angleCurrent = angleWant * curve.Evaluate(currentTime / timeRotate);
    this.transform.eulerAngles = new Vector3(0,0, angleCurrent);
}
isWheelRotating = false;
}



    public void Win(int Score)
    {
        Debug.Log(Score);
        Debug.Log("Points =" + earnedPoints);

    }
    

[ContextMenu("Rotate")]
public void RotateNow(){

StartCoroutine(RotateWheel());



}

void setPositionData () {
    for (int i = 0; i < parent.childCount; i++){
        parent.GetChild(i).eulerAngles = new Vector3(0,0, -CIRCLE / numberOfGift * i);
        parent.GetChild(i).GetChild(0).GetComponent<TextMeshPro>().text = (i + 1).ToString();
    }
}

}
