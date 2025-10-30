using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpendingAnalysis.Core.Models;

namespace SpendingAnalysis.DataAccess.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }  // Пароль храним хэшированным

        public ICollection<OperationEntity> Operations { get; set; }

        public ICollection<CategoryEntity> CategoryEntities { get; set; }
    }
}
