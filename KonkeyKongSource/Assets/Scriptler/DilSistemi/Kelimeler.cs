using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Kelimeler
{
    public string ayarlar;
    public string durdur;
    public string oyundanCik;
    public string yuksekPuan;
    public string nasilOynanir;
    public string puan;
    public string shiftileIsinlanabilirsin;
    public string yenidenOyna;
    public string sesAc;
    public string sesKapat;
    public string baslangicEkrani;
    public string baslangicEkraniAciklamasi;
    public string turkce;
    public string ingilizce;
    public string nasilOynanirAciklama;


    public void Turkce()
    {
        ayarlar = "Ayarlar";
        durdur = "Durdur";
        oyundanCik = "Oyundan Çık";
        puan = "Puan";
        nasilOynanir = "Nasıl Oynanır?";
        yuksekPuan = "Yüksek Puan";
        yenidenOyna = "Yeniden Oyna";
        baslangicEkrani = "Başlangıç Ekranı";
        sesAc = "Sesi Aç";
        sesKapat = "Sesi Kapat";
        turkce = "Türkçe";
        ingilizce = "Ingilizce";

        shiftileIsinlanabilirsin = "Shift tuşuna basarak ışınlanabilirsin.";
        baslangicEkraniAciklamasi = "Oyuna başlamak için sağdaki portaldan geçebilir veya soldaki eve girip oyundan çıkabilirsiniz.";
        nasilOynanirAciklama = "<b>Nasıl Oynanır</b>\n\n\"WASD\" veya \"Yön Tuşları\" tuşları ile karakterinizi hareket ettirebilir, boşluk tuşu ile engellerin üstünden zıplayabilirsiniz. \n\nEğer ışınlanma bonusu almışsanız \"Shift\" tuşunu kullanarak imlece doğru ışınlanabilirsiniz. \n\nOyuna başlamak için sağ tarafta bulunan portaldan geçebilir, oyunu kapatmak içinde soldaki eve girebilirsiniz.";
    }
    public void Ingilice()
    {
        ayarlar = "Settings";
        durdur = "Pause";
        oyundanCik = "Exit";
        puan = "Score";
        nasilOynanir = "How to play?";
        yuksekPuan = "High Score";
        yenidenOyna = "Restart";
        baslangicEkrani = "Main Screen";
        sesAc = "Audio On";
        sesKapat = "Audio Off";
        turkce = "Turkish";
        ingilizce = "English";

        shiftileIsinlanabilirsin = "You can teleport by pressing the Shift key.";
        baslangicEkraniAciklamasi = "To start the game, you can go through the portal on the right or enter the house on the left and exit the game.";
        nasilOynanirAciklama ="<b> How to Play </b>\n\n\"WASD \" or \"Arrow Keys \" You can move your character, jump over obstacles with the spacebar. \n\nIf you have received a teleport bonus, you can teleport to the cursor using the \"Shift\" key. \n\nnTo start the game, you can go through the portal on the right, enter the house on the left to close the game. ";
    }

}