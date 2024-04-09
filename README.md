# Emlak-Otomasyonu
C# - Emlak Otomasyonu / Real Estate Automation
<!-- wp:heading -->
<h2>Bu projemde basit bir emlak otomasyonunda gerekli olan işlevlerler vardır. Amatör kod hataları vb. unsurlar bulunabilir.</h2>
Giriş Bilgileri => Kullanıcı Adı: admin Şifre: 123
<!-- /wp:heading -->

## Giriş
![](https://github.com/omercanbilgin/Emlak-Otomasyonu/blob/main/Giri%C5%9F%20Ekran%C4%B1.png)
## İşlem Seçme Ekranı
![](https://github.com/omercanbilgin/Emlak-Otomasyonu/blob/main/%C4%B0%C5%9Flem%20Se%C3%A7me%20Ekran%C4%B1.png)
## İlanlar Ekranı
![](https://github.com/omercanbilgin/Emlak-Otomasyonu/blob/main/%C4%B0lanlar%20Ekran%C4%B1.png)
## İlan Ekleme Ekranı
![](https://github.com/omercanbilgin/Emlak-Otomasyonu/blob/main/%C4%B0lan%20Ekleme%20Ekran%C4%B1.png)

<h3>Proje Hakkında Bilgilendirme</h3>

<p>Arayüzde, kullanıcıların sisteme giriş yapabilmeleri için kullanıcı adı ve şifre girişi gibi temel öğeler bulunmaktadır. Kullanıcılar, ilanları filtreleyebilmek ve aramak için kullanabilecekleri bir ana ekranla karşılanmaktadır. İlan detaylarına ulaşmak veya yeni bir ilan eklemek için özel form alanları sunulmaktadır. Veritabanından alınan gerçek zamanlı verilerle güncellenen ve kullanıcıya anlık geri bildirimler sağlayan bir arayüz tasarlanarak, basit bir otomasyon elde edilmiştir. Proje kapsamında PostgreSQL veritabanı kullanılmıştır. İlk olarak, "kullanici" tablosu, projede yer alan kullanıcı bilgilerini depolamak için kullanılmaktadır. Bu tablo, kullanıcı adı ve şifresini içermektedir. İkinci olarak, "ilan_sahibi" tablosu, ilan veren kişilerin ad-soyad ve telefon numaralarını içermektedir. "sehir", "ilce" ve "mahalle" tabloları, ilanların konum bilgilerini düzenlemek için kullanılmaktadır. Şehir, ilçe ve mahalle tabloları, birbirlerine referanslar ile bağlanarak hiyerarşik bir yapı oluşturur ve her bir ilanın konum bilgilerini belirlemekte kullanılır. Son olarak, "ev" tablosu, emlak ilanlarını depolamak için kullanılmaktadır. Bu tablo, evin türü, metrekare, oda sayısı, kat numarası, fiyat, ilan sahibi, mahalle, ilan türü gibi özellikleri içermektedir.</p>

## Veri Tabanı Diagramı
![](https://github.com/omercanbilgin/Emlak-Otomasyonu/blob/main/Veri%20Taban%C4%B1%20Diagram%C4%B1.png)
