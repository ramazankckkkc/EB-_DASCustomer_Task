# Müşteri Yönetim Sistemi

## Genel Bakış
Müşteri Yönetim Sistemi, ASP.NET Core WebAPI ve MVC kullanılarak geliştirilmiş bir örnek projedir. 

## Özellikler
-  Müşteri Yönetimi: Müşteri verilerini yönetmek için CRUD işlemleri.
-  Middleware: Tutarlı hata yanıtları için özel hata yönetim katmanı.
-  Validayson işlmeleri ile veri hatalarını elimine edildi.
## Başlarken

### Gereksinimler
- ** .NET 8 SDK
- ** SQL Server
### Kurulum
1. Depoyu klonlayın:
 ```sh
 git clone https://github.com/ramazankckkkc/EB-_DASCustomer_Task.git
 ```
2. Proje dizinine gidin:
 ```sh
cd EB _DASCustomer_Task
  ```
3. Bağımlılıkları geri yükleyin:
 ```
 dotnet restore
  ```
### Veritabanı Kurulumu

1. `appsettings.json` dosyasındaki bağlantı dizesini SQL Server örneğinize göre güncelleyin.
2. Veritabanı şemasını oluşturmak için migrasyonları uygulayın:
    ```sh
    dotnet ef database update
    ```
3. Veritabanı yedeğini (`CustomerDb.bak`) SQL Server Management Studio (SSMS) kullanarak geri yükleyin.

### Uygulamayı Çalıştırma

1. Projeyi derleyin:
    ```sh
    dotnet build
    ```
2. Projeyi çalıştırın:
    ```sh
    dotnet run
    ```
### API Dokümantasyonu

Swagger aracılığıyla API dokümantasyonu mevcuttur. Uygulama çalıştığında, kullanılabilir uç noktaları keşfetmek için tarayıcınızda `/swagger` adresine gidin.

## Kullanım

### Müşteri Api İşlemleri

- **Tüm Müşterileri Getir**: `GET /api/customers`
- **ID ile Müşteri Getir**: `GET /api/customers/{id}`
- **Müşteri Oluştur**: `POST /api/customers/add`
- **Müşteri Güncelle**: `PUT /api/customers/update/{id}`
- **Müşteri Sil**: `DELETE /api/customers/delete/{id}`

### Müşteri Web İşlemleri
- **Tüm Müşterileri Listeleme**: ` /Customer`
- **Müşteri Oluştur**: `/Customer/CustomerAdd`

## Lisans

Bu proje MIT Lisansı ile lisanslanmıştır.

---

Daha fazla detay için [repo](https://github.com/ramazankckkkc/EB-_DASCustomer_Task) adresini ziyaret edin.
