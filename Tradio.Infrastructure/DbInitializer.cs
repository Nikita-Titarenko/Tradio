using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tradio.Domain;
using Tradio.Infrastructure.Common;

namespace Tradio.Infrastructure
{
    public static class DbInitializer
    {
        public async static Task InitializeAsync(ApplicationDbContext context)
        {
            if (await context.Services.CountAsync() != 0)
            {
                return;
            }


            var users = new ApplicationUser[]
            {
                new ApplicationUser { Email = "user1@example.com", NormalizedEmail = "USER1@EXAMPLE.COM", Fullname = "Олександр Іваненко", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 100, UserName = "user1", NormalizedUserName = "USER1", CityId = 1, VerificationCode = "111111", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user2@example.com", NormalizedEmail = "USER2@EXAMPLE.COM", Fullname = "Марія Петренко", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 120, UserName = "user2", NormalizedUserName = "USER2", CityId = 2, VerificationCode = "222222", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user3@example.com", NormalizedEmail = "USER3@EXAMPLE.COM", Fullname = "Андрій Коваленко", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 150, UserName = "user3", NormalizedUserName = "USER3", CityId = 3, VerificationCode = "333333", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user4@example.com", NormalizedEmail = "USER4@EXAMPLE.COM", Fullname = "Ірина Шевченко", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 200, UserName = "user4", NormalizedUserName = "USER4", CityId = 4, VerificationCode = "444444", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user5@example.com", NormalizedEmail = "USER5@EXAMPLE.COM", Fullname = "Дмитро Савченко", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 90, UserName = "user5", NormalizedUserName = "USER5", CityId = 5, VerificationCode = "555555", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user6@example.com", NormalizedEmail = "USER6@EXAMPLE.COM", Fullname = "Катерина Ткаченко", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 80, UserName = "user6", NormalizedUserName = "USER6", CityId = 6, VerificationCode = "666666", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user7@example.com", NormalizedEmail = "USER7@EXAMPLE.COM", Fullname = "Олег Марченко", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 70, UserName = "user7", NormalizedUserName = "USER7", CityId = 7, VerificationCode = "777777", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user8@example.com", NormalizedEmail = "USER8@EXAMPLE.COM", Fullname = "Анна Гаврилюк", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 95, UserName = "user8", NormalizedUserName = "USER8", CityId = 8, VerificationCode = "888888", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user9@example.com", NormalizedEmail = "USER9@EXAMPLE.COM", Fullname = "Михайло Кузьменко", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 60, UserName = "user9", NormalizedUserName = "USER9", CityId = 9, VerificationCode = "999999", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user10@example.com", NormalizedEmail = "USER10@EXAMPLE.COM", Fullname = "Юлія Остапенко", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 130, UserName = "user10", NormalizedUserName = "USER10", CityId = 10, VerificationCode = "101010", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user11@example.com", NormalizedEmail = "USER11@EXAMPLE.COM", Fullname = "Віктор Павленко", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 75, UserName = "user11", NormalizedUserName = "USER11", CityId = 11, VerificationCode = "111111", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user12@example.com", NormalizedEmail = "USER12@EXAMPLE.COM", Fullname = "Олена Романюк", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 85, UserName = "user12", NormalizedUserName = "USER12", CityId = 1, VerificationCode = "121212", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user13@example.com", NormalizedEmail = "USER13@EXAMPLE.COM", Fullname = "Назар Білик", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 110, UserName = "user13", NormalizedUserName = "USER13", CityId = 2, VerificationCode = "131313", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user14@example.com", NormalizedEmail = "USER14@EXAMPLE.COM", Fullname = "Інна Мороз", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 95, UserName = "user14", NormalizedUserName = "USER14", CityId = 3, VerificationCode = "141414", SecurityStamp = Guid.NewGuid().ToString() },
                new ApplicationUser { Email = "user15@example.com", NormalizedEmail = "USER15@EXAMPLE.COM", Fullname = "Павло Сидоренко", EmailConfirmed = true, ConcurrencyStamp = Guid.NewGuid().ToString(), CreditCount = 105, UserName = "user15", NormalizedUserName = "USER15", CityId = 4, VerificationCode = "151515", SecurityStamp = Guid.NewGuid().ToString() },
            };
            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            var services = new Service[]
            {
                new Service { ApplicationUserId = users[0].Id, CategoryId = 2, Name = "Математика для школярів", Description = "Допоможу з алгеброю та геометрією.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 15 },
                new Service { ApplicationUserId = users[1].Id, CategoryId = 2, Name = "Фізика онлайн", Description = "Розбір задач та пояснення тем.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 20 },
                new Service { ApplicationUserId = users[2].Id, CategoryId = 2, Name = "Хімія для студентів", Description = "Допомога з лабораторними та теорією.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 25 },
                new Service { ApplicationUserId = users[3].Id, CategoryId = 2, Name = "Підготовка до ЗНО", Description = "Комплексна підготовка з усіх предметів.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 30 },
                new Service { ApplicationUserId = users[4].Id, CategoryId = 2, Name = "Онлайн-репетитор з біології", Description = "Допомога у вивченні біології.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 18 },


                new Service { ApplicationUserId = users[5].Id, CategoryId = 3, Name = "Уроки гри на гітарі", Description = "Навчу базовим акордам та імпровізації.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 25 },
                new Service { ApplicationUserId = users[6].Id, CategoryId = 3, Name = "Уроки фортепіано", Description = "Для початківців та просунутих.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 30 },
                new Service { ApplicationUserId = users[7].Id, CategoryId = 3, Name = "Вокальні заняття", Description = "Розвиток голосу та техніки співу.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 20 },
                new Service { ApplicationUserId = users[8].Id, CategoryId = 3, Name = "Скрипка для дітей", Description = "Індивідуальні уроки для дітей.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 22 },
                new Service { ApplicationUserId = users[9].Id, CategoryId = 3, Name = "Барабани та ритм", Description = "Вивчення ритму та гри на барабанах.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 28 },


                new Service { ApplicationUserId = users[10].Id, CategoryId = 4, Name = "Англійська для початківців", Description = "Онлайн-уроки англійської мови.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 20 },
                new Service { ApplicationUserId = users[11].Id, CategoryId = 4, Name = "Німецька мова", Description = "Індивідуальні заняття з німецької.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 25 },
                new Service { ApplicationUserId = users[12].Id, CategoryId = 4, Name = "Французька онлайн", Description = "Онлайн уроки французької для дорослих.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 30 },
                new Service { ApplicationUserId = users[13].Id, CategoryId = 4, Name = "Іспанська для початківців", Description = "Базові уроки іспанської мови.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 20 },
                new Service { ApplicationUserId = users[14].Id, CategoryId = 4, Name = "Італійська мова", Description = "Короткі курси для подорожей та бізнесу.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 22 },

                new Service { ApplicationUserId = users[3].Id, CategoryId = 5, Name = "Основи програмування", Description = "Python, C#, алгоритми та логіка.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 30 },
                new Service { ApplicationUserId = users[4].Id, CategoryId = 7, Name = "Послуги сантехніка", Description = "Ремонт змішувачів, труб, зливів.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 40 },
                new Service { ApplicationUserId = users[5].Id, CategoryId = 8, Name = "Електромонтажні роботи", Description = "Монтаж розеток, світильників, лічильників.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 50 },
                new Service { ApplicationUserId = users[6].Id, CategoryId = 9, Name = "Збірка меблів IKEA", Description = "Якісно та швидко зберу меблі будь-якої складності.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 35 },
                new Service { ApplicationUserId = users[7].Id, CategoryId = 10, Name = "Малярні роботи", Description = "Фарбування стін, шпалери, декор.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 45 },
                new Service { ApplicationUserId = users[8].Id, CategoryId = 12, Name = "Дизайн логотипів", Description = "Створення сучасних логотипів для бізнесу.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 60 },
                new Service { ApplicationUserId = users[9].Id, CategoryId = 13, Name = "Копірайтинг", Description = "Написання текстів для сайтів і реклами.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 20 },
                new Service { ApplicationUserId = users[10].Id, CategoryId = 14, Name = "Розробка вебсайтів", Description = "Front-end і back-end під ключ.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 200 },
                new Service { ApplicationUserId = users[11].Id, CategoryId = 15, Name = "SMM-просування", Description = "Просування сторінок у соцмережах.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 70 },
                new Service { ApplicationUserId = users[12].Id, CategoryId = 17, Name = "Допомога в іграх", Description = "Поради та тренування в популярних іграх.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 10 },
                new Service { ApplicationUserId = users[13].Id, CategoryId = 21, Name = "Персональні тренування", Description = "Складання індивідуальної програми тренувань.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 50 },
                new Service { ApplicationUserId = users[14].Id, CategoryId = 22, Name = "Консультації з харчування", Description = "Розробка плану здорового раціону.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 40 },
                new Service { ApplicationUserId = users[0].Id, CategoryId = 23, Name = "Психологічна підтримка", Description = "Допомога у подоланні стресу.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 55 },
                new Service { ApplicationUserId = users[1].Id, CategoryId = 25, Name = "Доставка товарів", Description = "Швидка доставка по місту.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 25 },
                new Service { ApplicationUserId = users[2].Id, CategoryId = 26, Name = "Поїздки по місту", Description = "Комфортне перевезення пасажирів.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 30 },
                new Service { ApplicationUserId = users[3].Id, CategoryId = 28, Name = "Догляд за тваринами", Description = "Догляд за котами і собаками під час відсутності власника.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 20 },
                new Service { ApplicationUserId = users[4].Id, CategoryId = 31, Name = "Бізнес-консультації", Description = "Консультації для малого бізнесу.", IsVisible = true, CreationDateTime = DateTime.UtcNow, Price = 100 },
            };

            context.Services.AddRange(services);
            await context.SaveChangesAsync();
        }
    }
}
