# BOYZ-Ping-Pong-ANN
Bilgisayar Oyunlarda Yapay Zeka Dersinin Ödev 7

## Proje Hazırlıkları:

1. Yeni 2D Proje Oluşturma :
   
✓ Unity'yi açarak yeni bir 2D proje oluşturun. 

✓ PongStarter2022 dosyalarını Unity'ye içe aktarın. 

✓ Pong sahnesini açın. 

2. Sahne Düzeni :
   
✓ İki yatay (üst ve alt) ve iki dikey (sol ve sağ) oyun nesnesi ekleyin. Bu nesneler, 
topun hareket edebileceği sınırları belirler.

✓ Yatay nesnelere BoxCollider2D ve RigidBody2D bileşenleri mevcuttur. 

✓ Dikey nesnelere aynı şekilde bileşenleri mevcut ve kırmızı renkli olan nesnenin 
etiketi backwall olarak ayarlandı. 

✓ Katman Ayarları: Eğer mevcut değilse, 8 ve 9 numaralı katmanları ekleyin ve 
nesneleri uygun katmanlara atayın. 

## Top ve Paddle Ayarları : 

1. Top Nesnesi :
   
✓ Top nesnesine RigidBody2D ve CircleCollider2D bileşenlerini mevcut. 

✓ CircleCollider2D bileşenine Bounce isimli fizik materyali atandı. Bu, topun 
çarpışmalardan sekmesini sağlar. 

2. Paddle (Raket) :
   
✓ Paddle nesnesine BoxCollider2D ve RigidBody2D bileşenleri mevcut. Hareket 
yalnızca Y ekseninde sınırlandırıldı. 

## MoveBall Scripti: 

Bu script, topun hareketini ve çarpışmalarını kontrol eder. 

✓ Top başlangıç pozisyonuna dönmesi için ResetBall fonksiyonu kullanılır. 

✓ Topun backwall ve diğer nesnelere çarpması durumunda ses efektleri (blip ve blop) 
çalınır. 

✓ space tuşuna basıldığında top yeniden başlatılır.

## Brain Scripti : 

Yapay zekâ (ANN) ile paddle hareketlerini kontrol etmek için geliştirilmiştir. 

1. Girdi Değerleri (Input):
 
✓ Topun pozisyonu ve hızı (bx, by, bvx, bvy). 

✓ Paddle pozisyonu (px, py). 

2. Çıktı Değeri (Output):
   
✓ Paddle’ın Y eksenindeki hızı (pv). 

3. Raycast Kullanımı:

✓ Topun yörüngesini analiz etmek ve paddle ile çarpışma olasılığını hesaplamak 
için kullanılır. 

4. Yapay Sinir Ağı (ANN) Ayarları:

✓ Gizli katman: 1 

✓ Nöron sayısı: 4 

✓ Aktivasyon fonksiyonu: tanh (-1 ile 1 arası değer döner). 

## Test ve İyileştirme: 

1. Eğitim Seti Zenginleştirme: 

✓ Topun sınır nesnelerine çarpması durumunda raycast ile sonraki hareketleri 
analiz edilerek eğitim setine ek veri sağlandı. 

2. Öğrenme Hızının İyileştirilmesi: 

✓ Öğrenme hızı, farklı değerlerde test edilerek (ör. 0.001) en iyi sonuç gözlemlendi. 

## Sonuç : 

Proje başarıyla tamamlanmış ve paddle yapay zekâ tarafından kontrol edilebilir hale getirilmiştir. 
Eğitim seti genişletildiğinde ve daha fazla veri sağlandığında, yapay zekâ performansının 
iyileştirilebileceği gözlemlenmiştir.

![Ekran görüntüsü 2024-12-08 045404](https://github.com/user-attachments/assets/0b827a71-dbc4-4400-8f1c-f2bc795926bb)

![Ekran görüntüsü 2024-12-08 045533](https://github.com/user-attachments/assets/dd381146-c4ac-47ea-b878-47be41dea3a4)

![Ekran görüntüsü 2024-12-08 045617](https://github.com/user-attachments/assets/0e072921-bc1f-442f-9bab-6c6bfcb7b728)
