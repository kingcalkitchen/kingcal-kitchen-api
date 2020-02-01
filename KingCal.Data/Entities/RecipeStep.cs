using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KingCal.Data.Entities
{
    [Table("RecipeSteps", Schema = "Food")]
    public class RecipeStep
    {
        [Key]
        public Guid Id { get; set; }

        public Guid RecipeId { get; set; }

        public int Step { get; set; }

        public string Action { get; set; }


        [ForeignKey("RecipeId")]
        public virtual Recipe Recipe { get; set; }
    }
}
