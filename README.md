# Mini Game Framework


Bu proje, farklı türde mini oyunlar eklenebilecek şekilde tasarlanmış **modüler bir oyun framework'üdür**. Örneğin **Endless Runner**, **Match-3** gibi oyunlar kolayca eklenebilir. Kod yapısı **SOLID prensiplerine** uygundur, sistemler **Zenject (Dependency Injection)** ile yönetilir ve sahneler **Addressables** ile yüklenir.

---

##  Mimarisi

Proje katmanlı bir mimari yapıda tasarlanmıştır.

Core katmanı, oyunun temel yapı taşlarını içerir: FSM sistemi, EventBus,  ve servis arayüzleri bu katmanda bulunur.

MiniGame katmanı, her mini oyunun kendi oyun mantığını içerir. Bu yapı, MiniGameBase<T> üzerinden soyutlanarak FSM'lerle yönetilir.

UI katmanı, panellerin gösterimi, UI tetikleyiciler ve kullanıcı etkileşimlerini içerir. UIManager, panelleri panel ID üzerinden yönetir.

Infrastructure katmanı, sahne yükleyici, kayıt sistemi, Addressables yönetimi gibi altyapısal bağımlılıkları içerir.

Installers katmanı, Zenject üzerinden tüm sistemlerin bağlandığı yerdir. Her katman gerektiği kadar bağımsızdır ve Zenject DI sistemiyle birbirine bağlanır.

## Tercih Edilen Yapılar

- **SceneCatalog**: Yeni mini oyunlar kolayca sahne olarak eklenebilir. Ana mimari kodlarına değişklik yapmadan Scene catalog üzerinden referans verilirler.
- **Bootstrap** Uygulama Bootstrap isimli bir sahnede başlayıp proje temel yapıları ayağa kaldırılarak ana mimari ve bağlılıkları tek bir noktadan başlatılmaktadır.
- **Event tabanlı iletişim**: Sistemler birbirine doğrudan bağlı değildir. Event system game managera bağlanarak orchestration pattern kullanımı hedeflenmiştir.
- **Dependency Injection**: Referans kontrolü için Zenject yapısı kullanılmıştır. Proje bazlı injection ve sahne bazı injection imkanı sunulmuştur.
- **UIManager**: Oyundaki arayüz panelleri UI manager tarafından ayağa kaldırılıyor. Bu da arayüzlerin tekrar kullanılabilir olmasını ve kod yerine verilerle yönetilebilmesini sağlıyor.
- **SceneReference**: Sahneleri asset bundlelar ile eşleştirmeye izin veren bir yapı bulunuyor. Bu şekilde dinamik asset yönetimi yapılabiliyor.
- **FSM yapısı**: Her mini oyun kendi durumlarıyla izole çalışır. Mini oyunlar FSM'lerle yönetilir. Yeni mini oyun oluşturmak için zorunlu tutulmuştur
- **Test'e uygun kod**: Sistemler DI ile geldiği için kolay test edilebilir. Interfaceler ile desteklenmiştir. Sistemin büyük bir kısmı unit teste uygundur.
  
---

### Temel Sistemler
- **EventBus**: Sistemler arası iletişim için event tabanlı yapı. Performans ve gc'a yük oluşturmaması için tercih edildi.
- **SaveSystem**: JSON formatında basit kayıt sistemi. Playerprefs ile çalışıyor.
- **SceneLoader**: Sahneleri yükler ve yükleme ekranı gösterir. Loading kısmında ilerleme gösterilir.
- **MiniGameBase<TState>**: Her mini oyunun temel sınıfı (FSM destekli).

### Mini Oyunlar
- `EndlessRunner`: Tüm oyun döngüsü (FSM, UI, save, asset management, world reset) hazır, görsel eksik.

### UI Sistemi
- **UIManager** ve **UIPanelRegistry** ile paneller yönetilir.
- UI butonları sahne veya durum geçişini tetikler.
- Paneller `ShowPanel("PanelId")` ile açılıp kapanır.

### Dependency Injection (Bağımlılık Yönetimi)
- **Zenject** ile sistemler sahneye bağlı olmadan oluşturulur.
- Her sistem kendi Installer'ı içinde tanımlanır (`CoreInstaller`, `UIInstaller` vs).
- Prefab ve runtime objeler Zenject container üzerinden spawn edilir.

---

##  Bilinen Problemler

- Save system player prefsten güvenli bir sisteme taşınmalı.
- Runner game hareket mekaniğinde buglar var.
- DI ve UI systemin birlikte çalışmasının getirdiği bir bug var. Ana sahneye dönüldüğünde FSM referansları gidiyor.
- Projede temizlenmemiş kodlar (TODO veya kapalı kod gibi) veya doğru adlandırılmamış objeler (TestPanel) kalmış olabilir.  

## Eksikler
  
- Animasyon, ses, görsel geçiş gibi polish çalışmaları eksik.
- Unit test ve editör araçları yapılmadı.
- Match-3 mini-oyun eksik.

---

## 🚀 Gelecekteki Geliştirmeler

- FSM yapısı geliştirilebilir. FSM state geçişleri Async yapılıp geçişlerdeki yük dağıtılabilir.
- UI Management sistemi iyileştirilip işlevselliği artırılabilir.
- Mobil destek: haptics, çözünürlük yönetimi, UI scale.
- Unit test altyapısı.

---

## Not

Bu case, 72 saatten kısa bir sürede geliştirildi.
Odak noktası; görsel detaylar veya içerik büyüklüğünden ziyade kod mimarisi, modüler yapı ve sürdürülebilirlik olmuştur.

---

### Geliştirici

Yunus Emre YALÇINKAYA
