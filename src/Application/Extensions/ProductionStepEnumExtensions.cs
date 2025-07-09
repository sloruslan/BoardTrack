using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ProductionStepEnumExtensions
    {
        public static string ToRussianName(this ProductionStepEnum step)
        {
            return step switch
            {
                ProductionStepEnum.Registration => "Регистрация",
                ProductionStepEnum.InstallationOfComponents => "Установка компонентов",
                ProductionStepEnum.QualityControl => "Контроль качества",
                ProductionStepEnum.Repair => "Ремонт",
                ProductionStepEnum.Packaging => "Упаковывание",
                _ => "Неизвестно"
            };
        }
    }
}
