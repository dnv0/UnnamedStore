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
            if (!context.Companies.Any())
            {
                context.Companies.AddRange(GenerateCompanies());
            }
            if (!context.Phones.Any())
            {
                context.Phones.AddRange(GeneratePhoneItems(context, 50)); 
            }
            context.SaveChanges();
        }

        private static IEnumerable<Phone> GeneratePhoneItems(MobileContext context, int numOfItems = 0)
        {
            Random rnd = new Random();
            List<Phone> phones = new List<Phone>();
            IEnumerable<Company> companies = context.Companies;

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
                    Company = companies.ElementAt(rnd.Next(0, companies.ToList().Count)),
                    Name = model[rnd.Next(0, model.Length)] + " " + num[rnd.Next(0, num.Length)],
                    Price = rnd.Next(70, 1500),
                    ShortDescription = description[rnd.Next(0, description.Length)]
                };

                phones.Add(newPhone);
            }

            return phones;
        }

        private static IEnumerable<Company> GenerateCompanies()
        {
            Company[] companieArr =
            {
                new Company()
                {
                    Name = "Apple", Description = "The biggest fruit company in the world"
                },
                new Company()
                {
                    Name = "Samsung", Description = "Our products are actually bombs"
                },
                new Company()
                {
                    Name = "LG", Description = "Yeah, we still make phones"
                },
                new Company()
                {
                    Name = "Sony", Description = "We know nothing about hegdehogs, stop asking it"
                },
                new Company()
                {
                    Name = "Nokia", Description = "Thor some years ago order us to make a hammer for him"
                },
                new Company()
                {
                    Name = "HTC", Description = "H T C - hangover. today. cool."
                },
                new Company()
                {
                    Name = "ASUS", Description = "A S U S - admin see, u su"
                },
                new Company()
                {
                    Name = "Huawei", Description = "幸福猫 微笑"
                },
                new Company()
                {
                    Name = "Xiaomi", Description = "We make poor people happier"
                },
                new Company()
                {
                    Name = "Honor", Description = "We honor your money wallet"
                },
                new Company()
                {
                    Name = "Acer", Description = "Cambridge dictionary says that acer means type of tree whose leaves often turn bright red, yellow or orange in autumn. Why the heck we named like that"
                },
                new Company()
                {
                    Name = "Panasonic", Description = "No, no hedgehogs, we say no"
                }
            };
            List<Company> companies = new List<Company>();

            foreach(var item in companieArr)
            {
                companies.Add(item);
            }

            return companies;
        }
    }
}
