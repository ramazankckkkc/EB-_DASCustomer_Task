using EB__DASCustomer_TaskWebAPI.Bases;

namespace EB__DASCustomer_TaskWebAPI.Entites
{
    public class Customer : BaseEntity<string>
    {
        /// <summary>
        /// Müşteri adı (Zorunlu)
        /// </summary>
        public string FirstName { get; set; } = default!;
        /// <summary>
        /// Müşteri Soyadı (Zorunlu)
        /// </summary>
        public string LastName { get; set; } = default!;
        /// <summary>
        /// Email (Zorunlu)
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// Müşteri telefon numarası (Zorunlu)
        /// </summary>
        public string PhoneNumber { get; set; } = default!;
        /// <summary>
        /// Resim Yolu(Zorunlu)
        /// </summary>
        public string ImageUrl { get; set; } = default!;
        /// <summary>
        /// Doğum Tarihi Yolu(Zorunlu)
        /// </summary>
        public DateTime BirthDay { get; set; }

    }
}