using System.ComponentModel.DataAnnotations;

namespace src.Enums
{

    public enum Level
    {
        [Display(Name = "100")]
        one,
        [Display(Name = "200")]
        two,
        [Display(Name = "300")]
        three,
        [Display(Name = "400")]
        four,
        [Display(Name = "500")]
        five        
    }
}
