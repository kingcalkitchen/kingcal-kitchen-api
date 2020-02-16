//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace KingCal.Data.Entities
//{
//    [Table("RecipeIngredients", Schema = "Food")]
//    public class RecipeIngredient
//    {
//        [Key]
//        public Guid Id { get; set; }

//        public Guid RecipeId { get; set; }

//        public Guid FoodId { get; set; }

//        public Guid UnitOfMeasureId { get; set; }

//        public string Amount { get; set; }


//        [ForeignKey("RecipeId")]
//        public virtual Recipe Recipe { get; set; }

//        [ForeignKey("FoodId")]
//        public virtual Food Food { get; set; }

//        [ForeignKey("UnitOfMeasureId")]
//        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
//    }
//}
