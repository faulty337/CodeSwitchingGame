using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Wordlist : MonoBehaviour
{
    public GameObject WordList_1, WordList_2;
    public Text Subject;
    public GameObject PfWordCotent;
    private GameObject WordContent;
    private List<GameObject> wordlist;
    // Start is called before the first frame update

    public void WordSet(List<string[]> Data, string Subject){
        wordlist = new List<GameObject>();
        bool sw = true;
        this.Subject.text = "word lists : "+Subject;
        foreach(string[] word in Data){
            
            if(sw){
                WordContent = Instantiate(PfWordCotent, WordList_1.transform);
            }else{
                WordContent = Instantiate(PfWordCotent, WordList_2.transform);
            }
            WordContent.GetComponent<WordListScript>().ContentUpdata(word[0], word[1]);
            WordContent.SetActive(true);
            sw = !sw;
            wordlist.Add(WordContent);
        }
    }
    public void WordListReset(){
        foreach(GameObject Con in wordlist){
            Con.SetActive(false);
            Destroy(Con);
        }
        wordlist.Clear();
    }

}
