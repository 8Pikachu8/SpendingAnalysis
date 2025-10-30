using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpendingAnalysis.Core.Models;
using SpendingAnalysis.DataAccess.Entities;

namespace SpendingAnalysis.DataAccess.Consts
{
    internal static class CommonCosnts
    {
        public static CategoryEntity[] CategoryEntities = 
        { 
            new CategoryEntity() { Id=Guid.NewGuid(), Name = "Продукты", OperationTypeId = 0 } ,
            new CategoryEntity() { Id=Guid.NewGuid(), Name = "Кафе и рестораны", OperationTypeId = 0 },
            new CategoryEntity() { Id=Guid.NewGuid(), Name = "Одежда", OperationTypeId = 0 } ,
            new CategoryEntity() { Id=Guid.NewGuid(), Name = "Игры и развлечения", OperationTypeId = 0 },
            new CategoryEntity() { Id=Guid.NewGuid(), Name = "Подписки", OperationTypeId = 0 } ,
            new CategoryEntity() { Id=Guid.NewGuid(), Name = "Транспорт", OperationTypeId = 0 },
            new CategoryEntity() { Id=Guid.NewGuid(), Name = "Дом и ремонт", OperationTypeId = 0 } ,
            new CategoryEntity() { Id=Guid.NewGuid(), Name = "Здоровье", OperationTypeId = 0 },
            new CategoryEntity() { Id=Guid.NewGuid(), Name = "Зарплата", OperationTypeId = 1 }
        };
    }
}
