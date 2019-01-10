using System.ComponentModel.DataAnnotations;

namespace Foody.Services.DataServices.Models.Knowledge
{
    public class KnowledgeSearchBindingModel
    {
        [StringLength(30, ErrorMessage = "Maximum 30 character allowed")]
        [RegularExpression("^[\\dA-Za-z\\s]+$", ErrorMessage = "Only digits, letters and whitespaces allowed!")]
        public string SearchText { get; set; }

        public bool IsMicroElement { get; set; }

        public bool IsMacroElement { get; set; }

        public bool IsRecipe { get; set; }

        public bool IsFoodItem { get; set; }
    }
}
