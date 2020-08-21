using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCoreMvcApp.Models;

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

            string[] producer = { "Abble", "Zamzung", "GL", "Zony", "Donkia", "HTZ", "AZUZA", "Hwuawua", "Ninecent", "PPpupu", "Azer", "JOJO" };
            string[] model = { "Galazyy", "Hero", "Bixel", "Edge", "Prime", "HelloKitty", "Zperia", "MeowMe", "DIO", "Two", "TUF", "Poison" };
            string[] num = { "II", "III", "S", "SS", "X10", "X9", "Z", "X", "One", "2D", "3D", "10D", "99D", "1D" };

            for (int i = 0; i < numOfItems; i++)
            {
                Phone newPhone = new Phone();
                newPhone.Company = producer[rnd.Next(0, producer.Length)];
                newPhone.Name = model[rnd.Next(0, model.Length)] + " " + num[rnd.Next(0, num.Length)];
                newPhone.Price = rnd.Next(70, 1500);

                phones.Add(newPhone);
            }

            return phones;
        }
    }
}
