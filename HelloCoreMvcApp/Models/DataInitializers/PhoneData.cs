using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCoreMvcApp.Models;
using HelloCoreMvcApp.Models.Products;

namespace HelloCoreMvcApp.Models.DataInitializers
{
    public class PhoneData
    {
        public static void Initialize(MobileContext context)
        {
            if (!context.Phones.Any())
            {
                context.Phones.AddRange(GeneratePhoneItems(50));
                context.SaveChanges();
                
            }
        }

        private static IEnumerable<Phone> GeneratePhoneItems(int numOfItems = 0)
        {
            Random rnd = new Random();
            List<Phone> phones = new List<Phone>();

            string[] producer = { "Apple", "Samsung", "LG", "Sony", "Nokia", "HTC", "ASUS", "Huawei", "Xiaomi", "Honor", "Acer", "Panasonic" };
            string[] model = { "Galazyy", "Hero", "Bixel", "Edge", "Prime", "HelloKitty", "Zperia", "MeowMe", "DIO", "Two", "TUF", "Poison" };
            string[] num = { "II", "III", "S", "SS", "X10", "X9", "Z", "X", "One", "2D", "3D", "10D", "99D", "1D" };

            string[] description =
            {
                "[4x1.3 ГГц, 1 ГБ, 2 SIM, TN, 800x480, камера 2 Мп, 3G, GPS, 1600 мА*ч]",
                "[6x2.65 ГГц, 4 ГБ, 1 SIM, IPS, 1792x828, камера 12+12 Мп, 3G, 4G, NFC, GPS, 3110 мА*ч]",
                "[8x2.3 ГГц, 4 ГБ, 2 SIM, Super AMOLED, 2400x1080, камера 48+12+5+5 Мп, 3G, 4G, NFC, GPS, 4000 мА*ч]",
                "[8x1.8 ГГц, 2.3 ГГц, 6 ГБ, 2 SIM, IPS, 2400x1080, камера 64+8+5+2 Мп, 3G, 4G, NFC, GPS, FM, 5020 мА*ч]",
                "[8x2.2 ГГц, 1.7 ГГц, 4 ГБ, 2 SIM, IPS, 2340x1080, камера 24+8+2 Мп, 3G, 4G, NFC, GPS, FM, 3400 мА*ч]",
                "[6x2.5 ГГц, 3 ГБ, 1 SIM, IPS, 1792x828, камера 12 Мп, 3G, 4G, NFC, GPS, 2942 мА*ч]"
            };

            for (int i = 0; i < numOfItems; i++)
            {
                Phone newPhone = new Phone
                {
                    Company = producer[rnd.Next(0, producer.Length)],
                    Name = model[rnd.Next(0, model.Length)] + " " + num[rnd.Next(0, num.Length)],
                    Price = rnd.Next(70, 1500),
                    ShortDescription = description[rnd.Next(0, description.Length)]
                };

                phones.Add(newPhone);
            }

            return phones;
        }
    }
}
