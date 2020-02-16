//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace KingCal.Data.Entities
//{
//    [Table("MealNutritionFacts", Schema = "Food")]
//    public class MealNutritionFact
//    {
//        [Key]
//        public Guid Id { get; set; }

//        public Guid MealId { get; set; }

//        public int Protein { get; set; }

//        public int Carbs { get; set; }

//        public int Fats { get; set; }

//        public int Calories { get; set; }


//        [ForeignKey("MealId")]
//        public virtual Meal Meal { get; set; }
//    }
//}
