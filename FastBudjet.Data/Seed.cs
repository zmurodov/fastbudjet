using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FastBudjet.Data.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace FastBudjet.Data
{
    public class Seed
    {
        public static async Task SeedData(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        CategoryId = 1,
                        Name = "Еда/Напитки",
                        Image = "ic_shopping_cart.svg",
                        Income = false
                    },
                    new Category
                    {
                        CategoryId = 2,
                        Name = "Питание",
                        Image = "ic_plate_fork_knife.svg",
                        Income = false,
                        ParentId = 1
                    },
                    new Category
                    {
                        CategoryId = 3,
                        Name = "Бар",
                        Image = "ic_cup.svg",
                        Income = false,
                        ParentId = 1
                    },
                    new Category
                    {
                        CategoryId = 4,
                        Name = "Покупка товаров",
                        Image = "ic_base_shopping.svg",
                        Income = false,
                    },
                    new Category
                    {
                        CategoryId = 5,
                        Name = "Одежда",
                        Image = "ic_t_shirt.svg",
                        Income = false,
                        ParentId = 4
                    },
                    new Category
                    {
                        CategoryId = 6,
                        Name = "Обувь",
                        Image = "ic_shoe.svg",
                        Income = false,
                        ParentId = 4
                    },
                    new Category
                    {
                        CategoryId = 7,
                        Name = "Технология",
                        Image = "ic_hard_drive.svg",
                        Income = false,
                        ParentId = 4
                    },
                    new Category
                    {
                        CategoryId = 8,
                        Name = "Подарки",
                        Image = "ic_gift.svg",
                        Income = false,
                        ParentId = 4
                    },
                    new Category
                    {
                        CategoryId = 9,
                        Name = "Транспорт",
                        Image = "ic_coach.svg",
                        Income = false,
                    },
                    new Category
                    {
                        CategoryId = 10,
                        Name = "Машина",
                        Image = "ic_car.svg",
                        Income = false,
                        ParentId = 9
                    },
                    new Category
                    {
                        CategoryId = 11,
                        Name = "Бензин",
                        Image = "ic_fuel.svg",
                        Income = false,
                        ParentId = 9
                    },
                    new Category
                    {
                        CategoryId = 12,
                        Name = "Страхование",
                        Image = "ic_umbrella.svg",
                        Income = false,
                        ParentId = 9
                    },
                    new Category
                    {
                        CategoryId = 13,
                        Name = "Развлечения",
                        Image = "ic_party_hat.svg",
                        Income = false,
                    },
                    new Category
                    {
                        CategoryId = 14,
                        Name = "Книги / журналы",
                        Image = "ic_journal.svg",
                        Income = false,
                        ParentId = 13
                    },
                    new Category
                    {
                        CategoryId = 15,
                        Name = "Семья",
                        Image = "ic_family.svg",
                        Income = false,
                    },
                    new Category
                    {
                        CategoryId = 16,
                        Name = "Дети",
                        Image = "ic_pacifier.svg",
                        Income = false,
                        ParentId = 15
                    },
                    new Category
                    {
                        CategoryId = 17,
                        Name = "Дом",
                        Image = "ic_home.svg",
                        Income = false,
                        ParentId = 15
                    },
                    new Category
                    {
                        CategoryId = 18,
                        Name = "Доход от аренды",
                        Image = "ic_house_rent.svg",
                        Income = false,
                        ParentId = 15
                    },
                    new Category
                    {
                        CategoryId = 19,
                        Name = "Здоровье",
                        Image = "ic_health.svg",
                        Income = false,
                    },
                    new Category
                    {
                        CategoryId = 20,
                        Name = "Спорт",
                        Image = "ic_weights.svg",
                        Income = false,
                        ParentId = 19
                    },
                    new Category
                    {
                        CategoryId = 21,
                        Name = "Домашние животные",
                        Image = "ic_footprint.svg",
                        Income = false,
                    },
                    new Category
                    {
                        CategoryId = 22,
                        Name = "Еда - Домашние животные",
                        Image = "ic_base_pet_food.svg",
                        Income = false,
                        ParentId = 21
                    },
                    new Category
                    {
                        CategoryId = 23,
                        Name = "Путешествия",
                        Image = "ic_plane.svg",
                        Income = false,
                    },
                    new Category
                    {
                        CategoryId = 24,
                        Name = "Услуги",
                        Image = "ic_bed.svg",
                        Income = false,
                        ParentId = 23
                    },
                    new Category
                    {
                        CategoryId = 25,
                        Name = "Транспорт - Путешествия",
                        Image = "ic_coach.svg",
                        Income = false,
                        ParentId = 23
                    },
                    new Category
                    {
                        CategoryId = 26,
                        Name = "Финансовые доходы",
                        Image = "ic_bank.svg",
                        Income = true,
                    },
                    new Category
                    {
                        CategoryId = 27,
                        Name = "Доходы",
                        Image = "ic_base_incomes.svg",
                        Income = true,
                    },
                    new Category
                    {
                        CategoryId = 28,
                        Name = "Случайные заработки",
                        Image = "ic_tools.svg",
                        Income = true,
                        ParentId = 27
                    },
                    new Category
                    {
                        CategoryId = 29,
                        Name = "Зарплата",
                        Image = "ic_workbag.svg",
                        Income = true,
                        ParentId = 27
                    },
                    new Category
                    {
                        CategoryId = 30,
                        Name = "Пенсия",
                        Image = "ic_old_man.svg",
                        Income = true,
                        ParentId = 27
                    },
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            if (!context.Accounts.Any())
            {
                var accounts = new List<Account>
                {
                    new Account
                    {
                        Name = "Наличные",
                        Balance = 0,
                        Id = 1
                    },
                    new Account
                    {
                        Name = "Банковский счет",
                        Balance = 0,
                        Id = 2
                    },
                    new Account
                    {
                        Name = "Кредитная карта",
                        Balance = 0,
                        Id = 3
                    },
                };

                await context.Accounts.AddRangeAsync(accounts);
                await context.SaveChangesAsync();
            }
        }
    }
}